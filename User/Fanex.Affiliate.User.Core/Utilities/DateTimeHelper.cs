namespace Fanex.Affiliate.User.Core.Utilities
{
    using System;

    public static class DateTimeHelper
    {
        public static Func<DateTimeOffset> DateTimeOffsetProvider { get; set; } = () => DateTimeOffset.Now;

        public static DateTime Now => DateTimeOffsetProvider().DateTime;

        public static DateTime UtcNow => DateTimeOffsetProvider().UtcDateTime;

        public static DateTime Today => Now.Date;

        public static int CurrentYear => Now.Year;

        public static DateTime MinValue => DateTimeOffset.MinValue.DateTime;

        public static DateTime MaxValue => DateTimeOffset.MaxValue.DateTime;

        public static DateTime CreateDateTime(int year, int month, int day, int hour = 0, int minute = 0, int second = 0)
            => new DateTimeOffset(year, month, day, hour, minute, second, TimeSpan.Zero).DateTime;
    }
}
