namespace Fanex.Affiliate.User.Core.BusinessService
{
    using System.Collections.Generic;
    using CoreService;
    using Criterias;
    using Data.Repository;
    using DTO;
    using DTO.UserPanel;
    using MetalModels.UserPanel;
    using Utilities;

    public class StatementHistoryService : BaseDynamicService, IStatementHistoryService
    {
        public StatementHistoryService(IDynamicRepository dynamicRepository) : base(dynamicRepository)
        {
        }

        public IEnumerable<StatementHistory> GetStatementHistory(StatementHistoryParam statementHistoryParam)
        {
            var criteria = statementHistoryParam.MapTo<GetStatementHistoryCriteria>();

            return FetchTDataWithPaging<StatementHistory>(criteria, statementHistoryParam);
        }

        public IEnumerable<ProductItem> GetProductList(bool isInternal)
        {
            var criteria = new GetProductListCriteria(isInternal);

            return FetchTData<ProductItem>(criteria);
        }

        public EntityResult<DailyEarning> GetDailyEarning(DailyEarningParam dailyEarningParam)
        {
            var criteria = dailyEarningParam.MapTo<GetDailyEarningCriteria>();

            return FetchEntityResult<DailyEarning>(criteria);
        }

        public IEnumerable<SubAffComInfo> GetSubAffCommission(SubAffComParam subAffParam)
        {
            var criteria = subAffParam.MapTo<GetSubAffCommissionCriteria>();

            return FetchTData<SubAffComInfo>(criteria);
        }
    }
}
