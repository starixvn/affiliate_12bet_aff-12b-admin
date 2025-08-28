namespace Fanex.Affiliate.Admin.Core.Criterias.Report
{
    public class RedepositReportCriteria : PagingByDateCriteria
    {
        public string UserName { get; set; }

        public string DealName { get; set; }

        public bool IsExportExcel { get; set; }

        public override string GetSettingKey() => "Aff_Admin_Report_RedepositCPA";

        public override bool IsValid() => true;
    }
}
