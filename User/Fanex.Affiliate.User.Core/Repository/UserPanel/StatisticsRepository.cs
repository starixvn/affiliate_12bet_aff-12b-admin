namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    using System.Collections.Generic;
    using Criterias;
    using Data.Repository;
    using Extensions.Mappers;
    using Fanex.Affiliate.User.Core.DTO.UserPanel;
    using Fanex.Affiliate.User.Core.DTO.UserPanel.Statistics;
    using Fanex.Affiliate.User.Core.MetalModels.UserPanel;

    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly IDynamicRepository _repository;

        public StatisticsRepository(IDynamicRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ReferrerStatistics> GetReferrerStatistics(ReferrerStatisticsParam referrerStatisticsParam)
        {
            var criteria = new GetReferrerStatisticsCriteria();
            criteria.Map(referrerStatisticsParam);

            return _repository.Fetch<ReferrerStatistics>(criteria);
        }

        public IEnumerable<ReferrerStatistics> GetStatisticsDetailBySite(StatisticsDetailBySiteParam statisticsDetailParam)
        {
            var criteria = new GetStatisticsDetailBySiteCriteria();
            criteria.Map(statisticsDetailParam);

            return _repository.Fetch<ReferrerStatistics>(criteria);
        }

        public IEnumerable<LandingPageStatistics> GetLandingPageStatistics(LandingPageStatisticsParam landingPageStatisticsParam)
        {
            var criteria = new GetLandingPageStatisticsCriteria();
            criteria.Map(landingPageStatisticsParam);

            return _repository.Fetch<LandingPageStatistics>(criteria);
        }

        public IEnumerable<MarketingPageStatistics> GetMarketingPageStatistics(MarketingPageStatisticsParam param)
        {
            var criteria = new GetMarketingPageStatisticsCriteria();
            criteria.Map(param);

            return _repository.Fetch<MarketingPageStatistics>(criteria);
        }
    }
}