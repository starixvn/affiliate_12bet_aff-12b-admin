using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.Auth;
using Moq;
using Fanex.Affiliate.User.Core.BusinessService;
using NSubstitute;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.UnitTests.TestHelper
{
    public static class MockingHelper
    {
        public static ISessionService GetSessionService()
        {
            return Mock.Of<ISessionService>(ss =>
                ss.CaptchaCode == Constant.Captcha &&
                ss.CurrentLanguage == Constant.DefaultLanguage &&
                !ss.IsInternal &&
                ss.UserSession == new UserSession
                {
                    AffId = Constant.DefaultAffId,
                    AffType = Constant.AffTypeAsian,
                    OnUserId = 1,
                    PreferredLanguage = Constant.DefaultLanguage,
                    UserName = Constant.DefaultUserName
                }
            );
        }




        public static Controller FakeController(Controller controller)
        {
            var request = GetHttpRequest();
            var server = new Mock<HttpServerUtilityBase>();
            server.Setup(s => s.HtmlEncode(It.IsAny<string>())).Returns<string>(s => $"Encoded{s}");
            server.Setup(s => s.HtmlDecode(It.IsAny<string>())).Returns<string>(s => $"{s}");

            controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
                ctx.HttpContext.Request == request &&
                ctx.HttpContext.Server == server.Object
            );

            var context = Mock.Of<HttpContextBase>(ctx => ctx.Request == request);
            var requestContext = new RequestContext(context, new RouteData());
            controller.Url = new UrlHelper(requestContext);

            return controller;
        }

        public static HttpRequestBase GetHttpRequest()
        {
            var mockHttpRequest = new Mock<HttpRequestBase>();
            mockHttpRequest.Setup(p => p.ApplicationPath).Returns("/");
            mockHttpRequest.Setup(p => p.UserHostAddress).Returns("::1");
            mockHttpRequest.Setup(p => p.Url).Returns(new Uri(Constant.DefaultRootDomain));
            mockHttpRequest.Setup(p => p.ServerVariables).Returns(new NameValueCollection());
            mockHttpRequest.Setup(p => p.Browser.Browser).Returns(Constant.Browser);

            return mockHttpRequest.Object;
        }

        public static IMarketingService FakeMarkengtingService()
        {
            var marketingService = Substitute.For<IMarketingService>();
            marketingService.GetTextList(Arg.Compat.Any<MarketingTextParam>()).Returns(new List<MarketingItem>
            {
                new MarketingItem { BannerId = 1, TextId = 1 }
            });
            marketingService.GetCategoryList(Arg.Compat.Any<bool>(), Arg.Compat.Any<bool>()).Returns(Constant.CategoryList);
            marketingService.GetLanguageList(Arg.Compat.Any<bool>()).Returns(Constant.LanguageTypeList);
            marketingService.GetCurrencyList(Arg.Compat.Any<bool>()).Returns(Constant.CurrencyType);
            marketingService.GetDomainList(Arg.Compat.Any<int>(), Arg.Compat.Any<bool>()).Returns(Constant.DomainList);
            marketingService.GetLandingPageList(Arg.Compat.Any<bool>()).Returns(Constant.LandingPageList);

            return marketingService;
        }
    }
}