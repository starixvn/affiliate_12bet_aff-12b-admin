using Fanex.Affiliate.Admin.Core.DTO.Member;
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
    public class ActiveMemberRepositoryTest
    {
        [Fact]
        public void GetAllMembers_WhenCall_ReturnCorrectInactiveMemberData()
        {
            // Arrange
            var repository = new Mock<ActiveMemberRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false).Returns(true);
            dataReader.SetupSequence(d => d.NextResult()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetInactivatedMemberList());
            objectDb.Setup(o => o.ExecuteReader(null, null)).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns("abc");
            repository.Setup(r => r.GetObjectDb("Aff_Admin_ActiveMember_CheckStatus", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetAllMembers("12betaff226,abc");

            // Assert
            Assert.Single(result.InactiveMemberList);
            Assert.Equal("abc", result.MemberNotExist);
        }

        [Fact]
        public void GetAllMembers_WhenCall_ReturnCorrectActiveMemberData()
        {
            // Arrange
            var repository = new Mock<ActiveMemberRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetActivatedMemberList());
            objectDb.Setup(o => o.ExecuteReader(null, null)).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns("abc");
            repository.Setup(r => r.GetObjectDb("Aff_Admin_ActiveMember_CheckStatus", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetAllMembers("12betaff226,abc");
            var item = result.ActivedMemeberList.FirstOrDefault();

            // Assert
            Assert.Single(result.ActivedMemeberList);
            Assert.Equal("abc", result.MemberNotExist);
            Assert.Equal("10/23/2019 00:00:00", item.CreatedDateText);
        }

        [Fact]
        public void GetAllMembers_WhenCall_ReturnActiveMemberHasCreatedDateIsNull()
        {
            // Arrange
            var repository = new Mock<ActiveMemberRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetActivatedMemberListHasCreatedDateIsNull());
            objectDb.Setup(o => o.ExecuteReader(null, null)).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns("abc");
            repository.Setup(r => r.GetObjectDb("Aff_Admin_ActiveMember_CheckStatus", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetAllMembers("12betaff226,abc");
            var item = result.ActivedMemeberList.FirstOrDefault();

            // Assert
            Assert.Single(result.ActivedMemeberList);
            Assert.Equal("abc", result.MemberNotExist);
            Assert.Null(item.CreatedDateText);
        }

        [Fact]
        public void Get12BetPlayersDepositInfo_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<ActiveMemberRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, Get12BetPlayersDepositInfo());
            objectDb.Setup(o => o.ExecuteReader(null, null)).Returns(dataReader.Object);
            repository.Setup(r => r.GetObjectDb("Aff_Deposit_GetCustDataForCreateIBCACByUserNames", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.Get12BetPlayersDepositInfo("abc");

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void WriteLogAfterAdd12BetPlayerAccountToIBCSuccefully_WhenCallWithValidParam_ExcuteCorrectly()
        {
            // Arrange
            var repository = new Mock<ActiveMemberRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            repository.Setup(re => re.GetObjectDb("Aff_WriteLog", 120)).Returns(objectDb.Object);

            // Action
            repository.Object.WriteLogAfterAdd12BetPlayerAccountToIBCSuccefully(new ActiveMemberParam(), 123);

            // Assert
        }

        [Fact]
        public void Update12BetPlayerStatusToDeposit_WhenCallWithValidParam_UpdateSuccessfully()
        {
            // Arrange
            var repository = new Mock<ActiveMemberRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrorCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrorMsg")).Returns(null);
            repository.Setup(re => re.GetObjectDb("Aff_Deposit_MemberIBCDetail", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.Update12BetPlayerStatusToDeposit("abc", 123, "xyz");

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Update12BetPlayerStatusToDeposit_WhenCallWithValidParam_UpdateIsFailed()
        {
            // Arrange
            var repository = new Mock<ActiveMemberRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrorCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrorMsg")).Returns("Update is failed");
            repository.Setup(re => re.GetObjectDb("Aff_Deposit_MemberIBCDetail", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.Update12BetPlayerStatusToDeposit("abc", 123, "xyz");

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Update is failed", result.Message);
        }

        [Fact]
        public void WriteLogAfterUpdate12BetPlayerStatusToDepositSuccefully_WhenCallWithValidParam_ExcuteCorrectly()
        {
            // Arrange
            var repository = new Mock<ActiveMemberRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            repository.Setup(re => re.GetObjectDb("Aff_WriteLog", 120)).Returns(objectDb.Object);

            // Action
            repository.Object.WriteLogAfterUpdate12BetPlayerStatusToDepositSuccefully(new ActiveMemberParam(), 123456);

            // Assert
        }

        private IDictionary<string, object> GetInactivatedMemberList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["UserName"] = "12betaff226",
                ["FirstName"] = "12Bet",
                ["LastName"] = "Aff"
            };
        }

        private IDictionary<string, object> GetActivatedMemberList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["IBCCustID"] = 123,
                ["UserName"] = "12betaff226",
                ["IBCUserName"] = "12betaff226",
                ["FirstName"] = "12Bet",
                ["LastName"] = "Aff",
                ["CreatedDate"] = new DateTime(2019, 10, 23)
            };
        }

        private IDictionary<string, object> GetActivatedMemberListHasCreatedDateIsNull()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["IBCCustID"] = 123,
                ["UserName"] = "12betaff226",
                ["IBCUserName"] = "12betaff226",
                ["FirstName"] = "12Bet",
                ["LastName"] = "Aff",
                ["CreatedDate"] = DBNull.Value
            };
        }

        private IDictionary<string, object> Get12BetPlayersDepositInfo()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["UserName"] = "12betaff226",
                ["FirstName"] = "12Bet",
                ["LastName"] = "Aff",
                ["Email"] = "abc@gmail.com",
                ["Password"] = "123abc",
                ["OddsType"] = "abc",
                ["AffiliateId"] = 123,
                ["Country"] = "Viet Nam",
                ["AgentId"] = 1,
                ["BonusCode"] = "code",
                ["DepositUserId"] = 123456
            };
        }
    }
}
