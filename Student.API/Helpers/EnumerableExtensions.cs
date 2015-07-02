using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using System.Web;
using Student.API.Models;

namespace Student.API.Helpers
{
    public static class EnumerableExtensions
    {
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

        public static IEnumerable<T> ApplySort<T>(this IEnumerable<T> source, String sort)
        {
            var sortList = sort.ToLower().Split(',').ToList();
            var collections = sortList.Where(s => s.Contains(".")).ToList();
            var properties = sortList.Where(s => !s.Contains(".")).ToList();

            var results = source.AsQueryable()
                .ApplySort(String.Join(",", properties))
                .ToList();

            if (!collections.Any())
                return results;

            var propertyDictionary = new Dictionary<String, List<String>>();
            foreach (var field in collections)
            {
                var fieldArray = field.Split('.');
                if (fieldArray[0].StartsWith("-"))
                {
                    fieldArray[0] = fieldArray[0].Remove(0, 1);
                    fieldArray[1] = "-" + fieldArray[1];
                }

                if (propertyDictionary.ContainsKey(fieldArray[0]))
                    propertyDictionary[fieldArray[0]].Add(fieldArray[1]);
                else
                    propertyDictionary.Add(fieldArray[0], new List<string>() { fieldArray[1] });
            }

            foreach (var propertyCollection in propertyDictionary)
            {
                foreach (var value in results)
                {
                    var listValues = (IEnumerable)typeof(T)
                        .GetProperty(propertyCollection.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(value, null);

                    if(listValues == null || !listValues.Any())
                        continue;

                    var enumerator = listValues.GetEnumerator();
                    enumerator.MoveNext();
                    var first = enumerator.Current;
                    var type = first.GetType();

                    var method = typeof(EnumerableExtensions).GetMethod("ApplySort");
                    var generic = method.MakeGenericMethod(type);
                    var values = generic.Invoke(listValues, new object[] { listValues, String.Join(",", propertyCollection.Value) });
                    
                    typeof(T)
                        .GetProperty(propertyCollection.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .SetValue(value, values);
                }
            }

            return results;
        }
    }
}