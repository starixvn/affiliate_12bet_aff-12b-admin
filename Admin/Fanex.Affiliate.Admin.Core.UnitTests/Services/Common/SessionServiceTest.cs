using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Web;
using System.Web.SessionState;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Common
{
    public class SessionServiceTest
    {
        [Fact]
        public void PropertyUserId_WhenCall_PropertyStoreCorrectly()
        {
            SetUpHttpContextSession();
            var service = CreateService();
            var userId = 12345;

            service.UserId = userId;

            Assert.Equal(userId, service.UserId);
        }

        [Fact]
        public void PropertyUserId_WhenCall_ReturnCorrectData()
        {
            SetUpHttpContextSession();
            var service = CreateService();

            Assert.Equal(0, service.UserId);
        }

        [Fact]
        public void PropertyOnUserId_WhenCall_PropertyStoreCorrectly()
        {
            SetUpHttpContextSession();
            var service = CreateService();
            var OnUserId = 12345;

            service.OnUserId = OnUserId;

            Assert.Equal(OnUserId, service.OnUserId);
        }

        [Fact]
        public void PropertyOnUserId_WhenCall_ReturnCorrectData()
        {
            SetUpHttpContextSession();
            var service = CreateService();

            Assert.Equal(0, service.OnUserId);
        }

        [Fact]
        public void PropertyAffId_WhenCall_PropertyStoreCorrectly()
        {
            SetUpHttpContextSession();
            var service = CreateService();
            var AffId = 12345;

            service.AffId = AffId;

            Assert.Equal(AffId, service.AffId);
        }

        [Fact]
        public void PropertyOnAffId_WhenCall_ReturnCorrectData()
        {
            SetUpHttpContextSession();
            var service = CreateService();

            Assert.Equal(0, service.AffId);
        }

        [Fact]
        public void PropertyAffName_WhenCall_PropertyStoreCorrectly()
        {
            SetUpHttpContextSession();
            var service = CreateService();
            var AffName = "12345";

            service.AffName = AffName;

            Assert.Equal(AffName, service.AffName);
        }

        [Fact]
        public void PropertyAffName_WhenCall_ReturnCorrectData()
        {
            SetUpHttpContextSession();
            var service = CreateService();

            Assert.Empty(service.AffName);
        }

        [Fact]
        public void PropertyClaimIdentities_WhenCall_PropertyStoreCorrectly()
        {
            SetUpHttpContextSession();
            var service = CreateService();
            var ClaimIdentities = new List<ClaimsIdentity> { new ClaimsIdentity(new List<Claim> { new Claim("Test", "Write") }) };

            service.ClaimIdentities = ClaimIdentities;

            Assert.Equal(ClaimIdentities, service.ClaimIdentities);
        }

        [Fact]
        public void PropertyUserClaim_WhenCall_ReturnCorrectData()
        {
            SetUpHttpContextSession();
            var service = CreateService();

            Assert.Null(service.UserClaim);
        }

        private static SessionService CreateService()
            => new SessionService();

        private static void SetUpHttpContextSession()
        {
            var request = new HttpRequest(null, "http://abco.com", null);
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
