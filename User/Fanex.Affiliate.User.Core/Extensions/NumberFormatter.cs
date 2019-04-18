namespace Fanex.Affiliate.User.Core.Extensions
{
    using System;
    using System.Globalization;

    public static class NumberFormatter
    {
        private static readonly CultureInfo DefaultCulture = new CultureInfo("en-US");

        public static string FormatNumber(this int value, byte digit = 2)
        {
            return value.ToString("#,##0");
        }

        public static string ToStringMoneyWithSymbol(this decimal value, byte digit = 2, string symbol = "$")
        {
            return value < 0 ? $"-{symbol}{Math.Abs(value).FormatDecimal(digit)}" : $"{symbol}{value.FormatDecimal(digit)}";
        }

        public static string FormatDecimal(this decimal value, byte digit = 2)
        {
            var format = "N" + digit.ToString();
            return value.ToString(format, DefaultCulture.NumberFormat);
        }
    }
}