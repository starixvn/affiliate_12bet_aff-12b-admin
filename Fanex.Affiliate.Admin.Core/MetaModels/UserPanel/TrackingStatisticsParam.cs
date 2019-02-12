using System;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class TrackingStatisticsParam
    {
        public int AffId { get; set; }

        public string AffName { get; set; }

        public string TrackingCode { get; set; }

        public int Month { get; set; } = DateTime.Now.Month;

        public int Year { get; set; } = DateTime.Now.Year;
    }
}
