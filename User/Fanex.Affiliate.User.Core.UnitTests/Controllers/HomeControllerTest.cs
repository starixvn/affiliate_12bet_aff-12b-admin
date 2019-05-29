using System;
using System.Web;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.ViewModels;
using Moq;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers
{
    public class HomeControllerTest
    {
        const string PromotionTemplate = "https://promotions.{0}.com?lang=en&act=sports";

        [Theory]
        [InlineData(false, "/SignUp", "javascript:;")]
        [InlineData(true, "/UserPanel/Main/#AccountOverView", "/UserPanel/Main/#MediaGallery")]
        public void Index_CheckIsAuthenticated_ReturnCorrectViewModel(bool isAuthenticated, string expectedSignupLink, string expectedBannerlink)
        {
            // Arrange
            var sessionService = MockingHelper.GetSessionService();
            var controller = CreateController(isAuthenticated: isAuthenticated, sessionService: sessionService);

            var expectedResult = new HomeViewModel
            {
                IsAuthenticated = isAuthenticated,
                SignUpLink = expectedSignupLink,
                BannerLink = expectedBannerlink
            };

            // Action
            var actionResult = controller.Index() as ViewResult;
            var result = actionResult.Model as HomeViewModel;

            // Assert
            Assert.Equal(expectedResult.IsAuthenticated, result.IsAuthenticated);
            Assert.Equal(expectedResult.SignUpLink, result.SignUpLink);
            Assert.Equal(expectedResult.BannerLink, result.BannerLink);
        }

        [Fact]
        public void Index_HostNameHasSubDomain_ReturnCorrectPromotionLink()
        {
            // Arrange
            var uri = new Uri("http://www.subdomain.abcxyz.com");
            var controller = CreateController(uri: uri);

            var expectedResult = "https://promotions.abcxyz.com?lang=en&act=sports";

            // Action
            var actionResult = controller.Index() as ViewResult;
            var result = actionResult.Model as HomeViewModel;

            // Assert
            Assert.Equal(expectedResult, result.Promotion12BetLink);
        }

        [Fact]
        public void Index_HostNameIsLocalHost_ReturnPromotionLinkWithoutDomainName()
        {
            // Arrange
            var uri = new Uri("http://localhost:8765");
            var controller = CreateController(uri: uri);

            var expectedResult = "https://promotions.com?lang=en&act=sports";

            // Action
            var actionResult = controller.Index() as ViewResult;
            var result = actionResult.Model as HomeViewModel;

            // Assert
            Assert.Equal(expectedResult, result.Promotion12BetLink);
        }

        private static HomeController CreateController(
            ILanguageService languageService = null,
            ISessionService sessionService = null,
            IAppSetting appSetting = null,
            bool isAuthenticated = true,
            Uri uri = null)
        {
            languageService = languageService ?? Mock.Of<ILanguageService>();
            sessionService = sessionService ?? MockingHelper.GetSessionService();
            appSetting = appSetting ?? Mock.Of<IAppSetting>(ap =>
                !ap.IsEuroSite &&
                ap.Promotion12BetLink == PromotionTemplate);

            var controller = new HomeController(sessionService, appSetting, languageService);
            var request = MockingHelper.GetHttpRequest();
            var mockRequest = Mock.Get(request);
            mockRequest.Setup(r => r.IsAuthenticated).Returns(isAuthenticated);

            if (uri != null)
            {
                mockRequest.Setup(r => r.Url).Returns(uri);
            }

            var response = new Mock<HttpResponseBase>();
            response.Setup(r => r.Cookies).Returns(new HttpCookieCollection());

            controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
                ctx.HttpContext.Request == mockRequest.Object &&
                ctx.HttpContext.Response == response.Object
            );

            var urlHelper = new Mock<UrlHelper>();
            urlHelper.Setup(x => x.Action(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>((a, c) => $"/{c}/{a}");
            urlHelper.Setup(x => x.Content(It.IsAny<string>()))
                .Returns<string>(a => a.Substring(1, a.Length - 1));
            controller.Url = urlHelper.Object;

            return controller;
        }
    }
}