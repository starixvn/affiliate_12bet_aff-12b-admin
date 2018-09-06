using System;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Extensions
{
    public static class DataReaderExtension
    {
        public static bool HasColumn(this IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (string.Equals(reader.GetName(i), columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}