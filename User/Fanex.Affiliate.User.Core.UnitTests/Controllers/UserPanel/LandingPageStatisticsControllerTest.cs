using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.UserPanel.Statistics;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.ViewModels.UserPanel.Statistics;
using Moq;
using NSubstitute;
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

        private readonly ISessionService _sessionService;
        private readonly ISystemService _systemService;
        private readonly IStatisticsService _statisticsService;

        public LandingPageStatisticsControllerTest()
        {
            _sessionService = NSubtituteMockingHelper.FakeSessionService();
            _systemService = NSubtituteMockingHelper.FakeLanguageService();
            _statisticsService = Substitute.For<IStatisticsService>();
        }

        [Fact]
        public void IndexGet_WhenCall_ReturnViewWithCorrectModelData()
        {
            // Arrange

            var statisticsRepository = new Mock<IStatisticsService>();
            var landingPageStisticsList = CreateFakeLandingPageStatisticsList();
            statisticsRepository.Setup(m => m.GetLandingPageStatistics(It.IsAny<LandingPageStatisticsParam>())).Returns(landingPageStisticsList);

            var today = DateTime.Today;
            var controller = CreateController();

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

            var statisticsRepository = new Mock<IStatisticsService>();
            var landingPageStisticsList = CreateFakeLandingPageStatisticsList();
            statisticsRepository.Setup(m => m.GetLandingPageStatistics(It.IsAny<LandingPageStatisticsParam>()))
                .Returns(landingPageStisticsList)
                .Callback<LandingPageStatisticsParam>(p => passedParam = p);

            var today = DateTime.Today;
            var controller = CreateController();

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
            var statisticsRepository = new Mock<IStatisticsService>();
            var landingPageStisticsList = CreateFakeLandingPageStatisticsList();
            statisticsRepository.Setup(m => m.GetLandingPageStatistics(param)).Returns(landingPageStisticsList);

            var controller = CreateController();

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

        private IEnumerable<LandingPageStatistics> CreateFakeLandingPageStatisticsList()
        {
            return new List<LandingPageStatistics>
            {
                new LandingPageStatistics { LanguageCode = "vn" },
                new LandingPageStatistics { LanguageCode = "en" }
            };
        }

        private LandingPageStatisticsController CreateController()
        {
            var controller = new LandingPageStatisticsController(_sessionService, _systemService, _statisticsService);
            controller = NSubtituteMockingHelper.FakeController(controller) as LandingPageStatisticsController;

            return controller;
        }
    }
}
