using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Repository.AdminRole;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.AdminRole;
using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services
{
    public class AuthServiceTest
    {
        private readonly IAuthRepository authRepository;
        private readonly IAdminInfoRepository adminInfoRepository;
        private readonly IAdminRoleCommonService roleService;

        public AuthServiceTest()
        {
            authRepository = Substitute.For<IAuthRepository>();
            adminInfoRepository = Substitute.For<IAdminInfoRepository>();
            roleService = Substitute.For<IAdminRoleCommonService>();
        }

        [Fact]
        public void GetClaims_WhenCall_Excute()
        {
            // Arrange
            adminInfoRepository.GetUserInfo(Arg.Any<int>()).Returns(new AdminInforResultEntity
            {
                AdminInfo = new AdminInfo
                {
                    UserId = 123,
                    UserName = "UserName",
                    RoleId = 1
                },
                MarketList = new List<MarketInfo> { new MarketInfo { MarketId = "en-US", MarketName = "English", HasPermission = true } }
            });

            authRepository.GetPermissionList(Arg.Any<int>()).Returns(new List<UserPermission> {
                new UserPermission { FunctionName = "[Test]", IsHost = true },
                new UserPermission { FunctionName = "[Abc]", IsHost = false }
            });

            roleService.GetRoleList().Returns(new List<RoleInfo>
            {
                new RoleInfo { RoleId = 1, RoleName = "Management" }
            });
            var service = CreateAuthService;

            // Action
            var claims = service.GetClaims(123);

            // Assert
            Assert.NotEmpty(claims);
            adminInfoRepository.Received().GetUserInfo(123);
            authRepository.Received().GetPermissionList(123);
        }

        [Fact]
        public void IsUserHasPermissionOnAff_WhenCall_Excute()
        {
            // Arrange
            var service = CreateAuthService;

            // Action
            service.IsUserHasPermissionOnAff(123, 234);

            // Assert
            authRepository.Received().CheckIfAffBeInChargeByUser(123, 234);
        }

        private AuthService CreateAuthService
            => new AuthService(authRepository, adminInfoRepository, roleService);
    }
}
