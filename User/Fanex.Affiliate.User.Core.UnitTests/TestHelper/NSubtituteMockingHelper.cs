using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Fanex.Affiliate.User.Core.UnitTests.TestHelper
{
    public static class NSubtituteMockingHelper
    {
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
            marketingService.GenerateLinkList(Arg.Compat.Any<LinkGenerationParam>()).Returns(Constant.LinkList);

            return marketingService;
        }

        public static ISessionService FakeSessionService()
        {
            var service = Substitute.For<ISessionService>();
            service.CaptchaCode.Returns(Constant.Captcha);
            service.CurrentLanguage.Returns(Constant.DefaultLanguage);
            service.IsInternal.Returns(false);
            service.UserSession.Returns(Constant.Session);
            return service;
        }

        public static ILanguageService FakeLanguageService()
        {
            var service = Substitute.For<ILanguageService>();
            service.GetFavoriteLanguage().Returns(Constant.DefaultLanguage);
            return service;
        }

        public static Controller FakeController(Controller controller, RouteData routeData = null)
        {
            var request = GetHttpRequest();
            var server = Substitute.For<HttpServerUtilityBase>();
            server.HtmlEncode(Arg.Compat.Any<string>()).Returns(s => $"Encoded{s}");
            server.HtmlDecode(Arg.Compat.Any<string>()).Returns(s => $"{s}");

            var context = Substitute.For<HttpContextBase>();
            context.Request.Returns(request);
            context.Server.Returns(server);
            var routData = routeData ?? new RouteData();

            context.Request.Returns(request);
            controller.ControllerContext = new ControllerContext(context, routData, controller);

            var requestContext = new RequestContext(context, routData);
            controller.Url = new UrlHelper(requestContext);

            return controller;
        }

        public static HttpRequestBase GetHttpRequest()
        {
            var mockHttpRequest = Substitute.For<HttpRequestBase>();
            mockHttpRequest.ApplicationPath.Returns("/");
            mockHttpRequest.UserHostAddress.Returns("::1");
            mockHttpRequest.Url.Returns(new Uri(Constant.DefaultRootDomain));
            mockHttpRequest.ServerVariables.Returns(new NameValueCollection());
            mockHttpRequest.Browser.Browser.Returns(Constant.Browser);
            mockHttpRequest[Arg.Compat.Any<string>()].Returns("");
            mockHttpRequest.ServerVariables.Returns(new NameValueCollection());

            return mockHttpRequest;
        }

        public static void FakeAjaxRequest(HttpRequestBase request)
        {
            request["X-Requested-With"].Returns("XMLHttpRequest");
        }
    }
}
