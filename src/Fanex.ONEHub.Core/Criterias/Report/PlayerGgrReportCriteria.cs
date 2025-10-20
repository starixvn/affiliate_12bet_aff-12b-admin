namespace Fanex.Affiliate.Admin.Core.Criterias.Report
{
    public class PlayerGgrReportCriteria : PagingByDateCriteria
    {
        const string SP_NAME = "Aff_Admin_PlayerReport_GGR";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string AffUsername { get; set; }

        public string Username { get; set; }

        public string ProductIds { get; set; }

        public bool IsExportExcel { get; set; }

        public int BaseCurrency { get; set; }
    }
}
