using CEZ.AngularDemo.WebAPI.Infrastructure.Persistence.Entities;
using CEZ.AngularDemo.WebAPI.Infrastructure.Utils.Enums;
using FluentValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace CEZ.AngularDemo.WebAPI
{
    public class CreateUserCommand
    {
        public class Command : IRequest<CommandResult>
        {
            // Basic Info
            [Required]
            [MaxLength(50)]
            public string Name { get; set; }

            [Required]
            [MaxLength(50)]
            public string Surname { get; set; }
            
            [Required]
            public DateTime Birthday { get; set; }

            [Required]
            public string Email { get; set; }

            public int Telephone { get; set; }
            
            [Required]
            [MaxLength(3)]
            public string CountryCode { get; set; }

            [Required]
            public bool WishesToBeContacted { get; set; }
        }

        public class CommandResult
        {
            public int Id { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(Context db)
            {
                RuleFor(x => x.Name)
                    .NotEmpty().NotNull().WithMessage("The name can't be empty")
                    .Must(new Regex("^[a-zA-Z\\s]+$").IsMatch).WithMessage("The name can't contain numbers or special symbols")
                    .MaximumLength(50).WithMessage("The name can't be longer than 50 characters");
                
                RuleFor(x => x.Surname)
                    .NotEmpty().NotNull().WithMessage("The surname can't be empty")
                    .Must(new Regex("^[a-zA-Z\\s]+$").IsMatch).WithMessage("The surname can't contain numbers or special symbols")
                    .MaximumLength(50).WithMessage("The surname can't be longer than 50 characters");
                    
                RuleFor(x => x.CountryCode)
                    .NotEmpty().WithMessage("The country code can't be empty")
                    .Must(x => x.Length == 3).WithMessage("The country code must have 3 letters")
                    .Must(new Regex("^[A-Z]+$").IsMatch).WithMessage("The country code must be capital letters")
                    .MaximumLength(50).WithMessage("The country code can't be longer than 3 characters")
                    .Must((inst, countryCode, context) =>
                     {
                         return db.Countries.Any(c => c.Code.ToLower() == countryCode.ToLower());
                     }).WithMessage("Country not found");

                RuleFor(x => x.Email)
                    .NotEmpty().NotNull().WithMessage("The email can't be empty")
                    .Must((inst, email, context) =>
                    {
                        try
                        {
                            email = new MailAddress(email).Address;
                            return true;
                        }
                        catch (FormatException) { return false; }
                    }).WithMessage("The email is not valid");

                RuleFor(x => x.Birthday)
                    .NotEmpty().NotNull().WithMessage("The date of birth can't be empty");
            }
        }

        public class Handler : IRequestHandler<Command, CommandResult>
        {
            private Context _db;

            public Handler(Context db)
            {
                _db = db;
            }

            public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = new User
                {
                    Name = request.Name,
                    Surname = request.Surname,
                    Birthday = request.Birthday,
                    Email = request.Email,
                    Telephone = request.Telephone,
                    WishesToBeContacted = request.WishesToBeContacted,
                    ChangeHistory = new List<History>(),
                    Active = true
                };

                user.Country = _db.Countries.First(x => x.Code == request.CountryCode);
                
                // Update History
                user.ChangeHistory.Add(new History()
                {
                   ChangeDate = DateTime.Now,
                   ChangeType = (int)ChangeTypeEnum.New
                });

                _db.Users.Add(user);

                await _db.SaveChangesAsync();
                return new CommandResult { Id = user.Id};
            }
        }
    }
}
