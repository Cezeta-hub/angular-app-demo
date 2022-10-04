using CEZ.LoymarkTechTest.WebAPI.Infrastructure.Persistence.Entities;
using CEZ.LoymarkTechTest.WebAPI.Infrastructure.Utils.Enums;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CEZ.LoymarkTechTest.WebAPI
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
            public string Country { get; set; }

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
                RuleFor(c => c.Name)
                    .NotEmpty().WithMessage("The name can't be empty")
                    .MaximumLength(50).WithMessage("The name can't be longer than 50 characters");
                
                RuleFor(c => c.Surname)
                    .NotEmpty().WithMessage("The surname can't be empty")
                    .MaximumLength(50).WithMessage("The surname can't be longer than 50 characters");

                RuleFor(c => c.Country)
                    .NotEmpty().WithMessage("The country code can't be empty")
                    .MaximumLength(50).WithMessage("The country code can't be longer than 3 characters")
                    .Must((inst, countryCode, context) =>
                     {
                         return db.Countries.Any(c => c.Code.ToLower() == countryCode.ToLower());
                     }).WithMessage("Country not found");

                // Add Validator for Email, Date
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

                user.Country = _db.Countries.First(x => x.Code == request.Country);
                
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
