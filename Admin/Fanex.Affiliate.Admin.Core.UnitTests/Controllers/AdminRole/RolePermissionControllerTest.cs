using Fanex.Affiliate.Admin.Core.Controllers.AdminRole;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Affiliate.Admin.Core.Services.AdminRole;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.AdminRole
{
    public class RolePermissionControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var controller = CreateRolePermissionController();

            // Action
            var viewResult = controller.Index() as ViewResult;
            var viewName = viewResult.ViewName;

            // Assert
            Assert.Equal("~/Areas/Portal/Views/AdminRole/RolePermission.cshtml", viewName);
        }

        [Fact]
        public void GetPermissionInfoList_WhenCall_ReturnCorrectData()
        {
            //Arrange
            var service = new Mock<IRolePermissionService>();
            service.Setup(s => s.GetPermissionInfoList()).Returns(new List<RolePermissionInfo> { new RolePermissionInfo() });

            var controller = CreateRolePermissionController(null, service.Object);

            //Action
            var result = controller.GetPermissionInfoList() as JsonNetResult;
            var data = result.Data as List<RolePermissionInfo>;

            //Assert
            Assert.Single(data);
        }

        [Fact]
        public void UpdatePermissionInfo_WhenCallWithValidParam_UpdateSuccessfully()
        {
            //Arrange
            var service = new Mock<IRolePermissionService>();
            service.Setup(s => s.UpdatePermissionInfo(It.IsAny<UpdateRolePermissionParam>())).Returns(new ResultInfo());

            var controller = CreateRolePermissionController(null, service.Object);

            //Action
            var result = controller.UpdatePermissionInfo(new UpdateRolePermissionParam()) as JsonNetResult;
            var data = result.Data as ResultInfo;

            //Assert
            Assert.True(data.IsSuccessful);
        }

        [Fact]
        public void UpdatePermissionInfo_WhenCallWithInvalidParam_UpdateIsFailed()
        {
            //Arrange
            var controller = CreateRolePermissionController();
            controller.ModelState.AddModelError("Error01", "Invalid Param");

            //Action
            var result = controller.UpdatePermissionInfo(new UpdateRolePermissionParam()) as JsonNetResult;
            var data = result.Data as ResultInfo;

            //Assert
            Assert.False(data.IsSuccessful);
            Assert.Equal("Invalid Param", data.Message);
        }

        [Fact]
        public void UpdateMarketInfoList_WhenCallWithValidParam_UpdateSuccessfully()
        {
            //Arrange
            var service = new Mock<IRolePermissionService>();
            service.Setup(s => s.UpdateMarketInfoList(It.IsAny<UpdateMarketInfoParam>())).Returns(new ResultInfo());

            var controller = CreateRolePermissionController(null, service.Object);

            //Action
            var result = controller.UpdateMarketInfoList(new UpdateMarketInfoParam()) as JsonNetResult;
            var data = result.Data as ResultInfo;

            //Assert
            Assert.True(data.IsSuccessful);
        }

        [Fact]
        public void UpdateMarketInfoList_WhenCallWithInvalidParam_UpdateIsFailed()
        {
            //Arrange
            var controller = CreateRolePermissionController();
            controller.ModelState.AddModelError("Error01", "Invalid Param");

            //Action
            var result = controller.UpdateMarketInfoList(new UpdateMarketInfoParam()) as JsonNetResult;
            var data = result.Data as ResultInfo;

            //Assert
            Assert.False(data.IsSuccessful);
            Assert.Equal("Invalid Param", data.Message);
        }

        private RolePermissionController CreateRolePermissionController(
            ISessionService sessionService = null,
            IRolePermissionService rolePermissionService = null)
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeRolePermissionService = rolePermissionService ?? Mock.Of<IRolePermissionService>();

            var controller = new RolePermissionController(fakeSessionService, fakeRolePermissionService);

            var request = MockingHelper.GetHttpRequest();

            controller.ControllerContext = Mock.Of<ControllerContext>(c => c.HttpContext.Request == request);

            return controller;
        }
    }
}
