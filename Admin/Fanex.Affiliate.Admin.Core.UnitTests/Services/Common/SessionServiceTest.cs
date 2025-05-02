using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Common
{
    public class SessionServiceTest
    {
        [Fact]
        public void PropertyUserId_WhenCall_UserIdCorrectly()
        {
            MockingHelper.SetUpHttpContextSession();
            var service = CreateService();
            var userId = 123;

            service.UserId = userId;

            Assert.Equal(userId, service.UserId);
        }

        [Fact]
        public void PropertyUserId_WhenCall_ReturnZero()
        {
            MockingHelper.SetUpHttpContextSession();
            var service = CreateService();

            Assert.Equal(0, service.UserId);
        }

        [Fact]
        public void PropertyOnUserId_WhenCall_OnUserIdCorrectly()
        {
            MockingHelper.SetUpHttpContextSession();
            var service = CreateService();
            var onUserId = 123;

            service.OnUserId = onUserId;

            Assert.Equal(onUserId, service.OnUserId);
        }

        [Fact]
        public void PropertyOnUserId_WhenCall_ReturnZero()
        {
            MockingHelper.SetUpHttpContextSession();
            var service = CreateService();

            Assert.Equal(0, service.OnUserId);
        }

        [Fact]
        public void PropertyAffName_WhenCall_AffIdCorrectly()
        {
            MockingHelper.SetUpHttpContextSession();
            var service = CreateService();
            var affName = "abc";

            service.AffName = affName;

            Assert.Equal(affName, service.AffName);
        }

        [Fact]
        public void PropertyAffName_WhenCall_ReturnEmpty()
        {
            MockingHelper.SetUpHttpContextSession();
            var service = CreateService();

            Assert.Equal(string.Empty, service.AffName);
        }

        [Fact]
        public void PropertyUserPermissionList_WhenCall_UserPermissionListCorrectly()
        {
            MockingHelper.SetUpHttpContextSession();
            var service = CreateService();

            service.UserPermissionList = new List<UserPermission> { new UserPermission() };

            Assert.Single(service.UserPermissionList);
        }

        [Fact]
        public void PropertyUserPermissionList_WhenCall_ReturnEmpty()
        {
            MockingHelper.SetUpHttpContextSession();
            var service = CreateService();

            Assert.Empty(service.UserPermissionList);
        }

        [Fact]
        public void PropertyClaimIdentities_WhenSet_StoreCorrectly()
        {
            // Arrange
            MockingHelper.SetUpHttpContextSession();
            var service = CreateService();
            var claims = new List<ClaimsIdentity>
            {
                new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "test"),
                    new Claim(ClaimTypes.Role, "admin")
                })
            };

            // Action
            service.ClaimIdentities = claims;

            // Assert
            Assert.Equal(claims, service.ClaimIdentities);
        }

        [Fact]
        public void PropertyClaimIdentities_WhenNotSet_ReturnNull()
        {
            // Arrange
            MockingHelper.SetUpHttpContextSession();
            var service = CreateService();

            // Action & Assert
            Assert.Null(service.ClaimIdentities);
        }

        [Fact]
        public void PropertyUserClaim_WhenUserIsClaimsPrincipal_ReturnCorrectPrincipal()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "test"),
                new Claim(ClaimTypes.Role, "admin")
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            MockingHelper.SetUpHttpContextSession();
            HttpContext.Current.User = principal;
            var service = CreateService();

            // Action & Assert
            Assert.Equal(principal, service.UserClaim);
            Assert.Equal("test", service.UserClaim.FindFirst(ClaimTypes.Name).Value);
            Assert.Equal("admin", service.UserClaim.FindFirst(ClaimTypes.Role).Value);
        }

        [Fact]
        public void PropertyUserClaim_WhenUserIsNotClaimsPrincipal_ReturnNull()
        {
            // Arrange
            MockingHelper.SetUpHttpContextSession();
            HttpContext.Current.User = null;
            var service = CreateService();

            // Action & Assert
            Assert.Null(service.UserClaim);
        }

        [Fact]
        public void HasHostPermission_WhenUserHasHostPermission_ReturnTrue()
        {
            // Arrange
            MockingHelper.SetUpHttpContextSession();
            var service = CreateService();
            var permissions = new List<UserPermission>
            {
                new UserPermission { FunctionName = "TestFunction", IsHost = true }
            };
            service.UserPermissionList = permissions;

            // Action
            var result = service.HasHostPermission("TestFunction");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void HasHostPermission_WhenUserHasNoHostPermission_ReturnFalse()
        {
            // Arrange
            MockingHelper.SetUpHttpContextSession();
            var service = CreateService();
            var permissions = new List<UserPermission>
            {
                new UserPermission { FunctionName = "TestFunction", IsHost = false }
            };
            service.UserPermissionList = permissions;

            // Action
            var result = service.HasHostPermission("TestFunction");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void HasHostPermission_WhenFunctionDoesNotExist_ReturnFalse()
        {
            // Arrange
            MockingHelper.SetUpHttpContextSession();
            var service = CreateService();
            var permissions = new List<UserPermission>
            {
                new UserPermission { FunctionName = "OtherFunction", IsHost = true }
            };
            service.UserPermissionList = permissions;

            // Action
            var result = service.HasHostPermission("TestFunction");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void HasHostPermission_WhenPermissionListIsEmpty_ReturnFalse()
        {
            // Arrange
            MockingHelper.SetUpHttpContextSession();
            var service = CreateService();
            service.UserPermissionList = new List<UserPermission>();

            // Action
            var result = service.HasHostPermission("TestFunction");

            // Assert
            Assert.False(result);
        }

        private static SessionService CreateService() => new SessionService();
    }
}
