using Fanex.Affiliate.Admin.Core.Controllers.Report;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Report;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Report;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Affiliate.Admin.Core.ViewModels.Report;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.Report
{
    public class PlayerGgrReportControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnCorrectViewAndModel()
        {
            //Arrange
            var systemService = new Mock<ISystemService>();
            systemService.Setup(p => p.GetProductList()).Returns(new List<Product> { new Product() });
            systemService.Setup(c => c.GetBaseCurrencies()).Returns(new List<Currency> { new Currency() });
            var controller = CreatePlayerGgrReportController(null, systemService.Object, null);

            //Action
            var result = controller.Index() as ViewResult;
            var data = result.Model as ReportCommonViewModel;

            //Assert
            Assert.Single(data.ProductList);
            Assert.Single(data.BaseCurrencyList);
            Assert.Equal("~/Areas/Portal/Views/Report/PlayerGgrReport.cshtml", result.ViewName);
        }

        [Fact]
        public void GetPlayerGgrReportData_WhenCallWithInvalidParam_ReturnEmptyData()
        {
            //Arrange
            var param = new PlayerGgrReportParam();
            var controller = CreatePlayerGgrReportController();
            controller.ModelState.AddModelError("Error", string.Empty);

            //Action
            var result = controller.GetPlayerGgrReportData(param) as JsonNetResult;
            var data = result.Data as EntityResult<PlayerGgrReport>;

            //Assert
            Assert.Empty(data.MainData);
        }

        [Fact]
        public void GetPlayerGgrReportData_WhenCallWithValidParam_ReturnCorrectData()
        {
            //Arrange
            var param = new PlayerGgrReportParam();
            var playerGgrReportService = new Mock<IPlayerGgrReportService>();
            playerGgrReportService.Setup(p => p.GetPlayerGgrReportData(param)).Returns(new EntityResult<PlayerGgrReport> { MainData = new List<PlayerGgrReport> { new PlayerGgrReport() } });
            var controller = CreatePlayerGgrReportController(playerGgrReportService: playerGgrReportService.Object);

            //Action
            var result = controller.GetPlayerGgrReportData(param) as JsonNetResult;
            var data = result.Data as EntityResult<PlayerGgrReport>;

            //Assert
            Assert.Single(data.MainData);
        }

        public PlayerGgrReportController CreatePlayerGgrReportController(
            ISessionService sessionService = null,
            ISystemService systemService = null,
            IPlayerGgrReportService playerGgrReportService = null)
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakesystemService = systemService ?? Mock.Of<ISystemService>();
            var fakePlayerGgrReportService = playerGgrReportService ?? Mock.Of<IPlayerGgrReportService>();

            return new PlayerGgrReportController(fakeSessionService, fakesystemService, fakePlayerGgrReportService);
        }
    }
}
