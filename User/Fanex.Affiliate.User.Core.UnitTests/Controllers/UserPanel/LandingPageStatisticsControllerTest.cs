using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.DTO.UserPanel.Statistics;
using Fanex.Affiliate.User.Core.Enum;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.ViewModels.UserPanel.Statistics;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers.UserPanel
{
    public class LandingPageStatisticsControllerTest
    {
        private const bool IsInternal = false;
        private const string CommGroup = "test";

        [Fact]
        public void IndexGet_WhenCall_ReturnViewWithCorrectModelData()
        {
            // Arrange
            var marketingService = new Mock<IMarketingService>();
            marketingService.Setup(m => m.GetLandingPageList(CommGroup, IsInternal, It.IsAny<AffiliateLinkType>())).Returns(new List<LandingPage>());
            var languageList = CreateFakeLanguageList();
            var languageService = new Mock<ISystemService>();
            languageService.Setup(m => m.GetLanguageList()).Returns(languageList);

            var statisticsService = new Mock<IStatisticsService>();
            var landingPageStisticsList = CreateFakeLandingPageStatisticsList();
            statisticsService.Setup(m => m.GetLandingPageStatistics(It.IsAny<LandingPageStatisticsParam>())).Returns(landingPageStisticsList);

            var today = DateTime.Today;
            var controller = CreateController(null, languageService.Object, statisticsService.Object, marketingService.Object);

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
            marketingService.Setup(m => m.GetLandingPageList(CommGroup, IsInternal, It.IsAny<AffiliateLinkType>())).Returns(new List<LandingPage>());
            var languageList = CreateFakeLanguageList();
            var languageService = new Mock<ISystemService>();
            languageService.Setup(m => m.GetLanguageList()).Returns(languageList);

            var statisticsService = new Mock<IStatisticsService>();
            var landingPageStisticsList = CreateFakeLandingPageStatisticsList();
            statisticsService.Setup(m => m.GetLandingPageStatistics(It.IsAny<LandingPageStatisticsParam>()))
                .Returns(landingPageStisticsList)
                .Callback<LandingPageStatisticsParam>(p => passedParam = p);

            var today = DateTime.Today;
            var controller = CreateController(null, languageService.Object, statisticsService.Object, marketingService.Object);

            // Action
            controller.Index();

            // Assert
            Assert.Equal(Constant.DefaultAffId, passedParam.AffId);
            Assert.Equal(IsInternal, passedParam.IsInternal);
            Assert.Equal(new DateTime(today.Year, today.Month, 1), passedParam.FromDate.Date);
            Assert.Equal(today, passedParam.ToDate.Date);
        }

        [Theory]
        [InlineData(null, "en,vn,en")]
        [InlineData("", "en,vn,en")]
        [InlineData("en-US", "en")]
        [InlineData("th-TH", null)]
        public void IndexPost_WhenCall_ReturnViewWithCorrectModelData(string languageCode, string expectedShortedCode)
        {
            // Arrange
            var param = new LandingPageStatisticsParam
            {
                LanguageCode = languageCode
            };
            var statisticsService = new Mock<IStatisticsService>();
            var landingPageStatisticsList = CreateFakeLandingPageStatisticsList();
            statisticsService.Setup(m => m.GetLandingPageStatistics(param)).Returns(landingPageStatisticsList);

            var languageService = new Mock<ISystemService>();
            languageService.Setup(m => m.GetLanguageList()).Returns(Constant.LanguageList);
            var controller = CreateController(null, languageService.Object, statisticsService.Object);

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
            Assert.Equal(expectedShortedCode, param.LanguageCode);
        }

        private IEnumerable<Language> CreateFakeLanguageList()
        {
            return new List<Language>
            {
                new Language
                {
                    Code = "en-US",
                    DisplayName = "English",
                },
                new Language
                {
                    Code = "vi-VN",
                    DisplayName = "Vietnamese",
                }
            };
        }

        private IEnumerable<LandingPageStatistics> CreateFakeLandingPageStatisticsList()
        {
            return new List<LandingPageStatistics>
            {
                new LandingPageStatistics { LanguageCode = "abc" },
                new LandingPageStatistics { LanguageCode = "en" }
            };
        }

        private static LandingPageStatisticsController CreateController(
            ISessionService sessionService = null,
            ISystemService languageService = null,
            IStatisticsService statisticsService = null,
            IMarketingService marketingService = null)
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeLanguageService = languageService ?? Mock.Of<ISystemService>();
            var fakeStatisticsService = statisticsService ?? Mock.Of<IStatisticsService>();
            var fakeMarketingService = marketingService ?? Mock.Of<IMarketingService>();

            return new LandingPageStatisticsController(
                fakeSessionService,
                fakeLanguageService,
                fakeStatisticsService,
                fakeMarketingService);
        }
    }
}
