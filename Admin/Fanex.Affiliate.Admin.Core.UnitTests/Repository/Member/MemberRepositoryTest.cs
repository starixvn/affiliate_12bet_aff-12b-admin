using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using Fanex.Affiliate.Admin.Core.Repository.Member;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Repository.Member
{
    public class MemberRepositoryTest
    {
        private const string SpKeyMemberListSel = "Aff_MemberListSel";
        private const string SpKeyMemberDel = "Aff_MemberDel";
        private const string SpKeyAdminUpdAffID = "DEP_AdminUpdAffID";
        private const string SpKeyWriteLog = "Aff_WriteLog";

        [Fact]
        public void GetMemberList_WhenCallWithSpKeyBannerSel_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<MemberRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetMemberList());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(1);
            repository.Setup(r => r.GetObjectDb(SpKeyMemberListSel, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetMemberList(new MemberParam());
            var memberItem = result.MainData.FirstOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal(1, result.TotalItem);
            Assert.Equal("10/10/2019 00:00:00", memberItem.CreatedDateText);
        }

        [Fact]
        public void GetMemberList_WhenCallWithSpKeyBannerSel_ReturnDataHasCreatedDateIsNull()
        {
            // Arrange
            var repository = new Mock<MemberRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetMemberListHasCreatedDateIsNull());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(1);
            repository.Setup(r => r.GetObjectDb(SpKeyMemberListSel, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetMemberList(new MemberParam());
            var memberItem = result.MainData.FirstOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal(1, result.TotalItem);
            Assert.Null(memberItem.CreatedDateText);
        }

        [Fact]
        public void GetMemberList_WhenCallWithSpKeyBannerSel_ReturnNullData()
        {
            // Arrange
            var repository = new Mock<MemberRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false).Returns(false);

            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(DBNull.Value);
            repository.Setup(r => r.GetObjectDb(SpKeyMemberListSel, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetMemberList(new MemberParam());

            // Assert
            Assert.Empty(result.MainData);
            Assert.Equal(0, result.TotalItem);
        }

        [Fact]
        public void DeleteMember_WhenCallWithValidParam_DeleteSuccessfully()
        {
            // Arrange
            var param = new DeleteMemberParam();
            var repository = new Mock<MemberRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            objectDb.Setup(obj => obj.GetParameterValue("AffID")).Returns(1271808);
            objectDb.Setup(obj => obj.GetParameterValue("DepositCustID")).Returns(123456);
            repository.Setup(re => re.GetObjectDb(SpKeyMemberDel, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.DeleteMember(param);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(1271808, param.AffId);
            Assert.Equal(123456, param.DepositMemberId);
        }

        [Fact]
        public void DeleteMember_WhenCallWithInvalidParam_DeleteIsFailed()
        {
            // Arrange
            var message = "Delete member is failed.";
            var repository = new Mock<MemberRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            objectDb.Setup(obj => obj.GetParameterValue("AffID")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("DepositCustID")).Returns(DBNull.Value);
            repository.Setup(re => re.GetObjectDb(SpKeyMemberDel, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.DeleteMember(new DeleteMemberParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void DeleteMemberInDepositDB_WhenCallWithValidParam_DeleteSuccessfully()
        {
            // Arrange
            var repository = new Mock<MemberRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb(SpKeyAdminUpdAffID, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.DeleteMemberInDepositDB(123456);

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DeleteMemberInDepositDB_WhenCallWithValidParam_DeleteIsFailed()
        {
            // Arrange
            var message = "Delete deposit member is failed.";
            var repository = new Mock<MemberRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            repository.Setup(re => re.GetObjectDb(SpKeyAdminUpdAffID, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.DeleteMemberInDepositDB(123456);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void WriteAfterDeleteSuccefully_WhenCallWithValidParam_ExcuteCorrectly()
        {
            // Arrange
            var repository = new Mock<MemberRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            repository.Setup(re => re.GetObjectDb(SpKeyWriteLog, 120)).Returns(objectDb.Object);

            // Action
            repository.Object.WriteAfterDeleteSuccefully(new DeleteMemberParam());

            // Assert
        }

        private IDictionary<string, object> GetMemberList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["IBCCustID"] = 123,
                ["UserName"] = "12betaff226",
                ["IBCUserName"] = "12betaff226",
                ["FirstName"] = "12Bet",
                ["LastName"] = "Aff",
                ["CurrBalance"] = 123m,
                ["CreatedDate"] = new DateTime(2019, 10, 10),
                ["AffUserName"] = "12BETAFF226",
            };
        }

        private IDictionary<string, object> GetMemberListHasCreatedDateIsNull()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["IBCCustID"] = 123,
                ["UserName"] = "12betaff226",
                ["IBCUserName"] = "12betaff226",
                ["FirstName"] = "12Bet",
                ["LastName"] = "Aff",
                ["CurrBalance"] = -123m,
                ["CreatedDate"] = DBNull.Value,
                ["AffUserName"] = "12BETAFF226",
            };
        }
    }
}
