using CEZ.LoymarkTechTest.WebAPI.Infrastructure.Persistence.Entities;
using FluentValidation;
using CEZ.LoymarkTechTest.WebAPI.Infrastructure.Utils;
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
using CEZ.LoymarkTechTest.WebAPI.Infrastructure.Utils.Enums;

namespace CEZ.LoymarkTechTest.WebAPI
{
    public class GetHistoryPaginatedQuery
    {
        public class Query : IRequest<QueryResult>
        {
            [Required]
            public int Page { get; set; }
            [Required]
            public int PageSize { get; set; }
            public string? OrderBy { get; set; }
            public int? Direction { get; set; }
            public int? UserId { get; set; }
        }

        public class QueryResult
        {
            public PaginatedResult<HistoryDTO> Result { get; set; }
        }

        public class HistoryDTO
        {
            public int Id { get; set; }
            public string ChangeType { get; set; }
            public string? PrevValue { get; set; }
            public string? CurrValue { get; set; }
            public DateTime ChangeDate { get; set; }
            public int UserId { get; set; }
            public string UserFullName { get; set; }
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
                    var results = _db.Histories.Include(x => x.User).Where(x => true);
                    var changeTypes = _db.ChangeTypes.ToList();

                    if (request.UserId != null)
                    {
                        results = results.Where(x => x.UserId == request.UserId);
                    }

                    var TotalObjects = results.Count();
                    int TotalPages = ((TotalObjects - 1) / request.PageSize) + 1;

                    var orderBy = String.IsNullOrEmpty(request.OrderBy) ? "ChangeDate" : request.OrderBy;
                    var direction = request.Direction == -1 ? "DESC" : "ASC";
                    var resultsPaginated = await results.OrderBy(orderBy+" "+direction).PaginateAsync(request.Page, request.PageSize);

                    List<HistoryDTO> list = new List<HistoryDTO>();
                    foreach (var x in resultsPaginated.Result)
                    {
                        HistoryDTO user = new HistoryDTO
                        {
                            Id = x.Id,
                            ChangeType = changeTypes.Find(ct => ct.Id == x.ChangeType).Name,
                            PrevValue = x.PrevValue,
                            CurrValue = x.CurrValue,
                            ChangeDate = x.ChangeDate,
                            UserId = x.UserId,
                            UserFullName = x.User.Name+" "+x.User.Surname
                        };
                        list.Add(user);
                    }

                    return new QueryResult
                    {
                        Result = new PaginatedResult<HistoryDTO>
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
