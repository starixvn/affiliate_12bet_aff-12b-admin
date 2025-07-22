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
    }
}
