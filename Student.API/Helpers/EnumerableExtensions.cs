using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Web;
using Student.API.Models;

namespace Student.API.Helpers
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ApplySort<T>(this IEnumerable<T> source, String sort)
        {
            var queryable = source.AsQueryable();
            var sorted = queryable.ApplySort(sort);
            return sorted.ToList();
        }

        public static IEnumerable ApplyFieldFiltering<T>(this IEnumerable<T> source, String fields)
        {
            if (String.IsNullOrWhiteSpace(fields))
                return source;

            var results = new List<Object>();
            foreach (var record in source)
            {
                var obj = record.ApplyFieldFiltering(fields);
                results.Add(obj);
            }

            return results;
        }
    }
}