using System;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class TrackingStatisticsDetailParam : TrackingStatisticsParam
    {
        public int Month { get; set; } = DateTime.Now.Month;

        public int Year { get; set; } = DateTime.Now.Year;
    }
}
