using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO.UserPanel;

namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Statistics
{
    public class TrackingStatisticsViewModel
    {
        public IEnumerable<string> TrackingCodeList { get; set; }

        public IEnumerable<TrackingStatistics> TrackingStatisticsList { get; set; }

        public bool IsDetail { get; set; }

        public string CurrentLanguage { get; set; }
    }
}
