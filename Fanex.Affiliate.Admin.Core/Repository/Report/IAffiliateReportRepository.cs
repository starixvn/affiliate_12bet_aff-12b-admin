using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Report;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;

namespace Fanex.Affiliate.Admin.Core.Repository.Report
{
    public interface IAffiliateReportRepository
    {
        EntityResult<AffiliateReport> GetMainReportData(AffiliateReportMainParam param);

        EntityResult<AffiliateReport> GetDetailReportData(AffiliateReportMainParam param);

        IList<string> SearchAffiliateList(string userName);
    }
}