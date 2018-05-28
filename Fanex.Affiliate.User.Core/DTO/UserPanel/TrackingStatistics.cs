using System;

namespace Fanex.Affiliate.User.Core.DTO.UserPanel
{
    public class TrackingStatistics : StatisticsBase
    {
        public DateTime Date { get; set; }

        public string Month => Date.ToString("MMMM-yyyy");
    }
}
