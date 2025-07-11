using Fanex.Affiliate.Admin.Core.Criterias.Report;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Report;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;
using Fanex.Data.Repository;
using System;

namespace Fanex.Affiliate.Admin.Core.Services.Report
{
    public class AffiliateCpaReportService : BaseDynamicService, IAffiliateCpaReportService
    {
        private const int ValidMonthArrange = 5;
        private const int ValidDateArrange = 6;

        public AffiliateCpaReportService(IDynamicRepository dynamicRepository) : base(dynamicRepository) { }


        private static bool IsValidParam(AffiliateReportParam param)
        {
            return !string.IsNullOrEmpty(param.ProductIds) && IsValidDateRange(param.FromDate, param.ToDate, param.IsGetMonthly);
        }

        private static bool IsValidDateRange(DateTime from, DateTime to, bool isGetMonthly = true)
        {
            var maxMonth = from.AddMonths(isGetMonthly ? ValidMonthArrange : ValidDateArrange);

            return DateTime.Compare(maxMonth, to) >= 0;
        }

        public EntityResult<AffiliateCpaReport> GetData(AffiliateCpaRequest request)
        {
            var criteria = request.CreateCriteria();

            return FetchEntityResultHasItemTotal<AffiliateCpaReport>(criteria);
        }

        public EntityResult<AffiliateCpaReport> GetDetailData(AffiliateReportParam param)
        {
            throw new NotImplementedException();
        }
    }
}