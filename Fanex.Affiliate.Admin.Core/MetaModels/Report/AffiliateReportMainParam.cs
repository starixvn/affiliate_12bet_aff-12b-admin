using System;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Report
{
    public class AffiliateReportMainParam
    {
        public string UserName { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string PreferredLanguage { get; set; }

        public bool IsGetAll { get; set; }

        public string ProductIds { get; set; }

        public byte SortType { get; set; }

        public byte SortDirectionType { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public bool IsExportExcel { get; set; }
    }
}
