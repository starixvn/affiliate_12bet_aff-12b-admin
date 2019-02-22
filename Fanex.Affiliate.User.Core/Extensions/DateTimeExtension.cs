using System;

namespace Fanex.Affiliate.User.Core.Extensions
{
    public static class DateTimeExtension
    {
        public static bool IsAfterBySecond(this DateTime dateTimeValue, long timeTickToCompare, int rangeTimeByMinute, bool isAllowEqual = true)
        {
            var targetTick = GetMinTimeTickBySecond(dateTimeValue, rangeTimeByMinute);
            var elapsedTick = targetTick - timeTickToCompare;

            return isAllowEqual ? elapsedTick >= 0 : elapsedTick > 0;
        }

        private static long GetMinTimeTickBySecond(DateTime dateTimeValue, int rangeTimeByMinute)
        {
            var minTime = dateTimeValue.AddMinutes(rangeTimeByMinute * -1);

            return new DateTime(minTime.Year, minTime.Month, minTime.Day, minTime.Hour, minTime.Minute, minTime.Second).Ticks;
        }
    }
}
