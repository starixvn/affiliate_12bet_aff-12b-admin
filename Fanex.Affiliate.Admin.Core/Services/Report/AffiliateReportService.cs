using System;
using Fanex.Affiliate.Admin.Core.DTO.Report;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;
using Fanex.Affiliate.Admin.Core.Repository;

namespace Fanex.Affiliate.Admin.Core.Services.Report
{
    public class AffiliateReportService : IAffiliateReportService
    {
        private readonly IAffiliateReportRepository _affiliateReportRepository;

        public AffiliateReportService(IAffiliateReportRepository affiliateReportRepository)
        {
            _affiliateReportRepository = affiliateReportRepository;
        }

        public AffiliateReportMainEntityResult GetMainReportData(AffiliateReportMainParam param)
        {
            if (!IsValidParam(param))
            {
                return new AffiliateReportMainEntityResult();
            }

            return _affiliateReportRepository.GetMainReportData(param);
        }

        private static bool IsValidParam(AffiliateReportMainParam param)
        {
            return !string.IsNullOrEmpty(param.ProductIds) && IsValidMonthRange(param.FromDate, param.ToDate);
        }

        private static bool IsValidMonthRange(DateTime from, DateTime to)
        {
            var maxMonth = from.AddMonths(5);

            return DateTime.Compare(maxMonth, to) >= 0;
        }
    }
}
