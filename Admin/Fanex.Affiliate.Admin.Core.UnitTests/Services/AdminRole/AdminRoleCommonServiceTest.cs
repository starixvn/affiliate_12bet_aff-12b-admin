using Fanex.Affiliate.Admin.Core.Services.AdminRole;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.AdminRole
{
    public class AdminRoleCommonServiceTest
    {
        [Fact]
        public void GetRoleList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var service = new Mock<AdminRoleCommonService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetRoleList());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            service.Setup(r => r.GetObjectDb("Aff_Admin_AdminUser_Role_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetRoleList();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetMarketListByUser_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var service = new Mock<AdminRoleCommonService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetMarketListByUser());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            service.Setup(r => r.GetObjectDb("Aff_Admin_AdminUser_Market_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetMarketListByUser();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetMarketListByRole_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var service = new Mock<AdminRoleCommonService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetMarketListByRole());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            service.Setup(r => r.GetObjectDb("Aff_Admin_AdminUser_RoleMarket_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetMarketListByRole();

            // Assert
            Assert.Single(result);
        }

        private IDictionary<string, object> GetRoleList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["RoleId"] = 1,
                ["RoleName"] = "Role Name"
            };
        }

        private IDictionary<string, object> GetMarketListByUser()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["MarketId"] = 1,
                ["MarketName"] = "Market Name",
                ["Status"] = true
            };
        }

        private IDictionary<string, object> GetMarketListByRole()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["RoleId"] = 1,
                ["MarketId"] = 1,
                ["MarketName"] = "Market Name",
                ["Status"] = true
            };
        }
    }
}
