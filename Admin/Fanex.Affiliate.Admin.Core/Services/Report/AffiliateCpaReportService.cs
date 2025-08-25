namespace Fanex.Affiliate.Admin.Core.Services.Report
{
    using Fanex.Affiliate.Admin.Core.DTO;
    using Fanex.Affiliate.Admin.Core.DTO.Report;
    using Fanex.Affiliate.Admin.Core.MetaModels.Report;
    using Fanex.Data.Repository;
    using Utilities;

    public class AffiliateCpaReportService : BaseDynamicService, IAffiliateCpaReportService
    {
        public AffiliateCpaReportService(IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public EntityResult<AffiliateCpaReport> GetData(AffiliateCpaRequest request)
        {
            if (!DateTimeHelper.IsValidDateRange(request.FromDate, request.ToDate))
            {
                return new EntityResult<AffiliateCpaReport>();
            }

            var criteria = request.CreateCriteria();
            return FetchEntityResultHasItemTotal<AffiliateCpaReport>(criteria);
        }
    }
}