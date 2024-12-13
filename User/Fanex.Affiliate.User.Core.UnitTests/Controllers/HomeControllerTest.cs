using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.ViewModels;
using Moq;
using NSubstitute;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers
{
    public class HomeControllerTest
    {
        private const string PromotionTemplate = "https://promotions.{0}.com?lang=en&act=sports";

        public HomeControllerTest()
        {
            MockingUrlHelperExtension.FakeGetFavoriteShortedLanguage();
        }

        [Theory]
        [InlineData(false, "/en/SignUp", "javascript:;")]
        [InlineData(true, "/en/UserPanel/Main/#AccountOverView", "/en/UserPanel/Main/#MediaGallery")]
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

        [Theory]
        [InlineData("Home", "Index")]
        [InlineData("Auth", "ReCheckin")]
        [InlineData("Feedback", "Index")]
        public void Excute_WhenRequestChangeLang_SetForNewLang(string controller, string action)
        {
            var languageService = Substitute.For<ISystemService>();
            var sessionService = NSubstituteMockingHelper.FakeSessionService();
            var appSetting = Substitute.For<IAppSetting>();
            var homeController = new HomeController(sessionService, appSetting, languageService);
            var routeData = new RouteData();
            routeData.Values.Add(nameof(controller), controller);
            routeData.Values.Add(nameof(action), action);
            NSubstituteMockingHelper.FakeController(homeController, routeData);
            var requestContext = new RequestContext(homeController.HttpContext, homeController.RouteData);
            requestContext.HttpContext.Response.Cookies.Returns(new HttpCookieCollection());
            languageService.GetFavoriteLanguage().Returns("vn-vi");

            try
            {
                (homeController as IController).Execute(requestContext);
            }
            catch { }


            languageService.Received().SetCulture("vn-vi");
        }

        private static HomeController CreateController(
            ISystemService languageService = null,
            ISessionService sessionService = null,
            IAppSetting appSetting = null,
            bool isAuthenticated = true,
            Uri uri = null)
        {
            languageService = languageService ?? Mock.Of<ISystemService>();
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