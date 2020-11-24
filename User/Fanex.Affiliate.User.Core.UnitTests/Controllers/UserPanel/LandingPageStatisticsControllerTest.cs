using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.ViewModels.UserPanel.Statistics;
using Moq;
using Xunit;
using Fanex.Affiliate.User.Core.DTO;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers.UserPanel
{
    public class LandingPageStatisticsControllerTest
    {
        private const bool IsInternal = false;

        [Fact]
        public void IndexGet_WhenCall_ReturnViewWithCorrectModelData()
        {
            // Arrange
            var marketingService = new Mock<IMarketingService>();
            var languageList = CreateFakeLanguageList();
            marketingService.Setup(m => m.GetLanguageList()).Returns(languageList);
            marketingService.Setup(m => m.GetLandingPageList(IsInternal)).Returns(new List<LandingPage>());

            var statisticsRepository = new Mock<IStatisticsService>();
            var landingPageStisticsList = CreateFakeLandingPageStatisticsList();
            statisticsRepository.Setup(m => m.GetLandingPageStatistics(It.IsAny<LandingPageStatisticsParam>())).Returns(landingPageStisticsList);

            var today = DateTime.Today;
            var controller = CreateController(marketingService: marketingService.Object, statisticsService: statisticsRepository.Object);

            // Action
            var result = controller.Index() as ViewResult;
            var model = result.Model as LandingPageStatisticsViewModel;

            // Assert
            Assert.Equal(2, model.LanguageList.Count());
            Assert.Equal(2, model.DataList.Count());
            Assert.False(model.IsDetail);
            Assert.Equal(new DateTime(today.Year, today.Month, 1), model.FromDate.Date);
            Assert.Equal(today, model.ToDate.Date);
            Assert.Equal(0, model.RowNumber);
        }

        [Fact]
        public void IndexGet_WhenCall_CallGetLandingPageStatisticsWithCorrectParam()
        {
            // Arrange
            LandingPageStatisticsParam passedParam = null;
            var marketingService = new Mock<IMarketingService>();
            var languageList = CreateFakeLanguageList();
            marketingService.Setup(m => m.GetLanguageList()).Returns(languageList);
            marketingService.Setup(m => m.GetLandingPageList(IsInternal)).Returns(new List<LandingPage>());

            var statisticsRepository = new Mock<IStatisticsService>();
            var landingPageStisticsList = CreateFakeLandingPageStatisticsList();
            statisticsRepository.Setup(m => m.GetLandingPageStatistics(It.IsAny<LandingPageStatisticsParam>()))
                .Returns(landingPageStisticsList)
                .Callback<LandingPageStatisticsParam>(p => passedParam = p);

            var today = DateTime.Today;
            var controller = CreateController(marketingService: marketingService.Object, statisticsService: statisticsRepository.Object);

            // Action
            controller.Index();

            // Assert
            Assert.Equal(Constant.DefaultAffId, passedParam.AffId);
            Assert.Equal(IsInternal, passedParam.IsInternal);
            Assert.Equal(new DateTime(today.Year, today.Month, 1), passedParam.FromDate.Date);
            Assert.Equal(today, passedParam.ToDate.Date);
        }

        [Fact]
        public void IndexPost_WhenCall_ReturnViewWithCorrectModelData()
        {
            // Arrange
            var param = new LandingPageStatisticsParam();
            var marketingService = new Mock<IMarketingService>();
            var languageList = CreateFakeInvalidLanguageList();
            marketingService.Setup(m => m.GetLanguageList()).Returns(languageList);
            var statisticsRepository = new Mock<IStatisticsService>();
            var landingPageStisticsList = CreateFakeLandingPageStatisticsList();
            statisticsRepository.Setup(m => m.GetLandingPageStatistics(param)).Returns(landingPageStisticsList);

            var controller = CreateController(statisticsService: statisticsRepository.Object, marketingService: marketingService.Object);

            // Action
            var result = controller.Index(param) as PartialViewResult;
            var viewName = result.ViewName;
            var model = result.Model as LandingPageStatisticsViewModel;

            // Assert
            Assert.Equal("~/Views/UserPanel/Statistics/StatisticsPartial/_LandingPageStatisticsContent.cshtml", viewName);
            Assert.Equal(2, model.DataList.Count());
            Assert.False(model.IsDetail);
            Assert.Equal(Constant.DefaultAffId, param.AffId);
            Assert.Equal(IsInternal, param.IsInternal);
        }

        private IEnumerable<Language> CreateFakeLanguageList()
        {
            return new List<Language>
            {
                new Language
                {
                    MarketId = "en-US",
                    MarketName = "English",
                },
                new Language
                {
                    MarketId = "vi-VN",
                    MarketName = "Vietnamese",
                }
            };
        }

        private IEnumerable<Language> CreateFakeInvalidLanguageList()
        {
            return new List<Language>
            {
                new Language
                {
                    MarketId = "vi-VN",
                    MarketName = "Vietnamese",
                    ShortedCode = "vn"
                }
            };
        }

        private IEnumerable<LandingPageStatistics> CreateFakeLandingPageStatisticsList()
        {
            return new List<LandingPageStatistics>
            {
                new LandingPageStatistics { LanguageCode = "vn" },
                new LandingPageStatistics { LanguageCode = "en" }
            };
        }

        private static LandingPageStatisticsController CreateController(
            ISessionService sessionService = null,
            ILanguageService languageService = null,
            IStatisticsService statisticsService = null,
            IMarketingService marketingService = null)
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeLanguageService = languageService ?? Mock.Of<ILanguageService>();
            var fakeStaticticsService = statisticsService ?? Mock.Of<IStatisticsService>();
            var fakeMarketingService = marketingService ?? Mock.Of<IMarketingService>();

            return new LandingPageStatisticsController(
                fakeSessionService,
                fakeLanguageService,
                fakeStaticticsService,
                fakeMarketingService);
        }
    }
}
