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
    public class RolePermissionRepositoryTest
    {
        [Fact]
        public void GetPermissionInfoList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<RolePermissionRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetPermissionInfoList());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_AdminUser_RolePermission_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetPermissionInfoList();
            var item = result.FirstOrDefault();

            // Assert
            Assert.Single(result);
            Assert.True(item.IsRead);
            Assert.True(item.IsHost);
        }

        [Fact]
        public void UpdatePermissionInfo_WhenCall_UpdateSuccessfully()
        {
            // Arrange
            var repository = new Mock<RolePermissionRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_AdminUser_RolePermission_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdatePermissionInfo(new UpdateRolePermissionParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateMarketInfoList_WhenCall_UpdateSuccessfully()
        {
            // Arrange
            var repository = new Mock<RolePermissionRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_AdminUser_RoleMarket_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdateMarketInfoList(new UpdateMarketInfoParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        private IDictionary<string, object> GetPermissionInfoList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["RoleId"] = 123,
                ["RoleName"] = "RoleName",
                ["DisplayName"] = "Role Name",
                ["ParentId"] = 123,
                ["AllParentIds"] = "123",
                ["FuncId"] = 1,
                ["FuncName"] = "Func Name",
                ["Policy"] = 3
            };
        }
    }
}
