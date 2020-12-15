using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Affiliate.Admin.Core.Repository.AdminRole;
using Fanex.Affiliate.Admin.Core.Services.AdminRole;
using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.AdminRole
{
    public class RolePermissionServiceTest
    {
        private readonly IRolePermissionRepository rolePermissionRepository;

        public RolePermissionServiceTest()
        {
            rolePermissionRepository = Substitute.For<IRolePermissionRepository>();
        }

        [Fact]
        public void GetPermissionInfoList_WhenCall_Excute()
        {
            // Arrange
            var service = CreateRolePermissionService;

            // Action
            service.GetPermissionInfoList();

            // Assert
            rolePermissionRepository.Received().GetPermissionInfoList();
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData(true, true)]
        public void UpdatePermissionInfo_WhenCall_Excute(bool isRead, bool isHost)
        {
            // Arrange
            var param = new UpdateRolePermissionParam
            {
                PermissionInfo = new List<RolePermissionParam> { new RolePermissionParam { FuncId = 1, IsRead = isRead, IsHost = isHost } },
                RoleId = 1
            };

            var service = CreateRolePermissionService;

            // Action
            service.UpdatePermissionInfo(param);

            // Assert
            rolePermissionRepository.Received().UpdatePermissionInfo(Arg.Any<UpdateRolePermissionParam>());
        }

        [Fact]
        public void UpdateMarketInfoList_WhenCall_Excute()
        {
            // Arrange
            var param = new UpdateMarketInfoParam
            {
                MarketInfoList = new List<MarketInfoParam> { new MarketInfoParam { RoleId = 1, MarketId = "en-US", HasPermission = true } }
            };

            var service = CreateRolePermissionService;

            // Action
            service.UpdateMarketInfoList(param);

            // Assert
            rolePermissionRepository.Received().UpdateMarketInfoList(Arg.Any<UpdateMarketInfoParam>());
        }

        private RolePermissionService CreateRolePermissionService => new RolePermissionService(rolePermissionRepository);
    }
}
