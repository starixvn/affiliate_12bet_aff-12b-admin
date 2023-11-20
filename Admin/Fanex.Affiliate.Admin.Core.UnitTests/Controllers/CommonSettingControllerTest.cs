using Fanex.Affiliate.Admin.Core.Controllers;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
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
            var commonSettingService = new Mock<ISystemService>();
            commonSettingService.Setup(s => s.GetProductList()).Returns(new List<Product> { new Product() });
            var controller = CreateCommonSettingController(commonSettingService: commonSettingService.Object);

            // Action
            var result = controller.GetProductList() as JsonNetResult;
            var data = result.Data as List<Product>;

            // Assert
            Assert.Single(data);
        }

        [Fact]
        public void GetCountryList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var commonSettingService = new Mock<ISystemService>();
            commonSettingService.Setup(s => s.GetCountryList()).Returns(new List<Country> { new Country { CountryCode = "EN", CountryName = "England" } });
            var controller = CreateCommonSettingController(commonSettingService.Object);

            // Action
            var result = controller.GetCountryList() as JsonNetResult;
            var data = result.Data as List<Country>;

            // Assert
            Assert.Single(data);
        }

        private CommonSettingController CreateCommonSettingController(
            ISystemService commonSettingService = null
            )
        {
            var fakeCommonService = commonSettingService ?? Mock.Of<ISystemService>();

            return new CommonSettingController(fakeCommonService);
        }
    }
}
