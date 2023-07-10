using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Repository.Domain;
using Fanex.Data;
using Moq;
using System;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Repository.Domain
{
    public class DomainControlServiceTest
    {
        [Fact]
        public void GetDomainControlList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var service = new Mock<DomainControlRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();

            service.Setup(r => r.GetObjectDb("Aff_Admin_DomainControl_Sel", It.IsAny<int>())).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetDomainControlList(string.Empty);

            // Assert
            objectDb.Verify(obj => obj.Query<DomainControlInfo>(It.IsAny<object>(), null), Times.Once);
        }

        [Fact]
        public void GetDomainControlById_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var service = new Mock<DomainControlRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();

            service.Setup(r => r.GetObjectDb("Aff_Admin_DomainControl_Sel", It.IsAny<int>())).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetDomainControlById(123);

            // Assert
            objectDb.Verify(obj => obj.QueryEntity<DomainControlInfo>(It.IsAny<object>(), null), Times.Once);
        }

        [Fact]
        public void InsertDomainControl_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var service = new Mock<DomainControlRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            service.Setup(re => re.GetObjectDb("Aff_Admin_DomainControl_Ins", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.InsertDomainControl(new InsertDomainControlParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void InsertDomainControl_WhenCallWithInvalidParam_InsertIsFailed()
        {
            // Arrange
            var message = "Insert Domain is failed.";
            var service = new Mock<DomainControlRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            service.Setup(re => re.GetObjectDb("Aff_Admin_DomainControl_Ins", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.InsertDomainControl(new InsertDomainControlParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void UpdateDomainControl_WhenCallWithValidParam_UpdateSuccessfully()
        {
            // Arrange
            var service = new Mock<DomainControlRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            service.Setup(re => re.GetObjectDb("Aff_Admin_DomainControl_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.UpdateDomainControl(new UpdateDomainControlParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateDomainControl_WhenCallWithInvalidParam_UpdateIsFailed()
        {
            // Arrange
            var message = "Update Domain is failed.";
            var service = new Mock<DomainControlRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            service.Setup(re => re.GetObjectDb("Aff_Admin_DomainControl_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.UpdateDomainControl(new UpdateDomainControlParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void DeleteDomainControl_WhenCallWithValidParam_DeleteSuccessfully()
        {
            // Arrange
            var service = new Mock<DomainControlRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            service.Setup(re => re.GetObjectDb("Aff_Admin_DomainControl_Del", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.DeleteDomainControl(new DeleteDomainControlParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DeleteDomainControl_WhenCallWithInvalidParam_DeleteIsFailed()
        {
            // Arrange
            var message = "Delete Domain is failed.";
            var service = new Mock<DomainControlRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            service.Setup(re => re.GetObjectDb("Aff_Admin_DomainControl_Del", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.DeleteDomainControl(new DeleteDomainControlParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }
    }
}
