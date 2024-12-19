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
            dynamicRepository.Setup(r => r.Fetch<DepartmentInfo>(It.IsAny<BaseCriteria>())).Returns(new List<DepartmentInfo> { new DepartmentInfo() });

            // Action
            var service = new DepartmentService(dynamicRepository.Object);
            var result = service.GetDepartmentList();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetDepartmentDetail_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Get<DepartmentInfo>(It.IsAny<DepartmentDetailCriteria>())).Returns(new DepartmentInfo());

            // Action
            var service = new DepartmentService(dynamicRepository.Object);
            var result = service.GetDepartmentDetail(123);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void InsertDepartment_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Execute(It.IsAny<DepartmentCommand>()));

            // Action
            var service = new DepartmentService(dynamicRepository.Object);
            var result = service.InsertDepartment(new InsertUpdateDepartmentParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateDepartment_WhenCallWithValidParam_UpdateSuccessfully()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Execute(It.IsAny<DepartmentCommand>()));

            // Action
            var service = new DepartmentService(dynamicRepository.Object);
            var result = service.UpdateDepartment(new InsertUpdateDepartmentParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DeleteDepartment_WhenCallWithValidParam_DeleteSuccessfully()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Execute(It.IsAny<DeleteDepartmentCommand>()));

            // Action
            var service = new DepartmentService(dynamicRepository.Object);
            var result = service.DeleteDepartment(new DeleteDepartmentParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void GetRolePolicy_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Fetch<PolicyInfo>(It.IsAny<RolePolicyCriteria>())).Returns(new List<PolicyInfo> { new PolicyInfo() });

            // Action
            var service = new DepartmentService(dynamicRepository.Object);
            var result = service.GetRolePolicy(123);

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void UpdateRolePolicy_WhenCallWithValidParam_UpdateSuccessfully()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Execute(It.IsAny<UpdateRolePolicyCommand>()));

            // Action
            var service = new DepartmentService(dynamicRepository.Object);
            var result = service.UpdateRolePolicy(new UpdatePolicyParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }
    }
}
