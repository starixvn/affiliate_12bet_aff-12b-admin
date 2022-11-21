using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using Fanex.Data;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Domain
{
    public class DomainServiceTest
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetDomainList_WhenCall_ReturnCorrectData(bool isAffDomain)
        {
            // Arrange
            var service = new Mock<DomainService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();

            service.Setup(r => r.GetObjectDb(isAffDomain ? "Aff_Admin_AffiliateDomain_Sel" : "Aff_Admin_12BETDomain_Sel", It.IsAny<int>())).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetDomainList(123, isAffDomain);

            // Assert
            objectDb.Verify(obj => obj.Query<DomainInfo>(It.IsAny<object>(), null), Times.Once);
        }

        [Fact]
        public void InsertDomain_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var service = new Mock<DomainService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            service.Setup(re => re.GetObjectDb("Aff_Admin_AffiliateDomain_Ins", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.InsertDomain(new InsertDomainParam { IsAffDomain = true });

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void InsertDomain_WhenCallWithInvalidParam_InsertIsFailed()
        {
            // Arrange
            var message = "Insert Domain is failed.";
            var service = new Mock<DomainService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            service.Setup(re => re.GetObjectDb("Aff_Admin_12BETDomain_Ins", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.InsertDomain(new InsertDomainParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void UpdateDomain_WhenCallWithValidParam_UpdateSuccessfully()
        {
            // Arrange
            var service = new Mock<DomainService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            service.Setup(re => re.GetObjectDb("Aff_Admin_AffiliateDomain_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.UpdateDomain(new UpdateDomainParam { IsAffDomain = true });

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateDomain_WhenCallWithInvalidParam_UpdateIsFailed()
        {
            // Arrange
            var message = "Update Domain is failed.";
            var service = new Mock<DomainService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            service.Setup(re => re.GetObjectDb("Aff_Admin_12BETDomain_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.UpdateDomain(new UpdateDomainParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void DeleteDomain_WhenCallWithValidParam_DeleteSuccessfully()
        {
            // Arrange
            var service = new Mock<DomainService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            service.Setup(re => re.GetObjectDb("Aff_Admin_AffiliateDomain_Del", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.DeleteDomain(new DeleteDomainParam { IsAffDomain = true });

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DeleteDomain_WhenCallWithInvalidParam_DeleteIsFailed()
        {
            // Arrange
            var message = "Delete Domain is failed.";
            var service = new Mock<DomainService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            service.Setup(re => re.GetObjectDb("Aff_Admin_12BETDomain_Del", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.DeleteDomain(new DeleteDomainParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        private IDictionary<string, object> GetDomainList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["DomainId"] = 123,
                ["DomainName"] = "abc.com",
                ["IsSSL"] = true,
                ["Creator"] = "sa",
                ["BlockMarket"] = "",
                ["CreateDate"] = new DateTime(2019, 10, 25)
            };
        }

        private IDictionary<string, object> GetDomainListHasNull()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["DomainId"] = 123,
                ["DomainName"] = "abc.com",
                ["IsSSL"] = true,
                ["Creator"] = "sa",
                ["BlockMarket"] = "sa",
                ["CreateDate"] = DBNull.Value
            };
        }
    }
}
