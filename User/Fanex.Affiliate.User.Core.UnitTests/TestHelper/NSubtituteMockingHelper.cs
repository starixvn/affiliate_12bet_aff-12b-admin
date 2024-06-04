using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using NSubstitute;
using System;
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
            marketingService.GetTargetProductList(Arg.Compat.Any<bool>()).Returns(Constant.TargetProductList);
            marketingService.GetDomainListByTargetCountry(Arg.Compat.Any<string>(), Arg.Compat.Any<int>(), Arg.Compat.Any<bool>()).Returns(Constant.DomainList);
            marketingService.GetLandingPageList(Arg.Compat.Any<string>(), Arg.Compat.Any<string>(), Arg.Compat.Any<bool>()).Returns(Constant.LandingPageList);
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

        public static ISystemService FakeLanguageService()
        {
            var service = Substitute.For<ISystemService>();
            service.GetFavoriteLanguage().Returns(Constant.DefaultLanguage);
            service.GetSystemTargetLanguages().ReturnsForAnyArgs(Constant.LanguageList);
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
