using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using Fanex.Affiliate.Admin.Core.Services.Member;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Member
{
    public class RegisteredMemberServiceTest
    {
        private const string SpKeyRegisteredMemberListSel = "Aff_Admin_RegisteredMember_Sel";

        [Fact]
        public void GetRegisteredMemberList_WhenCallWithSpKeyBannerSel_ReturnCorrectData()
        {
            // Arrange
            var service = new Mock<RegisteredMemberService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetMemberList());
            objectDb.Setup(o => o.ExecuteReader(null, null)).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(1);
            service.Setup(r => r.GetObjectDb(SpKeyRegisteredMemberListSel, 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetRegisteredMemberList(new MemberParam());
            var memberItem = result.MainData.FirstOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal(1, result.TotalItem);
            Assert.Equal("10/10/2019 00:00:00", memberItem.CreatedDateText);
        }

        [Fact]
        public void GetRegisteredMemberList_WhenCall_ReturnDataHasCreatedDateIsNull()
        {
            // Arrange
            var service = new Mock<RegisteredMemberService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetMemberListHasCreatedDateIsNull());
            objectDb.Setup(o => o.ExecuteReader(null, null)).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(1);
            service.Setup(r => r.GetObjectDb(SpKeyRegisteredMemberListSel, 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetRegisteredMemberList(new MemberParam());
            var memberItem = result.MainData.FirstOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal(1, result.TotalItem);
            Assert.Null(memberItem.CreatedDateText);
        }

        private IDictionary<string, object> GetMemberList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["UserName"] = "12betaff226",
                ["FirstName"] = "12Bet",
                ["LastName"] = "Aff",
                ["CreatedDate"] = new DateTime(2019, 10, 10),
                ["AffName"] = "12BETAFF226",
                ["Currency"] = "UUS"
            };
        }

        private IDictionary<string, object> GetMemberListHasCreatedDateIsNull()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["UserName"] = "12betaff226",
                ["FirstName"] = "12Bet",
                ["LastName"] = "Aff",
                ["CreatedDate"] = DBNull.Value,
                ["AffName"] = "12BETAFF226",
                ["Currency"] = "UUS"
            };
        }
    }
}
