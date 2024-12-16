using Fanex.Affiliate.Admin.Core.Commands.Auth;
using Fanex.Affiliate.Admin.Core.Criterias.Auth;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.Login;
using Fanex.Affiliate.Admin.Core.Services.Auth;
using Fanex.Data.Repository;
using Moq;
using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Auth
{
    public class AuthServiceTest
    {
        [Fact]
        public void GetPermissionList_WhenCall_ReturnCorrectPermissionList()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Fetch<UserPermission>(It.IsAny<UserPermissionCriteria>())).Returns(new List<UserPermission>());

            // Action
            var service = new AuthService(dynamicRepository.Object);
            var result = service.GetPermissionList(123);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void ReCheckin_WhenCall_ReturnCorrect()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Execute(It.IsAny<ReCheckinCommand>()));

            // Action
            var service = new AuthService(dynamicRepository.Object);
            var result = service.ReCheckin(123);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Login_WhenCall_ReturnCorrect()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Execute(It.IsAny<LoginCommand>()));

            // Action
            var service = new AuthService(dynamicRepository.Object);
            var result = service.Login(new LoginParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void LoginOnUser_WhenCall_ReturnCorrect()
        {
            // Arrange
            var onUserId = 123456;
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Execute(It.IsAny<LoginOnUserCommand>())).Callback<LoginOnUserCommand>(c => c.OnUserId = onUserId);

            // Action
            var service = new AuthService(dynamicRepository.Object);
            var result = service.LoginOnUser(new LoginParam(), "123", "123");

            // Assert
            Assert.Equal(onUserId, result);
        }

        [Fact]
        public void Logout_WhenCall_Excuter()
        {
            // Arrange
            var dynamicRepository = Substitute.For<IDynamicRepository>();

            // Action
            var service = new AuthService(dynamicRepository);
            service.Logout(123);

            // Assert
            dynamicRepository.Received().Execute(Arg.Any<LogoutCommand>());
        }

        [Fact]
        public void CheckPermission_WhenCall_ReturnCorrect()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Get<ResultInfo>(It.IsAny<CheckUserPermissionCriteria>())).Returns(new ResultInfo());

            // Action
            var service = new AuthService(dynamicRepository.Object);
            var result = service.CheckPermission(new CheckPermissionParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }
    }
}
