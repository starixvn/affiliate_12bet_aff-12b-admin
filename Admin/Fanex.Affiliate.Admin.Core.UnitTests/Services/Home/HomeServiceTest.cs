using Fanex.Affiliate.Admin.Core.Services.Home;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Home
{
    public class HomeServiceTest
    {
        [Fact]
        public void GetOnlineList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var service = new Mock<HomeService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetOnlineList());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(1);
            service.Setup(r => r.GetObjectDb("Aff_UserOnlineList", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetOnlineList(1, 20);
            var item = result.MainData.FirstOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal("11/15/2019 00:00:00", item.LoggedInDateText);
            Assert.Equal("11/15/2019 00:00:00", item.LastRequestDateText);
        }

        [Fact]
        public void GetOnlineList_WhenCall_ReturnCorrectDataHasNull()
        {
            // Arrange
            var service = new Mock<HomeService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetOnlineListHasNull());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(1);
            service.Setup(r => r.GetObjectDb("Aff_UserOnlineList", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetOnlineList(1, 20);
            var item = result.MainData.FirstOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Null(item.LoggedInDateText);
            Assert.Null(item.LastRequestDateText);
        }

        [Fact]
        public void GetOnlineList_WhenCall_ReturnNull()
        {
            // Arrange
            var service = new Mock<HomeService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false);

            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(DBNull.Value);
            service.Setup(r => r.GetObjectDb("Aff_UserOnlineList", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetOnlineList(1, 20);

            // Assert
            Assert.Equal(0, result.TotalItem);
        }

        [Fact]
        public void GetPSPToday_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var service = new Mock<HomeService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();

            objectDb.Setup(obj => obj.GetParameterValue("PSP")).Returns(1.23m);
            service.Setup(r => r.GetObjectDb("DEP_AffiliateGetPSP", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetPSPToday();

            // Assert
            Assert.Equal(1.23m, result);
        }

        [Fact]
        public void GetDepositInfo_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var service = new Mock<HomeService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetDepositInfo());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            service.Setup(r => r.GetObjectDb("DEP_AffiliateGeneralInfo", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetDepositInfo();

            // Assert
            Assert.Equal(34, result.AllMemberDepositUpToNow);
        }

        [Fact]
        public void GetDepositInfo_WhenCall_ReturnNull()
        {
            // Arrange
            var service = new Mock<HomeService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false);

            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            service.Setup(r => r.GetObjectDb("DEP_AffiliateGeneralInfo", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetDepositInfo();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetGeneralInfo_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var service = new Mock<HomeService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetGeneralInfo());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            service.Setup(r => r.GetObjectDb("Aff_Admin_GeneralInfo_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetGeneralInfo();

            // Assert
            Assert.Equal(1, result.PaymentRequestNumber);
        }

        [Fact]
        public void GetGeneralInfo_WhenCall_ReturnNull()
        {
            // Arrange
            var service = new Mock<HomeService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false);

            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            service.Setup(r => r.GetObjectDb("Aff_Admin_GeneralInfo_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetGeneralInfo();

            // Assert
            Assert.Null(result);
        }

        private IDictionary<string, object> GetOnlineList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["UserName"] = "sa",
                ["LoggedIn"] = new DateTime(2019, 11, 15),
                ["LastRequest"] = new DateTime(2019, 11, 15),
                ["IP"] = "::1",
                ["RoleName"] = "Admin"
            };
        }

        private IDictionary<string, object> GetOnlineListHasNull()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["UserName"] = "sa",
                ["LoggedIn"] = DBNull.Value,
                ["LastRequest"] = DBNull.Value,
                ["IP"] = "::1",
                ["RoleName"] = "Admin"
            };
        }

        private IDictionary<string, object> GetDepositInfo()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["MemInProAllNum"] = 34,
                ["MemInProMonthNum"] = 12
            };
        }

        private IDictionary<string, object> GetGeneralInfo()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["PaymentReqNum"] = 1,
                ["BannerNum"] = 2,
                ["ImageBannerNum"] = 3,
                ["FlashBannerNum"] = 4,
                ["TextBannerNum"] = 5,
                ["AffInProAllNum"] = 1,
                ["PendingAllNum"] = 2,
                ["ApprovedAllNum"] = 3,
                ["DisapprovedAllNum"] = 4,
                ["ClosedAllNum"] = 5,
                ["DepositMemAllNum"] = 1,
                ["DepositAll"] = 2,
                ["WinlostAll"] = 3,
                ["BonusAll"] = 4,
                ["PSPFeeAll"] = 5,
                ["NetWinlostAll"] = 1,
                ["AffInProMonthNum"] = 2,
                ["PendingMonthNum"] = 3,
                ["ApprovedMonthNum"] = 4,
                ["DisapprovedMonthNum"] = 5,
                ["ClosedMonthNum"] = 1,
                ["DepositMemMonthNum"] = 2,
                ["DepositMonth"] = 3,
                ["WinlostMonth"] = 4,
                ["BonusMonth"] = 5,
                ["PSPFeeMonth"] = 4,
                ["NetWinlostMonth"] = 5
            };
        }
    }
}
