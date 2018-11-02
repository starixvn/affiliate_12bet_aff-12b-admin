using System;

namespace Fanex.Affiliate.DataService.Importer.Utilities
{
    public static class DateTimeConverter
    {
        public static DateTime ToGMT4(DateTime utcValue, string timeZoneId)
        {
            var utcDate = DateTime.SpecifyKind(utcValue, DateTimeKind.Utc);
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(utcDate, timeZoneInfo);
        }
    }
}
