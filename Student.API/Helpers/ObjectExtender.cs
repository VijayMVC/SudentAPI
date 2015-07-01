using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web;

namespace Student.API.Helpers
{
    public static class ObjectExtender
    {
        public static Object ApplyFieldFiltering<T>(this T source, String fields)
        {
            if (String.IsNullOrWhiteSpace(fields))
                return source;

            var expandoObject = new ExpandoObject();
            var fieldList = fields.ToLower().Split(',').ToList();
            var collections = fieldList.Where(s => s.Contains(".")).ToList();
            var properties = fieldList.Where(s => !s.Contains(".")).ToList();

            foreach (var field in properties)
            {
                var fieldValue = typeof(T)
                    .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                    .GetValue(source, null);
                ((IDictionary<String, Object>)expandoObject).Add(field, fieldValue);
            }

            if (!collections.Any())
                return expandoObject;

            var propertyDictionary = new Dictionary<String, List<String>>();
            foreach (var field in collections)
            {
                var fieldArray = field.Split('.');
                if (propertyDictionary.ContainsKey(fieldArray[0]))
                    propertyDictionary[fieldArray[0]].Add(fieldArray[1]);
                else
                    propertyDictionary.Add(fieldArray[0], new List<string>() {fieldArray[1]});
            }

            foreach (var propertyCollection in propertyDictionary)
            {
                var listValues = (IEnumerable)typeof(T)
                    .GetProperty(propertyCollection.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                    .GetValue(source, null);

                var list = new List<Object>();
                foreach (var listValue in listValues)
                {
                    var type = listValue.GetType();
                    var castedObject = Convert.ChangeType(listValue, type);

                    var method = typeof(ObjectExtender).GetMethod("ApplyFieldFiltering");
                    var generic = method.MakeGenericMethod(type);
                    var value = generic.Invoke(castedObject, new object[] { castedObject, String.Join(",", propertyCollection.Value) });

                    list.Add(value);
                }

                ((IDictionary<String, Object>)expandoObject).Add(propertyCollection.Key, list);
            }

            return expandoObject;
        }
    }
}