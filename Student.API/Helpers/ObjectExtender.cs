using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Student.API.Helpers
{
    public static class ObjectExtender
    {
        public static Object ApplyFieldFiltering<T>(this T source, String fields)
        {
            if (String.IsNullOrWhiteSpace(fields))
                return source;

            var fieldList = fields.ToLower().Split(',').ToList();

            var expandoObject = new ExpandoObject();
            foreach (var field in fieldList)
            {
                var fieldValue = typeof(T)
                    .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                    .GetValue(source, null);
                ((IDictionary<String, Object>)expandoObject).Add(field, fieldValue);
            }

            return expandoObject;
        }
    }
}