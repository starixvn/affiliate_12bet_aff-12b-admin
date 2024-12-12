namespace Fanex.Affiliate.User.Core.BusinessService
{
    using System.Collections.Generic;
    using DTO.UserPanel.Statistics;
    using MetalModels.UserPanel;
    using Repository.UserPanel;

    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public StatisticsService(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public IEnumerable<LandingPageStatistics> GetLandingPageStatistics(LandingPageStatisticsParam landingPageStatisticsParam)
        {
            return _statisticsRepository.GetLandingPageStatistics(landingPageStatisticsParam);
        }

        public IEnumerable<ReferrerStatistics> GetReferrerStatistics(ReferrerStatisticsParam referrerStatisticsParam)
        {
            return _statisticsRepository.GetReferrerStatistics(referrerStatisticsParam);
        }

        public IEnumerable<ReferrerStatistics> GetStatisticsDetailBySite(StatisticsDetailBySiteParam statisticsDetailParam)
        {
            return _statisticsRepository.GetStatisticsDetailBySite(statisticsDetailParam);
        }
    }
}
