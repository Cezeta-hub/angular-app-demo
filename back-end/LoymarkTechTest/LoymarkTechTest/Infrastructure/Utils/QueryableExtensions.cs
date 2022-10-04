using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CEZ.LoymarkTechTest.WebAPI.Infrastructure.Utils
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedResult<T>> PaginateAsync<T>(this IQueryable<T> collection, int page, int pageSize, int? seleccionableCount = null)
        {
            var count = await collection.CountAsync();

            List<T> result = new List<T>();

            if (count != 0)
                result = await collection.Skip(page * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResult<T>()
            {
                CurrentPage = page,
                Result = result,
                TotalObjects = count,
                TotalPages = (int)Math.Ceiling((decimal)count / (decimal)pageSize)
            };
        }
    }
}
