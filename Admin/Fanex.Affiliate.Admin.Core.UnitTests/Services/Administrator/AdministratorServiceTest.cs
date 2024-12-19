using Fanex.Affiliate.Admin.Core.Commands.Administrator;
using Fanex.Affiliate.Admin.Core.Criterias.Administrator;
using Fanex.Affiliate.Admin.Core.DTO.Administrator;
using Fanex.Affiliate.Admin.Core.MetaModels.Administrator;
using Fanex.Affiliate.Admin.Core.Services.Administrator;
using Fanex.Data.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Administrator
{
    public class AdministratorServiceTest
    {
        [Fact]
        public void GetUserList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Fetch<AdministratorInfo>(It.IsAny<AdministratorCriteria>())).Returns(new List<AdministratorInfo> { new AdministratorInfo { CreatedDate = new DateTime(2019, 11, 6) } });

            // Action
            var service = new AdministratorService(dynamicRepository.Object);
            var result = service.GetUserList(0, "", "");
            var item = result.FirstOrDefault();

            // Assert
            Assert.Single(result);
            Assert.Equal("11/06/2019 00:00:00", item.CreatedDateText);
        }

        [Fact]
        public void GetUserList_WhenCallWithHasGroupId_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Fetch<AdministratorInfo>(It.IsAny<AdministratorCriteria>())).Returns(new List<AdministratorInfo> { new AdministratorInfo() });

            // Action
            var service = new AdministratorService(dynamicRepository.Object);
            var result = service.GetUserList(123, "", "0-9");
            var item = result.FirstOrDefault();

            // Assert
            Assert.Single(result);
            Assert.Null(item.CreatedDateText);
        }

        [Fact]
        public void GetUserDetail_WhenCall_ReturnCorrectData()
        {

            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Get<AdministratorInfo>(It.IsAny<AdministratorDetailCriteria>())).Returns(new AdministratorInfo());

            // Action
            var service = new AdministratorService(dynamicRepository.Object);
            var result = service.GetUserDetail(123);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetAccountPolicyList_WhenCall_ReturnCorrectData()
        {

            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Fetch<PolicyInfo>(It.IsAny<PoliciesByGroupIdsCriteria>())).Returns(new List<PolicyInfo> { new PolicyInfo() });

            // Action
            var service = new AdministratorService(dynamicRepository.Object);
            var result = service.GetAccountPolicyList(123);

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetPolicyListByAccountIds_WhenCall_ReturnCorrectData()
        {

            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Fetch<PolicyInfo>(It.IsAny<PoliciesByGroupIdsCriteria>())).Returns(new List<PolicyInfo> { new PolicyInfo() });

            // Action
            var service = new AdministratorService(dynamicRepository.Object);
            var result = service.GetPolicyListByAccountIds("123");

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void InsertUser_WhenCall_InsertSuccessfully()
        {

            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Execute(It.IsAny<InsertAccountCommand>()));

            // Action
            var service = new AdministratorService(dynamicRepository.Object);
            var result = service.InsertUser(new InsertUserParam());

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void InsertExistedUser_WhenCall_UpdateSuccessfully()
        {

            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Execute(It.IsAny<InsertExistedAccountCommand>()));

            // Action
            var service = new AdministratorService(dynamicRepository.Object);
            var result = service.InsertExistedUser(new InsertExistedUserParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateUser_WhenCall_UpdateSuccessfully()
        {

            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Execute(It.IsAny<UpdateAccountCommand>()));

            // Action
            var service = new AdministratorService(dynamicRepository.Object);
            var result = service.UpdateUser(new UpdateUserParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void ChangeStatus_WhenCall_DeleteSuccessfully()
        {

            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Execute(It.IsAny<ChangeAccountStatusCommand>()));

            // Action
            var service = new AdministratorService(dynamicRepository.Object);
            var result = service.ChangeStatus(new ChangeStatusParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateAccountPolicy_WhenCall_UpdateSuccessfully()
        {

            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Execute(It.IsAny<UpdateAccountPolicyCommand>()));

            // Action
            var service = new AdministratorService(dynamicRepository.Object);
            var result = service.UpdateAccountPolicy(new UpdatePolicyParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }
        [Fact]
        public void ChangePassword_WhenCall_UpdateSuccessfully()
        {

            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Execute(It.IsAny<ChangeAccountPasswordCommand>()));

            // Action
            var service = new AdministratorService(dynamicRepository.Object);
            var result = service.ChangePassword(new UpdatePasswordParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }
    }
}
