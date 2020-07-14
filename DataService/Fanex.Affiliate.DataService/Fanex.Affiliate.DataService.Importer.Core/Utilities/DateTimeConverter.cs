using Fanex.Affiliate.DataService.Importer.Core.Constants;
using System;

namespace Fanex.Affiliate.DataService.Importer.Core.Utilities
{
    public static class DateTimeConverter
    {
        public static DateTime ToGMT(DateTime utcValue, string timeZoneId)
        {
            var utcDate = DateTime.SpecifyKind(utcValue, DateTimeKind.Utc);
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(utcDate, timeZoneInfo);
        }

        public static DateTime ConvertTime(DateTime value, string fromTimeZoneId, string toTimeZoneId)
        {
            var dateValue = DateTime.SpecifyKind(value, DateTimeKind.Unspecified);
            var fromTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(fromTimeZoneId);
            var toTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(toTimeZoneId);

            return TimeZoneInfo.ConvertTime(dateValue, fromTimeZoneInfo, toTimeZoneInfo);
        }

        public static long GetUnixTimeMilisecond(DateTime value, string timeZoneId = TimeZoneId.GmtPlus8)
        {
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            TimeSpan offset = timeZone.GetUtcOffset(value);

            var timeOffSet = new DateTimeOffset(value.Ticks, offset);
            return timeOffSet.ToUnixTimeMilliseconds();
        }
    }
}
