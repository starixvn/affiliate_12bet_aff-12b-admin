namespace Fanex.Affiliate.Admin.Core.Services.Report
{
    using Criterias.Report;
    using Data.Repository;
    using DTO;
    using DTO.Report;
    using Extensions;
    using MetaModels.Report;
    using Utilities;

    public class RedepositReportService : BaseDynamicService, IRedepositReportService
    {
        public RedepositReportService(IDynamicRepository dynamicRepository) : base(dynamicRepository)
        {
        }

        public EntityResult<RedepositReport> GetReportData(RedepositReportParam param)
        {
            if (!DateTimeHelper.IsValidDateRange(param.FromDate, param.ToDate))
            {
                return new EntityResult<RedepositReport>();
            }

            var criteria = param.MapTo<RedepositReportParam, RedepositReportCriteria>();
            return FetchEntityResultHasItemTotal<RedepositReport>(criteria);
        }
    }
}
