using CEZ.LoymarkTechTest.WebAPI.Infrastructure.Persistence.Entities;
using CEZ.LoymarkTechTest.WebAPI.Infrastructure.Utils.Enums;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CEZ.LoymarkTechTest.WebAPI
{
    public class UpdateUserCommand
    {
        public class Command : IRequest<CommandResult>
        {
            [Required]
            public int Id { get; set; }

            [MaxLength(50)]
            public string? Name { get; set; }

            [MaxLength(50)]
            public string? Surname { get; set; }

            public DateTime? Birthday { get; set; }

            public string? Email { get; set; }

            public int? Telephone { get; set; }

            [MaxLength(3)]
            public string? Country { get; set; }

            public bool? WishesToBeContacted { get; set; }
        }

        public class CommandResult
        {
            public int Id { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(Context db)
            {
                RuleFor(dc => dc.Id)
                    .Must((inst, userId, context) =>
                    {
                        return db.Users.Any(rt => rt.Id == userId);
                    }).WithMessage("User not found")
                    .Must((inst, userId, context) =>
                    {
                        var usr = db.Users.FirstOrDefault(x => x.Id == userId);
                        if (usr != null)
                            return usr.Active;
                        else
                            return true;
                    }).WithMessage("User is not active");
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
                User user = _db.Users.Include(x => x.ChangeHistory)
                                     .Include(x => x.Country)
                                     .Single(x => x.Id == request.Id);
                
                if (!String.IsNullOrEmpty(request.Name))
                    user.Name = request.Name;
                if (!String.IsNullOrEmpty(request.Surname))
                    user.Surname = request.Surname;
                if (request.Birthday != null)
                    user.Birthday = (DateTime)request.Birthday;
                if (!String.IsNullOrEmpty(request.Email))
                    user.Email = request.Email;
                if (request.Telephone != null)
                    user.Telephone = request.Telephone;
                if (!String.IsNullOrEmpty(request.Country))
                {
                    var country = _db.Countries.Single(x => x.Code == request.Country);
                    user.Country = country;
                    user.CountryId = country.Id;
                }
                if (request.WishesToBeContacted != null)
                    user.WishesToBeContacted = (bool)request.WishesToBeContacted;

                // Update History
                user.ChangeHistory.Add(new History()
                {
                    ChangeDate = DateTime.Now,
                    ChangeType = (int)ChangeTypeEnum.Update
                });

                _db.Users.Update(user);

                await _db.SaveChangesAsync();

                return new CommandResult { Id = user.Id };
            }
        }
    }
}
