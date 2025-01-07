using Fanex.Affiliate.Admin.Core.Commands.Administrator;
using Fanex.Affiliate.Admin.Core.Criterias;
using Fanex.Affiliate.Admin.Core.Criterias.Administrator;
using Fanex.Affiliate.Admin.Core.DTO.Administrator;
using Fanex.Affiliate.Admin.Core.MetaModels.Administrator;
using Fanex.Affiliate.Admin.Core.Services.Administrator;
using Fanex.Data.Repository;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Administrator
{
    public class DepartmentServiceTest
    {
        [Fact]
        public void GetDepartmentList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Fetch<DepartmentInfo>(It.IsAny<BaseCriteria>(), It.IsAny<string>())).Returns(new List<DepartmentInfo> { new DepartmentInfo() });

            // Action
            var service = new DepartmentService(dynamicRepository.Object);
            var result = service.GetDepartmentList();

            // Assert
            dynamicRepository.Verify(d => d.Fetch<DepartmentInfo>(It.Is<BaseCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.Single(result);
        }

        [Fact]
        public void GetDepartmentDetail_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Get<DepartmentInfo>(It.IsAny<DepartmentDetailCriteria>(), It.IsAny<string>())).Returns(new DepartmentInfo());

            // Action
            var service = new DepartmentService(dynamicRepository.Object);
            var result = service.GetDepartmentDetail(123);

            // Assert
            dynamicRepository.Verify(d => d.Get<DepartmentInfo>(It.Is<DepartmentDetailCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.NotNull(result);
        }

        [Fact]
        public void InsertDepartment_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Execute(It.IsAny<DepartmentCommand>(), It.IsAny<string>()));

            // Action
            var service = new DepartmentService(dynamicRepository.Object);
            var result = service.InsertDepartment(new InsertUpdateDepartmentParam());

            // Assert
            dynamicRepository.Verify(d => d.Execute(It.Is<DepartmentCommand>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateDepartment_WhenCallWithValidParam_UpdateSuccessfully()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Execute(It.IsAny<DepartmentCommand>(), It.IsAny<string>()));

            // Action
            var service = new DepartmentService(dynamicRepository.Object);
            var result = service.UpdateDepartment(new InsertUpdateDepartmentParam());

            // Assert
            dynamicRepository.Verify(d => d.Execute(It.Is<DepartmentCommand>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DeleteDepartment_WhenCallWithValidParam_DeleteSuccessfully()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Execute(It.IsAny<DeleteDepartmentCommand>(), It.IsAny<string>()));

            // Action
            var service = new DepartmentService(dynamicRepository.Object);
            var result = service.DeleteDepartment(new DeleteDepartmentParam());

            // Assert
            dynamicRepository.Verify(d => d.Execute(It.Is<DeleteDepartmentCommand>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && !c.IsValid()), It.IsAny<string>()));
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void GetRolePolicy_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Fetch<PolicyInfo>(It.IsAny<RolePolicyCriteria>(), It.IsAny<string>())).Returns(new List<PolicyInfo> { new PolicyInfo() });

            // Action
            var service = new DepartmentService(dynamicRepository.Object);
            var result = service.GetRolePolicy(123);

            // Assert
            dynamicRepository.Verify(d => d.Fetch<PolicyInfo>(It.Is<RolePolicyCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.Single(result);
        }

        [Fact]
        public void UpdateRolePolicy_WhenCallWithValidParam_UpdateSuccessfully()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Execute(It.IsAny<UpdateRolePolicyCommand>(), It.IsAny<string>()));

            // Action
            var service = new DepartmentService(dynamicRepository.Object);
            var result = service.UpdateRolePolicy(new UpdatePolicyParam());

            // Assert
            dynamicRepository.Verify(d => d.Execute(It.Is<UpdateRolePolicyCommand>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.True(result.IsSuccessful);
        }
    }
}
