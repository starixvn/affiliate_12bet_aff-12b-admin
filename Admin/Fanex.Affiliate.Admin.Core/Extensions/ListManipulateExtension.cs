namespace Fanex.Affiliate.Admin.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ListManipulateExtension
    {
        public static IEnumerable<T> SortBy<T>(this IEnumerable<T> list, string columnName, bool isAscending = true)
        {
            var type = typeof(T);
            var property = type.GetProperty(columnName);
            if (property == null)
            {
                throw new ArgumentException($"{type.Name} doesn't contain property {columnName}");
            }

            return isAscending ? list.OrderBy(x => x.GetType().GetProperty(columnName).GetValue(x)) : list.OrderByDescending(x => x.GetType().GetProperty(columnName).GetValue(x));
        }

        public static IEnumerable<T> Paging<T>(this IEnumerable<T> list, int pageIndex, int pageSize)
        {
            return list.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Sorts a list by multiple columns with specified sort directions
        /// </summary>
        /// <typeparam name="T">Type of the list elements</typeparam>
        /// <param name="list">The source list to sort</param>
        /// <param name="sortColumns">Dictionary of column names and their sort directions (true for ascending, false for descending)</param>
        /// <returns>Sorted enumerable</returns>
        public static IEnumerable<T> SortBy<T>(this IEnumerable<T> list, IDictionary<string, bool> sortColumns)
        {
            if (sortColumns == null || !sortColumns.Any())
            {
                return list;
            }

            var type = typeof(T);
            foreach (var column in sortColumns.Keys)
            {
                var property = type.GetProperty(column);
                if (property == null)
                {
                    throw new ArgumentException($"{type.Name} doesn't contain property {column}");
                }
            }

            IOrderedEnumerable<T> orderedList = null;
            var isFirst = true;

            foreach (var sortColumn in sortColumns)
            {
                if (isFirst)
                {
                    orderedList = sortColumn.Value 
                        ? list.OrderBy(x => x.GetType().GetProperty(sortColumn.Key).GetValue(x))
                        : list.OrderByDescending(x => x.GetType().GetProperty(sortColumn.Key).GetValue(x));
                    isFirst = false;
                }
                else
                {
                    orderedList = sortColumn.Value
                        ? orderedList.ThenBy(x => x.GetType().GetProperty(sortColumn.Key).GetValue(x))
                        : orderedList.ThenByDescending(x => x.GetType().GetProperty(sortColumn.Key).GetValue(x));
                }
            }

            return orderedList ?? list;
        }
    }
}
