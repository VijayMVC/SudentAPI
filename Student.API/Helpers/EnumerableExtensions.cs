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

            var fieldList = fields.ToLower().Split(',').ToList();
            var results = new List<Object>();

            foreach (var record in source)
            {
                var expandoObject = new ExpandoObject();
                foreach (var field in fieldList)
                {
                    var fieldValue = typeof(T)
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(record, null);
                    ((IDictionary<String, Object>)expandoObject).Add(field, fieldValue);
                }
                results.Add(expandoObject);
            }

            return results;
        }
    }
}