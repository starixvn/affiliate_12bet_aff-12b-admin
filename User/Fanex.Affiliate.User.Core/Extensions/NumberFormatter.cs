namespace Fanex.Affiliate.User.Core.Extensions
{
    using System;
    using System.Globalization;

    public static class NumberFormatter
    {
        private static readonly CultureInfo DefaultCulture = new CultureInfo("en-US");

        public static string ToStringNumber(this object value, byte digit = 2)
        {
            if (value is decimal)
            {
                var format = "N" + digit.ToString();
                return ((decimal)value).ToString(format, DefaultCulture.NumberFormat);
            }

            if (value is int)
            {
                return ((int)value).ToString("#,##0");
            }

            return value.ToString();
        }

        public static string ToStringMoneyWithSymbol(this decimal value, byte digit = 2, string symbol = "$")
        {
            return value < 0 ? $"-{symbol}{DecimalFormat(Math.Abs(value), digit)}" : $"{symbol}{DecimalFormat(value, digit)}";
        }

        public static string DecimalFormat(decimal value, byte digit = 2)
        {
            var format = "N" + digit.ToString();
            return value.ToString(format, DefaultCulture.NumberFormat);
        }
    }
}