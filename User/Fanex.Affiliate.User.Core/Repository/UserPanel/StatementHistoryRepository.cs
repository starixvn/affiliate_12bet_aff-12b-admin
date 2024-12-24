namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    using System.Collections.Generic;
    using Criterias;
    using Data.Repository;
    using DTO;
    using Extensions.Mappers;
    using Fanex.Affiliate.User.Core.DTO.UserPanel;
    using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
    using Fanex.Common.Extensions;

    public class StatementHistoryRepository : IStatementHistoryRepository
    {
        private readonly IDynamicRepository _repository;

        public StatementHistoryRepository(IDynamicRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<StatementHistory> GetStatementHistory(StatementHistoryParam statementHistoryParam)
        {
            var criteria = new GetStatementHistoryCriteria();
            criteria.Map(statementHistoryParam);

            return _repository.Fetch<StatementHistory>(criteria);
        }

        public IEnumerable<ProductItem> GetProductList(bool isInternal)
        {
            return _repository.Fetch<ProductItem>(new GetProductListCriteria(isInternal));
        }

        public DailyEarningResult GetDailyEarning(DailyEarningParam dailyEarningParam)
        {
            var criteria = new GetDailyEarningCriteria();
            criteria.Map(dailyEarningParam);
            var dailyEarningResult = new DailyEarningResult();

            var (data, total) = _repository.FetchMultiple<DailyEarning, DailyEarning>(criteria);

            dailyEarningResult.MainDataList = data;
            dailyEarningResult.Total = total.GetFirstOrDefault();

            return dailyEarningResult;
        }

        public IEnumerable<SubAffComInfo> GetSubAffCommission(SubAffComParam subAffParam)
        {
            var criteria = new GetSubAffCommissionCriteria();
            criteria.Map(subAffParam);

            return _repository.Fetch<SubAffComInfo>(criteria);
        }
    }
}