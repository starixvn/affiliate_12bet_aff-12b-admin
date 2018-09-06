using Fanex.Affiliate.Admin.Core.DTO.Report;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;

namespace Fanex.Affiliate.Admin.Core.Services.Report
{
    public interface IAffiliateReportService
    {
        AffiliateReportMainEntityResult GetMainReportData(AffiliateReportMainParam param);
    }
}
