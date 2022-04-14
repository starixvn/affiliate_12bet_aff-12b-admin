using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Affiliate.Admin.Core.Services.AdminRole;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.AdminRole
{
    public class AdminListServiceTest
    {
        [Fact]
        public void DeleteUsers_WhenCallWithValidParam_DeleteSuccessfully()
        {
            // Arrange
            var service = new Mock<AdminListService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            service.Setup(re => re.GetObjectDb("Aff_Admin_AdminUser_Del", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.DeleteUsers(new DeleteAdminUsersParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DeleteUsers_WhenCallWithInvalidParam_DeleteSuccessfully()
        {
            // Arrange
            var service = new Mock<AdminListService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(Code.GeneralErrorCode);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            service.Setup(re => re.GetObjectDb("Aff_Admin_AdminUser_Del", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.DeleteUsers(new DeleteAdminUsersParam());

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void GetUserList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var service = new Mock<AdminListService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetUser());
            objectDb.Setup(o => o.ExecuteReader(null, null)).Returns(dataReader.Object);
            service.Setup(r => r.GetObjectDb("Aff_Admin_AdminUser_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetUserList(string.Empty);
            var item = result.FirstOrDefault();

            // Assert
            Assert.Single(result);
            Assert.Equal("08/11/2020 00:00:00", item.LastLoginDateText);
        }

        [Fact]
        public void GetUserList_WhenCallWithHasGroupId_ReturnCorrectData()
        {
            // Arrange
            var service = new Mock<AdminListService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetUserHasNull());
            objectDb.Setup(o => o.ExecuteReader(null, null)).Returns(dataReader.Object);
            service.Setup(r => r.GetObjectDb("Aff_Admin_AdminUser_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetUserList(string.Empty);
            var item = result.FirstOrDefault();

            // Assert
            Assert.Single(result);
            Assert.Null(item.LastLoginDateText);
        }

        private IDictionary<string, object> GetUser()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["CustId"] = 1,
                ["UserName"] = "AAtests",
                ["FirstName"] = "AAtests",
                ["LastName"] = "AAtests",
                ["Creator"] = "Sa",
                ["LastLoginDate"] = new DateTime(2020, 08, 11),
                ["LastLoginIp"] = "::1",
                ["RoleName"] = "Manager",
                ["Closed"] = false
            };
        }

        private IDictionary<string, object> GetUserHasNull()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["CustId"] = 1,
                ["UserName"] = "AAtests",
                ["FirstName"] = "AAtests",
                ["LastName"] = "AAtests",
                ["Creator"] = "Sa",
                ["LastLoginDate"] = DBNull.Value,
                ["LastLoginIp"] = "::1",
                ["RoleName"] = "Manager",
                ["Closed"] = false
            };
        }
    }
}
