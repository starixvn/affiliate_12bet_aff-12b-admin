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
            var systemServive = new Mock<ISystemService>();
            systemServive.Setup(s => s.GetProductList()).Returns(new List<Product> { new Product() });
            var controller = CreateCommonSettingController(systemServive: systemServive.Object);

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
            var systemServive = new Mock<ISystemService>();
            systemServive.Setup(s => s.GetCountryList()).Returns(new List<Country> { new Country { CountryCode = "EN", CountryName = "England" } });
            var controller = CreateCommonSettingController(systemServive.Object);

            // Action
            var result = controller.GetCountryList() as JsonNetResult;
            var data = result.Data as List<Country>;

            // Assert
            Assert.Single(data);
        }

        private CommonSettingController CreateCommonSettingController(
            ISystemService systemServive = null
            )
        {
            var fakeCommonService = systemServive ?? Mock.Of<ISystemService>();

            return new CommonSettingController(fakeCommonService);
        }
    }
}
