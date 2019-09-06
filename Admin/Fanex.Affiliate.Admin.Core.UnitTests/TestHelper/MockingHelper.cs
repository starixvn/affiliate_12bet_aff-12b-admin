using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.UnitTests.TestHelper
{
    public static class MockingHelper
    {
        public static ISessionService GetSessionService()
        {
            return Mock.Of<ISessionService>(ss =>
                ss.AffId == Constant.AffId &&
                ss.AffName == Constant.AffName &&
                ss.OnUserId == Constant.OnUserId &&
                ss.UserId == Constant.UserId &&
                ss.UserPermissionList == new List<UserPermission>
                {
                    new UserPermission
                    {
                        IsHost = true,
                        FunctionName = Function.CommissionGroup
                    }
                }
            );
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
    }
}
