using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CEZ.LoymarkTechTest.WebAPI.Infrastructure.Utils
{
    public class PaginatedResult<T>
    {
        public int TotalObjects { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public List<T> Result { get; set; }

        public PaginatedResult<RT> Map<RT>(List<RT> mappedList)
        {
            PaginatedResult<RT> result = new PaginatedResult<RT>();

            result.CurrentPage = CurrentPage;
            result.TotalObjects = TotalObjects;
            result.TotalPages = TotalPages;
            result.Result = mappedList;

            return result;
        }
    }
}
