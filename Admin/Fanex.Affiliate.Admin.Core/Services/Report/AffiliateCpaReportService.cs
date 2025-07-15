namespace Fanex.Affiliate.Admin.Core.Services.Report
{
    using Fanex.Affiliate.Admin.Core.DTO;
    using Fanex.Affiliate.Admin.Core.DTO.Report;
    using Fanex.Affiliate.Admin.Core.MetaModels.Report;
    using Fanex.Data.Repository;

    public class AffiliateCpaReportService : BaseDynamicService, IAffiliateCpaReportService
    {
        public AffiliateCpaReportService(IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public EntityResult<AffiliateCpaReport> GetData(AffiliateCpaRequest request)
        {
            var criteria = request.CreateCriteria();

            return FetchEntityResultHasItemTotal<AffiliateCpaReport>(criteria);
        }
    }
}