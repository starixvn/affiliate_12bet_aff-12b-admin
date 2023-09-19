using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.DTO.UserPanel.Statistics;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Statistics
{
    public class LandingPageStatisticsViewModel
    {
        public IEnumerable<SelectListItem> LanguageList { get; set; }

        public IEnumerable<LandingPage> MemberSitePageList { get; set; }

        public IEnumerable<SelectListItem> CountryList { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public IEnumerable<LandingPageStatistics> DataList { get; set; }

        public int RowNumber { get; set; }

        public string ReportBy { get; set; }

        public bool IsDetail { get; set; }
    }
}
