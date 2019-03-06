using System;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class LandingPageStatisticsParam
    {
        public int AffId { get; set; }

        public string SelectedPage { get; set; }

        public string LanguageCode { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int DeviceType { get; set; }
    }
}
