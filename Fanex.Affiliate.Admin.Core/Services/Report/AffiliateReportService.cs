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

            var mainResult = _affiliateReportRepository.GetMainReportData(param);
            foreach (var item in mainResult.MainData)
            {
                item.CommissionRate = decimal.Round(item.CommissionRate * 100, 2);
            }

            if (mainResult.Total != null)
            {
                mainResult.Total.CommissionRate = decimal.Round(mainResult.Total.CommissionRate * 100, 2);
            }

            return mainResult;
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