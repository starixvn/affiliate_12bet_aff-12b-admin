using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Report;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public interface IAffiliateReportRepository
    {
        AffiliateReportMainEntityResult GetMainReportData(AffiliateReportMainParam param);

        EntityResult<AffiliateReportDetail> GetDetailReportData(AffiliateReportMainParam param);
    }
}
