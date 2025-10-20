namespace Fanex.Affiliate.Admin.Core.Criterias.Report
{
    public abstract class BaseAffiliateReportCriteria : PagingByDateCriteria
    {
        public string Username { get; set; }

        public string ProductIds { get; set; }

        public byte SortType { get; set; }

        public byte SortDirectionType { get; set; }

        public bool IsExportExcel { get; set; }

        public int BaseCurrency { get; set; }

        public string Currencies { get; set; }
    }
}
