using System;

namespace Fanex.Affiliate.Admin.Core.DTO.Report
{
    public class AffiliateReportDetail : AffiliateReportBase
    {
        public DateTime? MonthDate { get; set; }

        public string Description { get; set; }

        public string MonthDateString => MonthDate.HasValue ? MonthDate.Value.ToString("MM/yyyy") : string.Empty;
    }
}
