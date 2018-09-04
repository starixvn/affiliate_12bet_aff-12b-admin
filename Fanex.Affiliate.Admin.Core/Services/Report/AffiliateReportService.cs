using Fanex.Affiliate.Admin.Core.MetaModels.Report;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.ViewModels;

namespace Fanex.Affiliate.Admin.Core.Services.Report
{
    public class AffiliateReportService : IAffiliateReportService
    {
        private readonly IAffiliateReportRepository _affiliateReportRepository;

        public AffiliateReportService(IAffiliateReportRepository affiliateReportRepository)
        {
            _affiliateReportRepository = affiliateReportRepository;
        }

        public AffiliateReportMainDataViewModel GetMainReportData(AffiliateReportMainParam param)
        {
            var dataList = _affiliateReportRepository.GetMainReportData(param);

            return new AffiliateReportMainDataViewModel
            {
                MainData = dataList,
                TotalRow = param.Nrows
            };
        }
    }
}
