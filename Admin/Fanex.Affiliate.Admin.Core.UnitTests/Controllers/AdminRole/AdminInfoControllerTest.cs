using Fanex.Affiliate.Admin.Core.Controllers.AdminRole;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Affiliate.Admin.Core.Services;
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
    public class AdminInfoControllerTest
    {
        [Fact]
        public void GetUserInfo_WhenCall_ReturnCorrectData()
        {
            //Arrange
            var service = new Mock<IAdminInfoService>();
            service.Setup(s => s.GetUserInfo(1)).Returns(new AdminInforResultEntity { MarketList = new List<MarketInfo> { new MarketInfo() } });

            var controller = CreateAdminInfoController(null, null, service.Object);

            //Action
            var result = controller.GetUserInfo(1) as JsonNetResult;
            var data = result.Data as AdminInforResultEntity;

            //Assert
            Assert.Single(data.MarketList);
        }

        [Fact]
        public void GetPermissionListByRole_WhenCall_ReturnCorrectData()
        {
            //Arrange
            var service = new Mock<IAdminInfoService>();
            service.Setup(s => s.GetPermissionListByRole(1)).Returns(new List<UserPermissionInfo> { new UserPermissionInfo() });

            var controller = CreateAdminInfoController(null, null, service.Object);

            //Action
            var result = controller.GetPermissionListByRole(1) as JsonNetResult;
            var data = result.Data as List<UserPermissionInfo>;

            //Assert
            Assert.Single(data);
        }

        [Fact]
        public void CreateUser_WhenCallWithValidParam_CreateSuccessfully()
        {
            //Arrange
            var service = new Mock<IAdminInfoService>();
            service.Setup(s => s.CreateUser(It.IsAny<AdminInfoParam>())).Returns(new ResultInfo());

            var controller = CreateAdminInfoController(null, null, service.Object);

            //Action
            var result = controller.CreateUser(new AdminInfoParam { UserName = "ABC", Password = "123abc", FirstName = "A", LastName = "C" }) as JsonNetResult;
            var data = result.Data as ResultInfo;

            //Assert
            Assert.True(data.IsSuccessful);
        }

        [Fact]
        public void CreateUser_WhenCallWithInvalidParam_CreateIsFailed()
        {
            //Arrange
            var controller = CreateAdminInfoController();
            controller.ModelState.AddModelError("Error01", "Invalid Param");

            //Action
            var result = controller.CreateUser(new AdminInfoParam()) as JsonNetResult;
            var data = result.Data as ResultInfo;

            //Assert
            Assert.False(data.IsSuccessful);
            Assert.Equal("Invalid Param", data.Message);
        }

        [Fact]
        public void UpdateUser_WhenCallWithValidParam_UpdateSuccessfully()
        {
            //Arrange
            var service = new Mock<IAdminInfoService>();
            service.Setup(s => s.UpdateUser(It.IsAny<AdminInfoParam>())).Returns(new ResultInfo());

            var controller = CreateAdminInfoController(null, null, service.Object);

            //Action
            var result = controller.UpdateUser(new AdminInfoParam { UserId = Constant.UserId, UserName = "ABC", FirstName = "A", LastName = "C" }) as JsonNetResult;
            var data = result.Data as ResultInfo;

            //Assert
            Assert.True(data.IsSuccessful);
        }

        [Fact]
        public void UpdateUser_WhenCallWithInvalidParam_UpdateIsFailed()
        {
            //Arrange
            var controller = CreateAdminInfoController();
            controller.ModelState.AddModelError("Error01", "Invalid Param");

            //Action
            var result = controller.UpdateUser(new AdminInfoParam()) as JsonNetResult;
            var data = result.Data as ResultInfo;

            //Assert
            Assert.False(data.IsSuccessful);
            Assert.Equal("Invalid Param", data.Message);
        }

        [Fact]
        public void UpdateStatus_WhenCallWithValidParam_UpdateSuccessfully()
        {
            //Arrange
            var service = new Mock<IAdminInfoService>();
            service.Setup(s => s.UpdateStatus(It.IsAny<AdminInfoParam>())).Returns(new ResultInfo());

            var controller = CreateAdminInfoController(null, null, service.Object);

            //Action
            var result = controller.UpdateStatus(new AdminInfoParam { UserId = 123 }) as JsonNetResult;
            var data = result.Data as ResultInfo;

            //Assert
            Assert.True(data.IsSuccessful);
        }

        [Fact]
        public void UpdateStatus_WhenCallWithInvalidParam_UpdateIsFailed()
        {
            //Arrange
            var controller = CreateAdminInfoController();

            //Action
            var result = controller.UpdateStatus(new AdminInfoParam()) as JsonNetResult;
            var data = result.Data as ResultInfo;

            //Assert
            Assert.False(data.IsSuccessful);
            Assert.Equal("User is invalid!", data.Message);
        }

        [Fact]
        public void ChangePassword_WhenCallWithValidParam_UpdateSuccessfully()
        {
            //Arrange
            var service = new Mock<IAdminInfoService>();
            service.Setup(s => s.ChangePassword(It.IsAny<AdminInfoParam>())).Returns(new ResultInfo());

            var controller = CreateAdminInfoController(null, null, service.Object);

            //Action
            var result = controller.ChangePassword(new UpdatePasswordParam { Password = "123abc" }) as JsonNetResult;
            var data = result.Data as ResultInfo;

            //Assert
            Assert.True(data.IsSuccessful);
        }

        [Fact]
        public void ChangePassword_WhenCallWithInvalidParam_UpdateIsFailed()
        {
            //Arrange
            var controller = CreateAdminInfoController();
            controller.ModelState.AddModelError("Error01", "Invalid Param");

            //Action
            var result = controller.ChangePassword(new UpdatePasswordParam()) as JsonNetResult;
            var data = result.Data as ResultInfo;

            //Assert
            Assert.False(data.IsSuccessful);
            Assert.Equal("Invalid Param", data.Message);
        }

        private AdminInfoController CreateAdminInfoController(
            IAuthService authService = null,
            ISessionService sessionService = null,
            IAdminInfoService adminInfoService = null
            )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeAuthService = authService ?? Mock.Of<IAuthService>();
            var fakeAdminInfoService = adminInfoService ?? Mock.Of<IAdminInfoService>();
            var controller = new AdminInfoController(fakeAuthService, fakeSessionService, fakeAdminInfoService);

            var request = MockingHelper.GetHttpRequest();
            var server = new Mock<HttpServerUtilityBase>();
            server.Setup(s => s.HtmlEncode(It.IsAny<string>())).Returns<string>(s => $"Encoded{s}");

            controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
                ctx.HttpContext.Request == request &&
                ctx.HttpContext.Server == server.Object
            );

            return controller;
        }
    }
}
