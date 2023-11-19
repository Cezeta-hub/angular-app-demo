using CEZ.AngularDemo.WebAPI.Infrastructure.Persistence.Entities;
using FluentValidation;
using MediatR;

namespace CEZ.AngularDemo.WebAPI
{
    public class GetCountriesQuery
    {
        public class Query : IRequest<QueryResult>
        {
        }

        public class QueryResult
        {
            public List<CountryDTO> Result { get; set; }
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
            public Validator(Context db) { }

            public class Handler : IRequestHandler<Query, QueryResult>
            {
                private Context _db;

                public Handler(Context db)
                {
                    _db = db;
                }

                public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
                {
                    var results = _db.Countries.ToList();
                    List<CountryDTO> list = new List<CountryDTO>();
                    foreach (var c in results)
                    {
                        list.Add( new CountryDTO{ Id = c.Id, Name = c.Name, Code = c.Code, Value = c.Value });
                    }

                    return new QueryResult
                    {
                        Result = list
                    };
                }
            }
        }
        
    }
}
