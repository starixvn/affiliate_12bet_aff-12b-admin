using Fanex.Affiliate.Admin.Core.Controllers;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services.UserPanel.Statistics;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers
{
    public class CommonSettingControllerTest
    {
        [Fact]
        public void GetPageList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var landingPageService = new Mock<ILandingPageListService>();
            landingPageService.Setup(s => s.GetLandingPageList()).Returns(new List<LandingPage> { new LandingPage() });
            var controller = CreateCommonSettingController(landingPageService: landingPageService.Object);

            // Action
            var result = controller.GetPageList() as JsonNetResult;
            var data = result.Data as List<LandingPage>;

            // Assert
            Assert.Single(data);
        }

        [Fact]
        public void GetTrackingCodeList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = 1271808;
            var passedParam = 0;
            var trackingStatisticsService = new Mock<ITrackingStatisticsService>();
            trackingStatisticsService.Setup(s => s.GetTrackingCodeList(param)).Returns(new List<string> { "Sportbook" }).Callback<int>(p => passedParam = p);
            var controller = CreateCommonSettingController(trackingStatisticsService: trackingStatisticsService.Object);

            // Action
            var result = controller.GetTrackingCodeList(param) as JsonNetResult;
            var data = result.Data as List<string>;

            // Assert
            Assert.Single(data);
            Assert.Equal(param, passedParam);
        }

        private CommonSettingController CreateCommonSettingController(
            ILandingPageListService landingPageService = null,
            ITrackingStatisticsService trackingStatisticsService = null
            )
        {
            var fakelandingPageService = landingPageService ?? Mock.Of<ILandingPageListService>();
            var faketrackingStatisticsService = trackingStatisticsService ?? Mock.Of<ITrackingStatisticsService>();

            return new CommonSettingController(fakelandingPageService, faketrackingStatisticsService);
        }
    }
}
