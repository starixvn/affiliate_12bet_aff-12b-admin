using Fanex.Affiliate.Admin.Core.Commands.Auth;
using Fanex.Affiliate.Admin.Core.Criterias.Auth;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.Login;
using Fanex.Affiliate.Admin.Core.Services.Auth;
using Fanex.Data.Repository;
using Moq;
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
            dynamicRepository.Setup(re => re.Fetch<UserPermission>(It.IsAny<UserPermissionCriteria>(), It.IsAny<string>())).Returns(new List<UserPermission>());

            // Action
            var service = new AuthService(dynamicRepository.Object);
            var result = service.GetPermissionList(123);

            // Assert
            dynamicRepository.Verify(d => d.Fetch<UserPermission>(It.Is<UserPermissionCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.Empty(result);
        }

        [Fact]
        public void ReCheckin_WhenCall_ReturnCorrect()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Execute(It.IsAny<ReCheckinCommand>(), It.IsAny<string>()));

            // Action
            var service = new AuthService(dynamicRepository.Object);
            var result = service.ReCheckin(123);

            // Assert
            dynamicRepository.Verify(d => d.Execute(It.Is<ReCheckinCommand>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.False(result);
        }

        [Fact]
        public void Login_WhenCall_ReturnCorrect()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Execute(It.IsAny<LoginCommand>(), It.IsAny<string>()));

            // Action
            var service = new AuthService(dynamicRepository.Object);
            var result = service.Login(new LoginParam());

            // Assert
            dynamicRepository.Verify(d => d.Execute(It.Is<LoginCommand>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void LoginOnUser_WhenCall_ReturnCorrect()
        {
            // Arrange
            var onUserId = 123456;
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Execute(It.IsAny<LoginOnUserCommand>(), It.IsAny<string>())).Callback<INonQueryCommand, string>((c, _) => (c as LoginOnUserCommand).OnUserId = onUserId);

            // Action
            var service = new AuthService(dynamicRepository.Object);
            var result = service.LoginOnUser(new LoginParam(), "123", "123");

            // Assert
            dynamicRepository.Verify(d => d.Execute(It.Is<LoginOnUserCommand>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.Equal(onUserId, result);
        }

        [Fact]
        public void Logout_WhenCall_Excuter()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();

            // Action
            var service = new AuthService(dynamicRepository.Object);
            service.Logout(123);

            // Assert
            dynamicRepository.Verify(d => d.Execute(It.Is<LogoutCommand>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
        }

        [Fact]
        public void CheckPermission_WhenCall_ReturnCorrect()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Execute(It.IsAny<CheckUserPermissionCommand>(), It.IsAny<string>()));

            // Action
            var service = new AuthService(dynamicRepository.Object);
            var result = service.CheckPermission(new CheckPermissionParam());

            // Assert
            dynamicRepository.Verify(d => d.Execute(It.Is<CheckUserPermissionCommand>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && !c.IsValid()), It.IsAny<string>()));
            Assert.True(result.IsSuccessful);
        }
    }
}
