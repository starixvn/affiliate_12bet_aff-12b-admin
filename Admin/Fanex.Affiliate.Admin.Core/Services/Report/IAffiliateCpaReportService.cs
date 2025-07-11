using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Report;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;

namespace Fanex.Affiliate.Admin.Core.Services.Report
{
    public interface IAffiliateCpaReportService
    {
        EntityResult<AffiliateCpaReport> GetData(AffiliateCpaRequest request);

        EntityResult<AffiliateCpaReport> GetDetailData(AffiliateReportParam request);
    }
}
