namespace Fanex.Affiliate.Admin.Core.Criterias.UserPanel.MarketingStatistics
{
    using Data.Repository;
    using System;

    public class MarketingStatisticsCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string LanguageCodes { get; set; }

        public bool IncludedInActive { get; set; }

        public override string GetSettingKey() => "Aff_User_Rpt_MarketingPage_Statistics";

        public override bool IsValid() => AffId > 0;
    }
}
