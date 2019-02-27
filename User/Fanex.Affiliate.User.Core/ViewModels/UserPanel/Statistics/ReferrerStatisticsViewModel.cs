using System;
using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO.UserPanel;

namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Statistics
{
    public class ReferrerStatisticsViewModel
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public IEnumerable<ReferrerStatistics> DataList { get; set; }

        public bool IsDetail { get; set; }

        public int RowNumber { get; set; }
    }
}
