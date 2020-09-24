using Fanex.Affiliate.Admin.Core.Controllers.AdminRole;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Services.AdminRole;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.AdminRole
{
    public class AdminRoleCommonControllerTest
    {
        [Fact]
        public void GetRoleList_WhenCall_ReturnCorrectData()
        {
            //Arrange
            var service = new Mock<IAdminRoleCommonService>();
            service.Setup(s => s.GetRoleList()).Returns(new List<RoleInfo> { new RoleInfo() });

            var controller = CreateAdminRoleCommonController(null, service.Object);

            //Action
            var result = controller.GetRoleList() as JsonNetResult;
            var data = result.Data as List<RoleInfo>;

            //Assert
            Assert.Single(data);
        }

        [Fact]
        public void GetMarketListByUser_WhenCall_ReturnCorrectData()
        {
            //Arrange
            var service = new Mock<IAdminRoleCommonService>();
            service.Setup(s => s.GetMarketListByUser(It.IsAny<int?>())).Returns(new List<MarketInfo> { new MarketInfo() });

            var controller = CreateAdminRoleCommonController(null, service.Object);

            //Action
            var result = controller.GetMarketListByUser() as JsonNetResult;
            var data = result.Data as List<MarketInfo>;

            //Assert
            Assert.Single(data);
        }

        [Fact]
        public void GetMarketListByRole_WhenCall_ReturnCorrectData()
        {
            //Arrange
            var service = new Mock<IAdminRoleCommonService>();
            service.Setup(s => s.GetMarketListByRole(It.IsAny<int?>())).Returns(new List<MarketInfo> { new MarketInfo() });

            var controller = CreateAdminRoleCommonController(null, service.Object);

            //Action
            var result = controller.GetMarketListByRole() as JsonNetResult;
            var data = result.Data as List<MarketInfo>;

            //Assert
            Assert.Single(data);
        }

        private AdminRoleCommonController CreateAdminRoleCommonController (
            ISessionService sessionService = null,
            IAdminRoleCommonService adminRoleCommonService = null)
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeAdminRoleCommonService = adminRoleCommonService ?? Mock.Of<IAdminRoleCommonService>();

            return new AdminRoleCommonController(fakeSessionService, fakeAdminRoleCommonService);
        }
    }
}
