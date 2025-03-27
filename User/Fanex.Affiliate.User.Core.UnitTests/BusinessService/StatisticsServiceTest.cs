namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    using Criterias;
    using Data.Repository;
    using DTO.UserPanel;
    using DTO.UserPanel.Statistics;
    using Fanex.Affiliate.User.Core.BusinessService;
    using MetalModels.UserPanel;
    using Newtonsoft.Json;
    using NSubstitute;
    using System.Collections.Generic;
    using Xunit;

    public class StatisticsServiceTest
    {
        private readonly IDynamicRepository Repo = Substitute.For<IDynamicRepository>();
        private readonly ISystemService systemService = Substitute.For<ISystemService>();

        [Fact]
        public void GetLandingPageStatistics_WhenCallWithEmptyLanguage_CallStatisticsRepository()
        {
            // Arrange
            var expected = new List<LandingPageStatistics>
            {
                new LandingPageStatistics { LanguageCode = "vi" },
                new LandingPageStatistics { LanguageCode = "en" }
            };

            var param = new LandingPageStatisticsParam();
            Repo.Fetch<LandingPageStatistics>(Arg.Compat.Any<GetLandingPageStatisticsCriteria>()).Returns(expected);

            var service = CreateService();

            // Action
            var result = service.GetLandingPageStatistics(param);

            // Assert
            Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(result));
        }

        [Fact]
        public void GetLandingPageStatistics_WhenCall_CallStatisticsRepository()
        {
            var param = new LandingPageStatisticsParam();
            var service = CreateService();

            service.GetLandingPageStatistics(param);

            Repo.Received().Fetch<LandingPageStatistics>(Arg.Compat.Any<GetLandingPageStatisticsCriteria>());
        }

        [Fact]
        public void GetReferrerStatistics_WhenCall_CallStatisticsRepository()
        {
            var param = new ReferrerStatisticsParam();
            var service = CreateService();

            service.GetReferrerStatistics(param);

            Repo.Received().Fetch<ReferrerStatistics>(Arg.Compat.Any<GetReferrerStatisticsCriteria>());
        }

        [Fact]
        public void GetStatisticsDetailBySite_WhenCall_CallStatisticsRepository()
        {
            var param = new StatisticsDetailBySiteParam();
            var service = CreateService();

            service.GetStatisticsDetailBySite(param);

            Repo.Received().Fetch<ReferrerStatistics>(Arg.Any<GetStatisticsDetailBySiteCriteria>());
        }

        [Fact]
        public void GetMarketingPageStatistics_WhenCall_CallStatisticsRepository()
        {
            var param = new MarketingPageStatisticsParam();
            var service = CreateService();

            service.GetMarketingPageStatistics(param);

            Repo.Received().Fetch<MarketingPageStatistics>(Arg.Any<GetMarketingPageStatisticsCriteria>());
        }

        private StatisticsService CreateService()
            => new StatisticsService(Repo, systemService);
    }
}
