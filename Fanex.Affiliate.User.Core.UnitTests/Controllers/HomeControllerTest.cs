using System;
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
        const string PromotionTemplate = "https://promotions.{0}.com?lang={1}&act=sports";

        [Theory]
        [InlineData(false, "/SignUp/Index", "javascript:;")]
        [InlineData(true, "/UserPanel/Index/#AccountOverView", "/UserPanel/Index/#MediaGallery")]
        public void Index_CheckIsAuthenticated_ReturnCorrectViewModel(bool isAuthenticated, string expectedSignupLink, string expectedBannerlink)
        {
            // Arrange
            var controller = CreateController(isAuthenticated: isAuthenticated);

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
        public void Index_IsEuroSite_ReturnCorrectPromotionLink()
        {
            // Arrange
            var appSetting = Mock.Of<IAppSetting>(ap => ap.IsEuroSite && ap.Promotion12BetLink == PromotionTemplate);
            var controller = CreateController(isAuthenticated: false, appSetting: appSetting);

            var expectedResult = "https://promotions.abcxyz.com?lang=eneuro&act=sports";

            // Action
            var actionResult = controller.Index() as ViewResult;
            var result = actionResult.Model as HomeViewModel;

            // Assert
            Assert.Equal(expectedResult, result.Promotion12BetLink);
        }

        [Fact]
        public void Index_UnknownLanguageCode_ReturnCorrectPromotionLinkWithDefaultShortedLangCode()
        {
            // Arrange
            var sessionService = MockingHelper.GetSessionService();
            sessionService.CurrentLanguage = "abc";
            var controller = CreateController(sessionService: sessionService);

            var expectedResult = "https://promotions.abcxyz.com?lang=en&act=sports";

            // Action
            var actionResult = controller.Index() as ViewResult;
            var result = actionResult.Model as HomeViewModel;

            // Assert
            Assert.Equal(expectedResult, result.Promotion12BetLink);
        }

        [Fact]
        public void Index_WhenCallWithCorrectLanguageCode_ReturnCorrectPromotionLink()
        {
            // Arrange
            var sessionService = MockingHelper.GetSessionService();
            sessionService.CurrentLanguage = "vi-VN";
            var controller = CreateController(sessionService: sessionService);

            var expectedResult = "https://promotions.abcxyz.com?lang=vn&act=sports";

            // Action
            var actionResult = controller.Index() as ViewResult;
            var result = actionResult.Model as HomeViewModel;

            // Assert
            Assert.Equal(expectedResult, result.Promotion12BetLink);
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
        [InlineData("vi-VN")]
        [InlineData("")]
        public void ChangeLanguage_WhenCall_SetLanguageCorrectly(string languageCode)
        {
            // Arrange
            var sessionService = MockingHelper.GetSessionService();
            var languageService = new Mock<ILanguageService>();
            var controller = CreateController(languageService: languageService.Object, sessionService: sessionService);

            var expectedLanguagCode = string.IsNullOrEmpty(languageCode) ? "en-US" : languageCode;

            // Action
            controller.ChangeLanguage(languageCode);

            // Assert
            Assert.Equal(expectedLanguagCode, sessionService.CurrentLanguage);
            languageService.Verify(l => l.SetCulture(expectedLanguagCode), Times.Once);
        }

        private HomeController CreateController(
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

            controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
                ctx.HttpContext.Request == mockRequest.Object
            );

            var urlHelper = new Mock<UrlHelper>();
            urlHelper.Setup(x => x.Action(It.IsAny<string>(), It.IsAny<string>())).Returns<string, string>((a, c) => $"/{c}/{a}");
            controller.Url = urlHelper.Object;

            return controller;
        }
    }
}