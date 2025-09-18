namespace Fanex.Affiliate.Admin.Core.Criterias.Report
{
    using System;

    public class AffiliateCpaReportCriteria : PagingCriteria
    {
        private const int RangeMonth = 6;
        private const int Twelve = 12;

        public string Username { get; set; }

        public string DealName { get; set; }

        public short ViewBy { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public short SortType { get; set; }

        public bool SortDirectionType { get; set; }

        public bool IsExportExcel { get; set; }

        public override string GetSettingKey() => "Aff_Admin_Report_AffiliateCPA";

        public override bool IsValid()
        {
            return (ToDate.Year - FromDate.Year) * Twelve + ToDate.Month - FromDate.Month <= RangeMonth;
        }
    }
}
