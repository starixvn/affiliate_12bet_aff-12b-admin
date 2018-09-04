using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Report;

namespace Fanex.Affiliate.Admin.Core.ViewModels
{
    public class AffiliateReportMainDataViewModel
    {
        public IEnumerable<AffiliateReportMain> MainData { get; set; }

        public int TotalRow { get; set; }
    }
}
