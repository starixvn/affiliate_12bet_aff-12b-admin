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
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.AdminRole
{
    public class AdminRoleControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var controller = CreateAdminRoleController();

            // Action
            var viewResult = controller.Index() as ViewResult;
            var viewName = viewResult.ViewName;

            // Assert
            Assert.Equal("", viewName);
        }

        [Fact]
        public void GetUserList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var adminUsersService = new Mock<IAdminListService>();
            adminUsersService.Setup(t => t.GetUserList(It.IsAny<string>())).Returns(new List<AdminListInfo> { new AdminListInfo() });
            var controller = CreateAdminRoleController(adminUsersService: adminUsersService.Object);

            // Action
            var result = controller.GetUserList(string.Empty) as JsonNetResult;
            var data = result.Data as List<AdminListInfo>;

            // Assert
            Assert.Single(data);
        }

        [Fact]
        public void GetUserPermissionList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var service = new Mock<IAdminInfoService>();
            service.Setup(t => t.GetUserInfo(It.IsAny<int>())).Returns(new AdminInforResultEntity { UserPermissionList = new List<UserPermissionInfo> { new UserPermissionInfo() } });
            var controller = CreateAdminRoleController(adminInfoService: service.Object);

            // Action
            var result = controller.GetUserPermissionList(1) as JsonNetResult;
            var data = result.Data as List<UserPermissionInfo>;

            // Assert
            Assert.Single(data);
        }

        [Fact]
        public void DeleteUser_WhenCallWithValidParam_UpdateSuccessful()
        {
            // Arrange
            var param = new DeleteAdminUsersParam { UserIds = "123" };
            DeleteAdminUsersParam passedParam = null;
            var adminUsersService = new Mock<IAdminListService>();
            adminUsersService.Setup(s => s.DeleteUsers(It.IsAny<DeleteAdminUsersParam>())).Returns(new ResultInfo()).Callback<DeleteAdminUsersParam>(p => passedParam = p);
            var controller = CreateAdminRoleController(adminUsersService: adminUsersService.Object);

            // Action
            var result = controller.DeleteUsers(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
            Assert.Equal(param.UserIds, passedParam.UserIds);
        }

        [Fact]
        public void DeleteUsers_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var controller = CreateAdminRoleController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.DeleteUsers(new DeleteAdminUsersParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("", resultInfo.Message);
        }

        private AdminRoleController CreateAdminRoleController(
        ISessionService sessionService = null,
        IAdminListService adminUsersService = null,
        IAdminInfoService adminInfoService = null
        )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeAdminUsersService = adminUsersService ?? Mock.Of<IAdminListService>();
            var fakeAdminInfoService = adminInfoService ?? Mock.Of<IAdminInfoService>();

            var controller = new AdminRoleController(fakeSessionService, fakeAdminUsersService, fakeAdminInfoService);

            var request = MockingHelper.GetHttpRequest();
            var server = new Mock<HttpServerUtilityBase>();
            server.Setup(s => s.HtmlEncode(It.IsAny<string>())).Returns<string>(s => $"Encoded{s}");

            controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
                ctx.HttpContext.Request == request &&
                ctx.HttpContext.Server == server.Object &&
                ctx.HttpContext.Session.SessionID == "SessionId"
            );

            return controller;
        }
    }
}
