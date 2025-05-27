namespace Fanex.Affiliate.User.Core.Utilities
{
    using System;

    public static class DateTimeHelper
    {
        private const int GMT8 = 8;

        public static TimeSpan Gmt8TimeSpan => new TimeSpan(GMT8, 0, 0);

        public static Func<DateTimeOffset> DateTimeOffsetProvider { get; set; } = () => DateTimeOffset.Now;

        private static DateTimeOffset ServerTimeOffset => DateTimeOffsetProvider();

        public static DateTime ServerTime => ServerTimeOffset.DateTime;

        public static DateTime Gmt8Now => ServerTimeOffset.ToOffset(Gmt8TimeSpan).DateTime;

        public static DateTime UtcNow => ServerTimeOffset.UtcDateTime;

        public static int Gmt8CurrentYear => Gmt8Now.Year;

        public static DateTime CreateDateTime(int year, int month, int day, int hour = 0, int minute = 0, int second = 0)
            => new DateTimeOffset(year, month, day, hour, minute, second, TimeSpan.Zero).DateTime;
    }
}
