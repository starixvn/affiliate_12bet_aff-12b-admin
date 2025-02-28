namespace Fanex.Affiliate.User.Core.BusinessService
{
    using CoreService;
    using Criterias;
    using Data.Repository;
    using DTO.UserPanel;
    using DTO.UserPanel.Statistics;
    using MetalModels.UserPanel;
    using System.Collections.Generic;
    using Utilities;

    public class StatisticsService : BaseDynamicService, IStatisticsService
    {
        public StatisticsService(IDynamicRepository dynamicRepository) : base(dynamicRepository)
        {
        }

        public IEnumerable<LandingPageStatistics> GetLandingPageStatistics(LandingPageStatisticsParam landingPageStatisticsParam)
        {
            var criteria = landingPageStatisticsParam.MapTo<LandingPageStatisticsParam, GetLandingPageStatisticsCriteria>();

            return FetchTData<LandingPageStatistics>(criteria);
        }

        public IEnumerable<ReferrerStatistics> GetReferrerStatistics(ReferrerStatisticsParam referrerStatisticsParam)
        {
            var criteria = referrerStatisticsParam.MapTo<ReferrerStatisticsParam, GetReferrerStatisticsCriteria>();

            return FetchTData<ReferrerStatistics>(criteria);
        }

        public IEnumerable<ReferrerStatistics> GetStatisticsDetailBySite(StatisticsDetailBySiteParam statisticsDetailParam)
        {
            var criteria = statisticsDetailParam.MapTo<StatisticsDetailBySiteParam, GetStatisticsDetailBySiteCriteria>();

            return FetchTDataWithPaging<ReferrerStatistics>(criteria, statisticsDetailParam);
        }

        public IEnumerable<MarketingPageStatistics> GetMarketingPageStatistics(MarketingPageStatisticsParam param)
        {
            var criteria = param.MapTo<MarketingPageStatisticsParam, GetMarketingPageStatisticsCriteria>();

            return FetchTData<MarketingPageStatistics>(criteria);
        }
    }
}
