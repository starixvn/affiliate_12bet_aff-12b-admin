using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Report;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;

namespace Fanex.Affiliate.Admin.Core.Services.Report
{
    public interface IAffiliateReportService
    {
        EntityResult<AffiliateReport> GetMainReportData(AffiliateReportMainParam param);

        EntityResult<AffiliateReport> GetDetailReportData(AffiliateReportMainParam param);

        IList<string> SearchAffiliateList(string userName);
    }
}
