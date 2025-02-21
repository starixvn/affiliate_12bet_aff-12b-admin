namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Statistics
{
    using System;

    public class MarketingPageStatisticsViewModel
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string Language { get; set; }

        public bool IncludeInActiveLink { get; set; }
    }
}
