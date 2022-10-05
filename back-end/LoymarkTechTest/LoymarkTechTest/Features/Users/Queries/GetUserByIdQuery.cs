using CEZ.LoymarkTechTest.WebAPI.Infrastructure.Persistence.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CEZ.LoymarkTechTest.WebAPI
{
    public class GetUserByIdQuery
    {
        public class Query : IRequest<QueryResult>
        {
            [Required]
            public int Id { get; set; }
        }


        public class QueryResult
        {
            public UserDTO Result { get; set; }
        }

        public class UserDTO
        {
            public int Id { get; set; }
            public string Name { get; set; }         
            public string Surname { get; set; }  
            public DateTime Birthday { get; set; }
            public string Email { get; set; }
            public int? Telephone { get; set; }
            public CountryDTO Country { get; set; }
            public bool WishesToBeContacted { get; set; }
        }
        public class CountryDTO
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
            public int Value { get; set; }
        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator(Context db)
            {
                RuleFor(q => q.Id)
                    .Must((inst, userId, context) =>
                    {
                        return db.Users.Any(x => x.Id == userId);
                    }).WithMessage("User not found");
            }

            public class Handler : IRequestHandler<Query, QueryResult>
            {
                private Context _db;

                public Handler(Context db)
                {
                    _db = db;
                }

                public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
                {
                    var result = await _db.Users.Include(x => x.Country).FirstOrDefaultAsync(x => x.Id == request.Id);

                    UserDTO user = new UserDTO
                    {
                        Id = result.Id,
                        Name = result.Name,
                        Surname = result.Surname,
                        Birthday = result.Birthday,
                        Email = result.Email,
                        Telephone = result.Telephone,
                        Country = new CountryDTO() {
                            Id = result.Country.Id,
                            Name = result.Country.Name,
                            Code = result.Country.Code,
                            Value = result.Country.Value
                        },
                        WishesToBeContacted = result.WishesToBeContacted
                    };


                    return new QueryResult
                    {
                        Result = user
                    };
                }
            }
        }
        
    }
}
