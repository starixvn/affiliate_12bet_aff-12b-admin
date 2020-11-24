using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using NSubstitute;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    public class StatisticsServiceTest
    {
        private readonly IStatisticsRepository Repo;
        private readonly ILanguageService LanguageService;

        public StatisticsServiceTest()
        {
            Repo = Substitute.For<IStatisticsRepository>();
            LanguageService = NSubtituteMockingHelper.FakeLanguageService();
        }

        [Fact]
        public void GetLandingPageStatistics_WhenCallWithEmptyLanguage_CallStatisticsRepository()
        {
            var param = new LandingPageStatisticsParam();
            var service = CreateService();

            service.GetLandingPageStatistics(param);

            Repo.Received().GetLandingPageStatistics(Arg.Compat.Any<LandingPageStatisticsParam>());
            Assert.Equal("en,vn,us", param.LanguageCodes);
        }

        [Fact]
        public void GetLandingPageStatistics_WhenCallWithLanguage_CallStatisticsRepository()
        {
            var param = new LandingPageStatisticsParam { LanguageCode = "en-US" };
            var service = CreateService();

            service.GetLandingPageStatistics(param);

            Repo.Received().GetLandingPageStatistics(Arg.Compat.Any<LandingPageStatisticsParam>());
            Assert.Equal("us", param.LanguageCodes);
        }

        [Fact]
        public void GetLandingPageStatistics_WhenCallWithInvaidLanguage_CallStatisticsRepository()
        {
            var param = new LandingPageStatisticsParam { LanguageCode = "abc" };
            var service = CreateService();

            service.GetLandingPageStatistics(param);

            Repo.Received().GetLandingPageStatistics(Arg.Compat.Any<LandingPageStatisticsParam>());
            Assert.Equal("en", param.LanguageCodes);
        }

        [Fact]
        public void GetLandingPageStatistics_WhenCall_CallStatisticsRepository()
        {
            var param = new LandingPageStatisticsParam();
            var service = CreateService();

            service.GetLandingPageStatistics(param);

            Repo.Received().GetLandingPageStatistics(param);
        }

        [Fact]
        public void GetReferrerStatistics_WhenCall_CallStatisticsRepository()
        {
            var param = new ReferrerStatisticsParam();
            var service = CreateService();

            service.GetReferrerStatistics(param);

            Repo.Received().GetReferrerStatistics(param);
        }

        [Fact]
        public void GetStatisticsDetailBySite_WhenCall_CallStatisticsRepository()
        {
            var param = new StatisticsDetailBySiteParam();
            var service = CreateService();

            service.GetStatisticsDetailBySite(param);

            Repo.Received().GetStatisticsDetailBySite(param);
        }

        [Fact]
        public void GetStatisticsOverview_WhenCall_CallStatisticsRepository()
        {
            var param = new StatisticsOverviewParam();
            var service = CreateService();

            service.GetStatisticsOverview(param);

            Repo.Received().GetStatisticsOverview(param);
        }

        [Fact]
        public void GetTrackingCode_WhenCall_CallStatisticsRepository()
        {
            var service = CreateService();

            service.GetTrackingCode(1, false);

            Repo.Received().GetTrackingCode(1, false);
        }

        [Fact]
        public void GetTrackingStatistics_WhenCall_CallStatisticsRepository()
        {
            var service = CreateService();

            service.GetTrackingStatistics(1, "abc", false);

            Repo.Received().GetTrackingStatistics(1, "abc", false);
        }

        [Fact]
        public void GetTrackingStatisticsDetail_WhenCall_CallStatisticsRepository()
        {
            var param = new TrackingStatisticsDetailParam();
            var service = CreateService();

            service.GetTrackingStatisticsDetail(param);

            Repo.Received().GetTrackingStatisticsDetail(param);
        }

        private StatisticsService CreateService()
            => new StatisticsService(Repo, LanguageService);
    }
}
