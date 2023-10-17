using Fanex.Affiliate.User.Core.DTO.UserPanel.Statistics;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Statistics
{
    public class ReferrerStatisticsViewModel
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public IEnumerable<ReferrerStatistics> DataList { get; set; }

        public IEnumerable<SelectListItem> CountryList { get; set; }

        public bool IsDetail { get; set; }

        public int RowNumber { get; set; }
    }
}
