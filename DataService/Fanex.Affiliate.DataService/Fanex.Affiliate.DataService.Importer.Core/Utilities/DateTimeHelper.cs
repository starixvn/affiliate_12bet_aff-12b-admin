using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.DataService.Importer.Core.Utilities
{
    public static class DateTimeHelper
    {
        public static IEnumerable<Tuple<DateTime, DateTime>> GetProportionTimeRange(DateTime startTime, DateTime endTime, TimeSpan span)
        {
            var startRequestTime = startTime;
            var endRequestTime = GetSmallerTime(startRequestTime.Add(span), endTime);
            while (startRequestTime < endRequestTime)
            {
                yield return new Tuple<DateTime, DateTime>(startRequestTime, endRequestTime);
                startRequestTime = endRequestTime;
                endRequestTime = GetSmallerTime(endRequestTime.Add(span), endTime);
            }
        }

        public static DateTime GetSmallerTime(DateTime compareTime, DateTime endTime)
        {
            return DateTime.Compare(compareTime, endTime) > 0 ? endTime : compareTime;
        }
    }
}
