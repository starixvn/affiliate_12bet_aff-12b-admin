using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.Auth;
using Moq;

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

        public static void SetClaimsPrincipal(string claim, bool hasClaim)
        {
            var request = new HttpRequest("111", "http://127.0.0.1:8080/controller/action", string.Empty);
            var response = new HttpResponse(new StringWriter());
            HttpContext.Current = new HttpContext(request, response)
            {
                User = new ClaimsPrincipal(new List<ClaimsIdentity> { new ClaimsIdentity(new List<Claim> { new Claim(hasClaim ? claim : string.Empty, "Read") }) })
            };
        }
    }
}