using Fanex.Affiliate.User.Core.DTO.UserPanel.Statistics;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Statistics
{
    public class TrackingStatisticsViewModel
    {
        public IEnumerable<string> TrackingCodeList { get; set; }

        public IEnumerable<TrackingStatistics> TrackingStatisticsList { get; set; }

        public IEnumerable<SelectListItem> CountryList { get; set; }

        public bool IsDetail { get; set; }

        public string CurrentLanguage { get; set; }
    }
}
