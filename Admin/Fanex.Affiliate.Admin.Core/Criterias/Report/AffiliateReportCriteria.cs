namespace Fanex.Affiliate.Admin.Core.Criterias.Report
{
    public class AffiliateReportCriteria : BaseAffiliateReportCriteria
    {
        const string SP_NAME = "Aff_Admin_AffiliateReport_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string GroupIds { get; set; }

        public string PreferredLanguage { get; set; }

        public string PaymentCurrencies { get; set; }

        public bool IsExcludeNegvBBF { get; set; }

        public bool IsExcludeNegvCurrentBBF { get; set; }

        public bool IsGetMonthly { get; set; }

        public string Statuses { get; set; }
    }
}
