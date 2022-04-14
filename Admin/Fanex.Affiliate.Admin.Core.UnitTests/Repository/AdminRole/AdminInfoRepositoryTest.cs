using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Affiliate.Admin.Core.Repository.AdminRole;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Repository.AdminRole
{
    public class AdminInfoRepositoryTest
    {

        [Fact]
        public void CreateUser_WhenCall_UpdateSuccessfully()
        {
            // Arrange
            var repository = new Mock<AdminInfoRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_AdminUser_Ins", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.CreateUser(new AdminInfoParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void GetUserInfo_WhenCall_ReturnCorrectAdminInfo()
        {
            // Arrange
            var repository = new Mock<AdminInfoRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetAdminInfo());
            objectDb.Setup(o => o.ExecuteReader(null, null)).Returns(dataReader.Object);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_AdminUser_Detail_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetUserInfo(1);

            // Assert
            Assert.NotNull(result.AdminInfo);
        }

        [Fact]
        public void GetUserInfo_WhenCall_ReturnCorrectMarketList()
        {
            // Arrange
            var repository = new Mock<AdminInfoRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false).Returns(true).Returns(false);
            dataReader.SetupSequence(d => d.NextResult()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetMarketList());
            objectDb.Setup(o => o.ExecuteReader(null, null)).Returns(dataReader.Object);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_AdminUser_Detail_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetUserInfo(1);

            // Assert
            Assert.NotEmpty(result.MarketList);
        }

        [Fact]
        public void GetUserInfo_WhenCall_ReturnCorrectUserPermissionList()
        {
            // Arrange
            var repository = new Mock<AdminInfoRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false).Returns(false).Returns(true);
            dataReader.SetupSequence(d => d.NextResult()).Returns(true).Returns(true);

            ReaderHelper.MapReaderData(dataReader, GetUserPermissionList());
            objectDb.Setup(o => o.ExecuteReader(null, null)).Returns(dataReader.Object);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_AdminUser_Detail_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetUserInfo(1);

            // Assert
            Assert.NotEmpty(result.UserPermissionList);
        }

        [Fact]
        public void UpdateUser_WhenCall_UpdateSuccessfully()
        {
            // Arrange
            var repository = new Mock<AdminInfoRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_AdminUser_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdateUser(new AdminInfoParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void GetPermissionListByRole_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<AdminInfoRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetUserPermissionInfoList());
            objectDb.Setup(o => o.ExecuteReader(null, null)).Returns(dataReader.Object);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_AdminUser_RolePermission_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetPermissionListByRole(1);
            var item = result.FirstOrDefault();

            // Assert
            Assert.Single(result);
            Assert.False(item.IsRead);
            Assert.False(item.IsHost);
            Assert.True(item.IsRoleRead);
            Assert.True(item.IsRoleHost);
        }

        private IDictionary<string, object> GetUserPermissionInfoList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["DisplayName"] = "Role Name",
                ["ParentId"] = 123,
                ["AllParentIds"] = "123",
                ["FuncId"] = 1,
                ["FuncName"] = "Func Name",
                ["Policy"] = 3
            };
        }

        private IDictionary<string, object> GetAdminInfo()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["CustId"] = 123,
                ["UserName"] = "User Name",
                ["FirstName"] = "First Name",
                ["LastName"] = "Last Name",
                ["RoleId"] = 1,
                ["Closed"] = false,
                ["IsRoleMarketDefault"] = true,
                ["IsRoleFunctionDefault"] = true
            };
        }

        private IDictionary<string, object> GetMarketList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["MarketId"] = 123,
                ["MarketName"] = "Market Name",
                ["Status"] = true
            };
        }

        private IDictionary<string, object> GetUserPermissionList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["DisplayName"] = "Role Name",
                ["ParentId"] = 123,
                ["AllParentIds"] = "123",
                ["FuncId"] = 1,
                ["FuncName"] = "Func Name",
                ["UserPolicy"] = 3,
                ["RolePolicy"] = 3
            };
        }
    }
}
