using Fanex.Data.Repository;
using System;

namespace Fanex.Affiliate.Admin.Core.Criterias.UserPanel.LandingPageStatistics
{
    public abstract class BaseLandingPageStatisticsCritera : CriteriaBase
    {
        public int AffId { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string SportCode { get; set; }

        public string LanguageCode { get; set; }

        public short DeviceType { get; set; }
    }
}
