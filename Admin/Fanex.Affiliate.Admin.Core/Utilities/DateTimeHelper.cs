using System;

namespace Fanex.Affiliate.Admin.Core.Utilities
{
    public static class DateTimeHelper
    {
        private const int GMT8 = 8;

        public static bool IsValidDateRange(DateTime from, DateTime to, int monthRange = 6)
        {
            var maxMonth = from.AddMonths(monthRange);

            return DateTime.Compare(maxMonth, to) >= 0;
        }

        public static bool IsValidDateTime(DateTime from, DateTime to)
        {
            return DateTime.Compare(from, to) <= 0;
        }

        public static TimeSpan TimeSpanGMT8 => new TimeSpan(GMT8, 0, 0);

        public static DateTime DateTimeGMT8 => DateTimeOffset.Now.ToOffset(TimeSpanGMT8).DateTime;

        public static string DateTimeGMT8String => DateTimeGMT8.ToString("MM/dd/yyyy");

        public static DateTimeOffset NowDateTimeOffset => DateTimeOffset.Now;

        public static DateTimeOffset UtcNowDateTimeOffset => DateTimeOffset.UtcNow;

        public static DateTime CreateDateTime(int year, int month, int day, int hour = 0, int minute = 0, int second = 0)
            => new DateTimeOffset(year, month, day, hour, minute, second, TimeSpan.Zero).DateTime;
    }

}
