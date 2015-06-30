using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Student.API.Helpers
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> ApplySort<T>(this IEnumerable<T> source, String sort)
        {
            var queryable = source.AsQueryable();
            var sorted = queryable.ApplySort(sort);
            return sorted.ToList();
        }
    }
}