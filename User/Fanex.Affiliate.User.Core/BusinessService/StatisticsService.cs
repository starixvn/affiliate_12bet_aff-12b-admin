namespace Fanex.Affiliate.User.Core.BusinessService
{
    using System.Collections.Generic;
    using CoreService;
    using Criterias;
    using Data.Repository;
    using DTO.UserPanel;
    using DTO.UserPanel.Statistics;
    using MetalModels.UserPanel;
    using Utilities;

    public class StatisticsService : BaseDynamicService, IStatisticsService
    {
        public StatisticsService(IDynamicRepository dynamicRepository) : base(dynamicRepository)
        {
        }

        public IEnumerable<LandingPageStatistics> GetLandingPageStatistics(LandingPageStatisticsParam landingPageStatisticsParam)
        {
            var criteria = landingPageStatisticsParam.MapTo<GetLandingPageStatisticsCriteria>();

            return FetchTData<LandingPageStatistics>(criteria);
        }

        public IEnumerable<ReferrerStatistics> GetReferrerStatistics(ReferrerStatisticsParam referrerStatisticsParam)
        {
            var criteria = referrerStatisticsParam.MapTo<GetReferrerStatisticsCriteria>();

            return FetchTData<ReferrerStatistics>(criteria);
        }

        public IEnumerable<ReferrerStatistics> GetStatisticsDetailBySite(StatisticsDetailBySiteParam statisticsDetailParam)
        {
            var criteria = statisticsDetailParam.MapTo<GetStatisticsDetailBySiteCriteria>();

            return FetchTData<ReferrerStatistics>(criteria);
        }

        public IEnumerable<MarketingPageStatistics> GetMarketingPageStatistics(MarketingPageStatisticsParam param)
        {
            var criteria = param.MapTo<GetMarketingPageStatisticsCriteria>();
           
            return FetchTData<MarketingPageStatistics>(criteria);
        }
    }
}
