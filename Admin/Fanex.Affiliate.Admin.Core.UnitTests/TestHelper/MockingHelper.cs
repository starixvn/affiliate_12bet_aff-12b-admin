using Fanex.Affiliate.Admin.Core.Services.Common;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Security.Claims;
using System.Web;
using System.Web.SessionState;

namespace Fanex.Affiliate.Admin.Core.UnitTests.TestHelper
{
    public static class MockingHelper
    {
        public static ISessionService GetSessionService(string claim = null, bool hasClaim = false, bool isHost = false)
        {
            var claimsIdentities = new List<ClaimsIdentity> { new ClaimsIdentity(new List<Claim> { new Claim(hasClaim ? claim : string.Empty, isHost ? "Write" : "Read") }) };
            return Mock.Of<ISessionService>(ss =>
                ss.AffId == Constant.AffId &&
                ss.AffName == Constant.AffName &&
                ss.OnUserId == Constant.OnUserId &&
                ss.UserId == Constant.UserId &&
                ss.RequestIp == Constant.UserHostAddress &&
                ss.ClaimIdentities == claimsIdentities &&
                ss.UserClaim == new ClaimsPrincipal(claimsIdentities)
            );
        }

        public static HttpRequestBase GetHttpRequest()
        {
            var mockHttpRequest = new Mock<HttpRequestBase>();
            mockHttpRequest.Setup(p => p.ApplicationPath).Returns("/");
            mockHttpRequest.Setup(p => p.UserHostAddress).Returns(Constant.UserHostAddress);
            mockHttpRequest.Setup(p => p.Url).Returns(new Uri(Constant.DefaultRootDomain));
            mockHttpRequest.Setup(p => p.ServerVariables).Returns(new NameValueCollection());
            mockHttpRequest.Setup(p => p.Browser.Browser).Returns(Constant.Browser);
            mockHttpRequest.Setup(p => p.UserHostName).Returns("123");

            return mockHttpRequest.Object;
        }

        public static void SetUpEnviroment()
        {
            var request = new HttpRequest("111", "http://127.0.0.1:8080/controller/action", string.Empty);
            var test = Substitute.For<HttpBrowserCapabilities>();
            test.Type.Returns("");
            test.Version.Returns("");
            request.Browser = test;

            var response = new HttpResponse(new StringWriter());
            HttpContext.Current = new HttpContext(request, response);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                 new HttpStaticObjectsCollection(), 10, true,
                 HttpCookieMode.AutoDetect,
                 SessionStateMode.InProc, false);
            SessionStateUtility.AddHttpSessionStateToContext(HttpContext.Current, sessionContainer);
        }
    }
}
