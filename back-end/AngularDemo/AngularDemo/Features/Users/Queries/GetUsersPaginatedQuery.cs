using CEZ.AngularDemo.WebAPI.Infrastructure.Persistence.Entities;
using FluentValidation;
using CEZ.AngularDemo.WebAPI.Infrastructure.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace CEZ.AngularDemo.WebAPI
{
    public class GetUsersPaginatedQuery
    {
        public class Query : IRequest<QueryResult>
        {
            [Required]
            public int Page { get; set; }
            [Required]
            public int PageSize { get; set; }
            public string? OrderBy { get; set; }
            public int? Direction { get; set; }
            public string? Name { get; set; }
        }

        public class QueryResult
        {
            public PaginatedResult<UserDTO> Result { get; set; }
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
                RuleFor(q => q.Page)
                    .NotNull().WithMessage("Page number can't be empty")
                    .GreaterThanOrEqualTo(0).WithMessage("Page number can't be less than 0");

                RuleFor(q => q.PageSize)
                    .NotEmpty().WithMessage("Page size can't be empty")
                    .GreaterThan(0).WithMessage("Page size must be higher than 0");            
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
                    var results = _db.Users.Include(x => x.ChangeHistory)
                                           .Include(x => x.Country)
                                           .Where(x => x.Active);

                    if (!String.IsNullOrEmpty(request.Name))
                    {
                        results = results.Where(x => x.Name.ToLower().Contains(request.Name.ToLower()));
                    }

                    var TotalObjects = results.Count();
                    int TotalPages = ((TotalObjects - 1) / request.PageSize) + 1;

                    var orderBy = String.IsNullOrEmpty(request.OrderBy) ? "Name" : request.OrderBy;
                    var direction = request.Direction == -1 ? "DESC" : "ASC";
                    var resultsPaginated = await results.OrderBy(orderBy+" "+direction).PaginateAsync(request.Page, request.PageSize);

                    List<UserDTO> list = new List<UserDTO>();
                    foreach (var x in resultsPaginated.Result)
                    {
                        UserDTO user = new UserDTO
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Surname = x.Surname,
                            Birthday = x.Birthday,
                            Email = x.Email,
                            Telephone = x.Telephone,
                            Country = new CountryDTO
                            {
                                Id = x.Country.Id,
                                Name = x.Country.Name,
                                Code = x.Country.Code,
                                Value = x.Country.Value
                            },
                            WishesToBeContacted = x.WishesToBeContacted
                        };
                        list.Add(user);
                    }

                    return new QueryResult
                    {
                        Result = new PaginatedResult<UserDTO>
                        {
                            TotalObjects = TotalObjects,
                            TotalPages = TotalPages,
                            CurrentPage = request.Page,
                            Result = list
                        }
                    };
                }
            }
        }
        
    }
}
