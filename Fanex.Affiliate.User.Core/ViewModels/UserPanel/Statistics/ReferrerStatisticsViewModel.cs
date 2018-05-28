using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO.UserPanel;

namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Statistics
{
    public class ReferrerStatisticsViewModel
    {
        public IEnumerable<ReferrerStatistics> DataList { get; set; }

        public bool IsDetail { get; set; }

        public int TotalPages { get; set; }

        public int RowNumber { get; set; }
    }
}
