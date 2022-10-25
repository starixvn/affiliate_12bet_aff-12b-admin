using Fanex.Affiliate.Admin.Core.Controllers;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers
{
    public class CommonSettingControllerTest
    {
        [Fact]
        public void GetProductList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var commonSettingService = new Mock<ICommonSettingService>();
            commonSettingService.Setup(s => s.GetProductList()).Returns(new List<Product> { new Product() });
            var controller = CreateCommonSettingController(commonSettingService: commonSettingService.Object);

            // Action
            var result = controller.GetProductList() as JsonNetResult;
            var data = result.Data as List<Product>;

            // Assert
            Assert.Single(data);
        }

        [Fact]
        public void GetPageList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var landingPageRepository = new Mock<ILandingPageRepository>();
            landingPageRepository.Setup(s => s.GetLandingPageList()).Returns(new List<LandingPage> { new LandingPage() });
            var controller = CreateCommonSettingController(landingPageRepository: landingPageRepository.Object);

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

        [Fact]
        public void GetCountryList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var commonSettingService = new Mock<ICommonSettingService>();
            commonSettingService.Setup(s => s.GetCountryList()).Returns(new List<Country> { new Country { CountryCode = "EN", CountryName = "England" } });
            var controller = CreateCommonSettingController(commonSettingService.Object);

            // Action
            var result = controller.GetCountryList() as JsonNetResult;
            var data = result.Data as List<Country>;

            // Assert
            Assert.Single(data);
        }

        private CommonSettingController CreateCommonSettingController(
            ICommonSettingService commonSettingService = null,
            ILandingPageRepository landingPageRepository = null,
            ITrackingStatisticsService trackingStatisticsService = null
            )
        {
            var fakeCommonService = commonSettingService ?? Mock.Of<ICommonSettingService>();
            var fakeLandingPageRepository = landingPageRepository ?? Mock.Of<ILandingPageRepository>();
            var faketrackingStatisticsService = trackingStatisticsService ?? Mock.Of<ITrackingStatisticsService>();

            return new CommonSettingController(fakeCommonService, fakeLandingPageRepository, faketrackingStatisticsService);
        }
    }
}
