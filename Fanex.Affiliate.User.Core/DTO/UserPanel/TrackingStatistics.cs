using System;

namespace Fanex.Affiliate.User.Core.DTO.UserPanel
{
    public class TrackingStatistics
    {
        public DateTime Date { get; set; }

        public int TotalClick { get; set; }

        public int UniqueClick { get; set; }

        public string Month => Date.ToString("MMMM-yyyy");
    }
}
