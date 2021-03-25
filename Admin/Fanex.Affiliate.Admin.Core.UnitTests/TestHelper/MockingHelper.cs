using Fanex.Affiliate.Admin.Core.Services.Common;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Claims;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.UnitTests.TestHelper
{
    public static class MockingHelper
    {
        public static ISessionService GetSessionService(string claim = null, bool hasClaim = false, bool isHost = false)
        {
            return Mock.Of<ISessionService>(ss =>
                ss.AffId == Constant.AffId &&
                ss.AffName == Constant.AffName &&
                ss.OnUserId == Constant.OnUserId &&
                ss.UserId == Constant.UserId &&
                ss.ClaimIdentities == new List<ClaimsIdentity> { new ClaimsIdentity(new List<Claim> { new Claim(hasClaim ? claim : string.Empty, isHost ? "Write" : "Read") }) }
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
    }
}
