using Fanex.Affiliate.Admin.Core.MetaModels.Customer;
using Fanex.Affiliate.Admin.Core.Repository.Customer;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Repository.Customer
{
    public class CustomerLeftPanelRepositoryTest
    {
        private const string userAffName = "12BETAFF226";

        [Fact]
        public void GetAffiliateInfoOnLeftPanel_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<CustomerLeftPanelRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetAffiliateInfoOnLeftPanel());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_AffiliateInfo_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetAffiliateInfoOnLeftPanel(userAffName);

            // Assert
            Assert.Equal(1271808, result.AffId);
            Assert.Equal("11/24/2019 00:00:00", result.LastLoginDateText);
        }

        [Fact]
        public void GetAffiliateInfoOnLeftPanel_WhenCall_ReturnCorrectDataHasNull()
        {
            // Arrange
            var repository = new Mock<CustomerLeftPanelRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetAffiliateInfoOnLeftPanelHasNull());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_AffiliateInfo_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetAffiliateInfoOnLeftPanel(userAffName);

            // Assert
            Assert.Equal(1271808, result.AffId);
            Assert.Equal(string.Empty, result.LastLoginDateText);
        }

        [Fact]
        public void GetAffiliateInfoOnLeftPanel_WhenCall_ReturnNull()
        {
            // Arrange
            var repository = new Mock<CustomerLeftPanelRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false);

            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_AffiliateInfo_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetAffiliateInfoOnLeftPanel(userAffName);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetOnlineList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<CustomerLeftPanelRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetOnlineList());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_AffiliateOnlineList_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetOnlineList(userAffName);
            var item = result.FirstOrDefault();

            // Assert
            Assert.Single(result);
            Assert.Equal("11/24/2019 00:00:00", item.ReloadTimeText);
        }

        [Fact]
        public void GetOnlineList_WhenCall_ReturnNull()
        {
            // Arrange
            var repository = new Mock<CustomerLeftPanelRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false);

            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_AffiliateOnlineList_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetOnlineList(userAffName);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void UpdateAffType_WhenCallWithInvalidParam_UpdateSuccessfully()
        {
            // Arrange
            var repository = new Mock<CustomerLeftPanelRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_AffiliateAffType_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdateAffType(new UpdateAffTypeParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateAffType_WhenCallWithValidParam_UpdateIsFailed()
        {
            // Arrange
            var repository = new Mock<CustomerLeftPanelRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_AffiliateAffType_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdateAffType(new UpdateAffTypeParam());

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void UpdateClosedStatus_WhenCallWithInvalidParam_UpdateSuccessfully()
        {
            // Arrange
            var repository = new Mock<CustomerLeftPanelRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_AffiliateClosed_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdateClosedStatus(new UpdateClosedStatusParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateClosedStatus_WhenCallWithValidParam_UpdateIsFailed()
        {
            // Arrange
            var message = "Update is failed.";
            var repository = new Mock<CustomerLeftPanelRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_AffiliateClosed_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdateClosedStatus(new UpdateClosedStatusParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void UpdateLanguage_WhenCallWithInvalidParam_UpdateSuccessfully()
        {
            // Arrange
            var repository = new Mock<CustomerLeftPanelRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_AffiliatePreferredLanguage_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdateLanguage(new UpdateLanguageParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateLanguage_WhenCallWithValidParam_UpdateIsFailed()
        {
            // Arrange
            var message = "Update language is failed.";
            var repository = new Mock<CustomerLeftPanelRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_AffiliatePreferredLanguage_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdateLanguage(new UpdateLanguageParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void UpdateStatus_WhenCallWithInvalidParam_UpdateSuccessfully()
        {
            // Arrange
            var repository = new Mock<CustomerLeftPanelRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_AffiliateStatus_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdateStatus(new UpdateStatusParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateStatus_WhenCallWithValidParam_UpdateIsFailed()
        {
            // Arrange
            var message = "Update status is failed.";
            var repository = new Mock<CustomerLeftPanelRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_AffiliateStatus_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdateStatus(new UpdateStatusParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        private IDictionary<string, object> GetAffiliateInfoOnLeftPanel()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["AffId"] = 1271808,
                ["GroupId"] = 100,
                ["UserName"] = "12BETAFF226",
                ["FirstName"] = "Hubert",
                ["LastName"] = "Duong",
                ["PreferredLanguage"] = "en-US",
                ["AffType"] = 4,
                ["BalBroughtForward"] = 123,
                ["GroupName"] = "Test",
                ["ContactApps"] = "test",
                ["Gender"] = true,
                ["Closed"] = false,
                ["LastLoginDate"] = new DateTime(2019, 11, 24),
                ["LastLoginIp"] = "::1",
                ["MoreSiteUrls"] = "test",
                ["SiteUrl"] = "test.com",
                ["NoSiteDesc"] = "tét",
                ["Status"] = "Approved",
                ["Phone"] = "0123456789"
            };
        }

        private IDictionary<string, object> GetAffiliateInfoOnLeftPanelHasNull()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["AffId"] = 1271808,
                ["GroupId"] = 100,
                ["UserName"] = "12BETAFF226",
                ["FirstName"] = "Hubert",
                ["LastName"] = "Duong",
                ["PreferredLanguage"] = "en-US",
                ["AffType"] = 4,
                ["BalBroughtForward"] = 123,
                ["GroupName"] = "Test",
                ["ContactApps"] = "test",
                ["Gender"] = true,
                ["Closed"] = false,
                ["LastLoginDate"] = DBNull.Value,
                ["LastLoginIp"] = "::1",
                ["MoreSiteUrls"] = "test",
                ["SiteUrl"] = "test.com",
                ["NoSiteDesc"] = "tét",
                ["Status"] = "Approved",
                ["Phone"] = "0123456789"
            };
        }

        private IDictionary<string, object> GetOnlineList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["Ip"] = "::1",
                ["ReloadTime"] = new DateTime(2019, 11, 24)
            };
        }
    }
}
