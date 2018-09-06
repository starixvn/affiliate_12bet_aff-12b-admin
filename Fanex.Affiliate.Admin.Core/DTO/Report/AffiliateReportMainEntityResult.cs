using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.DTO.Report
{
    public class AffiliateReportMainEntityResult
    {
        public IEnumerable<AffiliateReportMain> MainData { get; set; }

        public AffiliateReportMain Total { get; set; }

        public int TotalItem { get; set; }
    }
}
