using Fanex.Affiliate.Admin.Core.Controllers;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.ExchangeRate;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.ExchangeRate;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.ExchangeRate;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Moq;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.ExchangeRate
{
    public class ExchangeRateControllerTest
    {
        private ExchangeRateController CreateExchangeRateController(
            ISessionService sessionService = null,
            IExchangeRateService exchangeRateService = null
            )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeExchangeRateService = exchangeRateService ?? Mock.Of<IExchangeRateService>();

            var controller = new ExchangeRateController(fakeSessionService, fakeExchangeRateService);

            var request = MockingHelper.GetHttpRequest();
            var server = new Mock<HttpServerUtilityBase>();
            server.Setup(s => s.HtmlEncode(It.IsAny<string>())).Returns<string>(s => $"Encoded{s}");

            controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
                ctx.HttpContext.Request == request &&
                ctx.HttpContext.Server == server.Object &&
                ctx.HttpContext.Session.SessionID == "SessionId"
            );

            return controller;
        }

        [Fact]
        public void Index_WhenCallHasPermission_ReturnView()
        {
            // Arrange
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(s => s.UserPermissionList).Returns(new List<UserPermission> { new UserPermission { FunctionName = "[AffEx.Rate]", IsHost = true } });
            var controller = CreateExchangeRateController(sessionService.Object);

            // Action
            var viewResult = controller.Index() as ViewResult;
            var viewName = viewResult.ViewName;
            var hasPermission = viewResult.Model as bool?;

            // Assert
            Assert.Equal("", viewName);
            Assert.True(hasPermission);
        }

        [Fact]
        public void Index_WhenCallNonPermission_ReturnView()
        {
            // Arrange
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(s => s.UserPermissionList).Returns(new List<UserPermission> { new UserPermission { FunctionName = "[Rate]", IsHost = false } });
            var controller = CreateExchangeRateController(sessionService.Object);

            // Action
            var viewResult = controller.Index() as ViewResult;
            var viewName = viewResult.ViewName;
            var hasPermission = viewResult.Model as bool?;

            // Assert
            Assert.Equal("", viewName);
            Assert.False(hasPermission);
        }

        [Fact]
        public void GetExchangeRateList_WhenCall_ExcuteWithCorrectData()
        {
            // Arrange
            var date = new DateTime();
            DateTime? passedDate = null;
            var exchangeRateService = new Mock<IExchangeRateService>();
            exchangeRateService.Setup(s => s.GetExchangeRateList(date)).Returns(new List<ExchangeRateInfo> { new ExchangeRateInfo() }).Callback<DateTime>(d => passedDate = d);
            var controller = CreateExchangeRateController(exchangeRateService: exchangeRateService.Object);

            // Action
            var result = controller.GetExchangeRateList(date) as JsonNetResult;
            var exchangeRateList = result.Data as List<ExchangeRateInfo>;

            // Assert
            Assert.Single(exchangeRateList);
            Assert.Equal(date, passedDate);
        }

        [Fact]
        public void UpdateExchangeRate_WhenCallWithInvalidModel_ReturnError()
        {
            // Arrange
            var param = new ExchangeRateParam();
            var controller = CreateExchangeRateController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.UpdateExchangeRate(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("The rate is invalid.", resultInfo.Message);
        }

        [Fact]
        public void UpdateExchangeRate_WhenCallWithInvalidRate_ReturnError()
        {
            // Arrange
            var param = new ExchangeRateParam { ExchangeId = 2, Rate = -1, RateType = 0 };
            var controller = CreateExchangeRateController();

            // Action
            var result = controller.UpdateExchangeRate(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("The rate is invalid.", resultInfo.Message);
        }

        [Fact]
        public void UpdateExchangeRate_WhenCallWithValidParam_ExcuteSuccessfully()
        {
            // Arrange
            var param = new ExchangeRateParam { ExchangeId = 2, Rate = 0.05555m, RateType = 0 };
            ExchangeRateParam passedParam = null;
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(s => s.UserId).Returns(123);
            var exchangeRateService = new Mock<IExchangeRateService>();
            exchangeRateService.Setup(s => s.UpdateExchangeRate(param)).Returns(new ResultInfo()).Callback<ExchangeRateParam>(d => passedParam = d);
            var controller = CreateExchangeRateController(sessionService.Object, exchangeRateService.Object);

            // Action
            var result = controller.UpdateExchangeRate(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.Equal(param, passedParam);
            Assert.True(resultInfo.IsSuccessful);
        }
    }
}
