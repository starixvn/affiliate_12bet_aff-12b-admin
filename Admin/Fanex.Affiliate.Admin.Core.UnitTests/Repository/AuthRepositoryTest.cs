using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.Login;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Repository
{
    public class AuthRepositoryTest
    {
        [Fact]
        public void GetPermissionList_WhenCall_ReturnCorrectPermissionList()
        {
            // Arrange
            var repository = new Mock<AuthRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetPermissionList());
            objectDb.Setup(obj => obj.ExecuteReader(It.IsAny<object>(), null)).Returns(dataReader.Object);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_PolicySel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetPermissionList(123);

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void ReCheckin_WhenCall_ReturnCorrect()
        {
            // Arrange
            var repository = new Mock<AuthRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null));
            objectDb.Setup(obj => obj.GetParameterValue("iskickedout")).Returns(true);
            repository.Setup(re => re.GetObjectDb("Aff_Recheckin", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.ReCheckin(123);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ReCheckin_WhenCall_ReturnInCorrect()
        {
            // Arrange
            var repository = new Mock<AuthRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null));
            objectDb.Setup(obj => obj.GetParameterValue("iskickedout")).Returns(DBNull.Value);
            repository.Setup(re => re.GetObjectDb("Aff_Recheckin", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.ReCheckin(123);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Login_WhenCall_ReturnCorrect()
        {
            // Arrange
            var param = new LoginParam { Password = "123" };
            var repository = new Mock<AuthRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null));
            objectDb.Setup(obj => obj.GetParameterValue("UserId")).Returns(1234);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns("");
            repository.Setup(re => re.GetObjectDb("Aff_Admin_Login", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.Login(param);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(1234, param.UserId);
        }

        [Fact]
        public void Login_WhenCall_ReturnInCorrect()
        {
            // Arrange
            var param = new LoginParam { Password = "123" };
            var repository = new Mock<AuthRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null));
            objectDb.Setup(obj => obj.GetParameterValue("UserId")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns("Error");
            repository.Setup(re => re.GetObjectDb("Aff_Admin_Login", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.Login(param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(0, param.UserId);
        }

        [Fact]
        public void LoginOnUser_WhenCall_ReturnCorrect()
        {
            // Arrange
            var param = new LoginParam { Password = "123" };
            var repository = new Mock<AuthRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null));
            objectDb.Setup(obj => obj.GetParameterValue("on_userid")).Returns(1234);
            repository.Setup(re => re.GetObjectDb("Aff_CheckIn", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.LoginOnUser(param, "123", "234");

            // Assert
            Assert.Equal(1234, result);
        }

        [Fact]
        public void LoginOnUser_WhenCall_ReturnInCorrect()
        {
            // Arrange
            var param = new LoginParam { Password = "123" };
            var repository = new Mock<AuthRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null));
            objectDb.Setup(obj => obj.GetParameterValue("on_userid")).Returns(DBNull.Value);
            repository.Setup(re => re.GetObjectDb("Aff_CheckIn", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.LoginOnUser(param, "123", "234");

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Logout_WhenCall_Excuter()
        {
            // Arrange
            var repository = new Mock<AuthRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null));
            repository.Setup(re => re.GetObjectDb("Aff_Logout", 120)).Returns(objectDb.Object);

            // Action
            repository.Object.Logout(1234);

            // Assert
            objectDb.Verify(obj => obj.ExecuteNonQuery(null, null), Times.Once);
        }

        [Fact]
        public void CheckPermission_WhenCall_ReturnCorrect()
        {
            // Arrange
            var param = new CheckPermissionParam { CustId = 123, FuncName = Function.Affiliate };
            var repository = new Mock<AuthRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null));
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns("");
            repository.Setup(re => re.GetObjectDb("Aff_Con_CheckPermission", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.CheckPermission(param);

            // Assert
            Assert.True(result.IsSuccessful);
        }

        private IDictionary<string, object> GetPermissionList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["FuncName"] = "affiliate",
                ["Host"] = true
            };
        }
    }
}
