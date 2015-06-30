using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;

namespace Student.API.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string sort)
        {
            if(source == null)
                throw new ArgumentNullException();

            if (sort == null)
                return source;

            var sortList = sort.Split(',');
            var completeSortExpression = String.Empty;
            foreach(var sortOption in sortList)
            {
                if (sortOption.StartsWith("-"))
                    completeSortExpression = completeSortExpression + sortOption.Remove(0, 1) + " descending,";
                else
                    completeSortExpression = completeSortExpression + sortOption + ",";
            }

            if (!String.IsNullOrWhiteSpace(completeSortExpression))
                source = source.OrderBy(completeSortExpression.Remove(completeSortExpression.Count() - 1));

            return source;
        }
    }
}