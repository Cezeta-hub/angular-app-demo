using CEZ.AngularDemo.WebAPI.Infrastructure.Persistence.Entities;
using CEZ.AngularDemo.WebAPI.Infrastructure.Utils.Enums;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CEZ.AngularDemo.WebAPI
{
    public class DeleteUserCommand
    {
        public class Command : IRequest<CommandResult>
        {
            [Required]
            public int Id { get; set; }
        }

        public class CommandResult
        {
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
                    }).WithMessage("User is already inactive");
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
                var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
                user.Active = false;

                // Update History
                user.ChangeHistory.Add(new History()
                {
                    ChangeDate = DateTime.Now,
                    ChangeType = (int)ChangeTypeEnum.Delete
                });

                _db.Users.Update(user);

                await _db.SaveChangesAsync();
                return new CommandResult { };
            }
        }
    }
}
