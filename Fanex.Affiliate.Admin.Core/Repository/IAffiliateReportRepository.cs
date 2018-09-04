using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Report;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public interface IAffiliateReportRepository
    {
        IEnumerable<AffiliateReportMain> GetMainReportData(AffiliateReportMainParam param);
    }
}
