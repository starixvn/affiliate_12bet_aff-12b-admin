using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Affiliate.Admin.Core.Repository.AdminRole;
using Fanex.Affiliate.Admin.Core.Services.AdminRole;
using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.AdminRole
{
    public class AdminInfoServiceTest
    {
        private readonly IAdminInfoRepository adminInfoRepository;
        public AdminInfoServiceTest()
        {
            adminInfoRepository = Substitute.For<IAdminInfoRepository>();
        }

        [Fact]
        public void CreateUser_WhenCallWithValidParam_ExcuteWithCorrectData()
        {
            // Arrange
            var param = new AdminInfoParam
            {
                PermissionInfo = new List<RolePermissionParam> { new RolePermissionParam() },
                MarketList = new List<UserMarketParam> { new UserMarketParam() }
            };
            adminInfoRepository.CreateUser(Arg.Any<AdminInfoParam>()).Returns(new ResultInfo());
            var service = CreateAdminInfoService();

            // Action
            var result = service.CreateUser(param);

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void GetPermissionListByRole_WhenCall_RepoCalledWithCorrectParam()
        {
            // Arrange
            var service = CreateAdminInfoService();

            // Action
            service.GetPermissionListByRole(123);

            // Assert
            adminInfoRepository.Received().GetPermissionListByRole(123);
        }

        [Fact]
        public void GetUserInfo_WhenCall_RepoCalledWithCorrectParam()
        {
            // Arrange
            var service = CreateAdminInfoService();

            // Action
            service.GetUserInfo(123);

            // Assert
            adminInfoRepository.Received().GetUserInfo(123);
        }

        [Fact]
        public void UpdateUser_WhenCallWithValidParam_ExcuteWithCorrectData()
        {
            // Arrange
            var param = new AdminInfoParam
            {
                PermissionInfo = new List<RolePermissionParam> { new RolePermissionParam() },
                MarketList = new List<UserMarketParam> { new UserMarketParam() }
            };
            adminInfoRepository.UpdateUser(Arg.Any<AdminInfoParam>()).Returns(new ResultInfo());
            var service = CreateAdminInfoService();

            // Action
            var result = service.UpdateUser(param);

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void ChangePassword_WhenCall_RepoCalledWithCorrectParam()
        {
            // Arrange
            var param = new AdminInfoParam();
            var service = CreateAdminInfoService();

            // Action
            service.ChangePassword(param);

            // Assert
            adminInfoRepository.Received().UpdateUser(param);
        }

        [Fact]
        public void UpdateStatus_WhenCall_RepoCalledWithCorrectParam()
        {
            // Arrange
            var param = new AdminInfoParam();
            var service = CreateAdminInfoService();

            // Action
            service.UpdateStatus(param);

            // Assert
            adminInfoRepository.Received().UpdateUser(param);
        }

        private AdminInfoService CreateAdminInfoService()
        {
            return new AdminInfoService(adminInfoRepository);
        }
    }
}
