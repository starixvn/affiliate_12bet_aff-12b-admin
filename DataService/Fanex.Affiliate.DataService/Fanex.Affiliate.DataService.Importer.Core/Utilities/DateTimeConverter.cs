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

        public static long GetMilliseconds(DateTime utcValue, string timeZoneId = TimeZoneId.GmtPlus8)
        {
            //var toGMT = ToGMT(utcValue, timeZoneId);

            return new DateTimeOffset(utcValue).ToUnixTimeMilliseconds();
        }
    }
}
