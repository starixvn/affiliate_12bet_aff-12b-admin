using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Domain
{
    public class DomainServiceTest
    {
        [Fact]
        public void GetDomainList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var service = new Mock<DomainService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetDomainList());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            service.Setup(r => r.GetObjectDb("Aff_Admin_AffiliateDomain_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetDomainList(true);
            var item = result.FirstOrDefault();

            // Assert
            Assert.Single(result);
            Assert.Equal("10/25/2019", item.CreatedDateText);
        }

        [Fact]
        public void GetDomainList_WhenCall_ReturnCorrectDataHasNull()
        {
            // Arrange
            var service = new Mock<DomainService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetDomainListHasNull());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            service.Setup(r => r.GetObjectDb("Aff_Admin_AffiliateDomain_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetDomainList(true);
            var item = result.FirstOrDefault();

            // Assert
            Assert.Single(result);
            Assert.Null(item.CreatedDateText);
        }

        [Fact]
        public void GetDomainList_WhenCall_ReturnNullData()
        {
            // Arrange
            var service = new Mock<DomainService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false).Returns(false);

            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            service.Setup(r => r.GetObjectDb("Aff_Admin_12BETDomain_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetDomainList(false);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void InsertDomain_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var service = new Mock<DomainService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            service.Setup(re => re.GetObjectDb("Aff_Admin_AffiliateDomain_Ins", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.InsertDomain(new InsertUpdateDomainParam { IsAffDomain = true });

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
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            service.Setup(re => re.GetObjectDb("Aff_Admin_12BETDomain_Ins", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.InsertDomain(new InsertUpdateDomainParam());

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
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            service.Setup(re => re.GetObjectDb("Aff_Admin_AffiliateDomain_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.UpdateDomain(new InsertUpdateDomainParam { IsAffDomain = true });

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
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            service.Setup(re => re.GetObjectDb("Aff_Admin_12BETDomain_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.UpdateDomain(new InsertUpdateDomainParam());

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
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
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
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
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
                ["CreateDate"] = DBNull.Value
            };
        }
    }
}
