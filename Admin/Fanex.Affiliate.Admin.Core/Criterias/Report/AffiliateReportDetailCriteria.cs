namespace Fanex.Affiliate.Admin.Core.Criterias.Report
{
    public class AffiliateReportDetailCriteria : BaseAffiliateReportCriteria
    {
        const string SP_NAME = "Aff_Admin_AffiliateReport_Detail_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string EarningType { get; set; }

        public bool IsGetAll { get; set; }
    }
}
