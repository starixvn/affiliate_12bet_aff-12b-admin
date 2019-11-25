using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
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
    public class StatementHistoryRepositoryTest
    {
        [Fact]
        public void GetStatementHistoryList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<StatementHistoryRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetStatementHistoryList());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(1);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_StatementHistory_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetStatementHistoryList(new AffiliatePagingByDateParam());
            var item = result.MainData.FirstOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal("22-Nov-2019", item.DateText);
        }

        [Fact]
        public void GetStatementHistoryList_WhenCall_ReturnCorrectDataHasNull()
        {
            // Arrange
            var repository = new Mock<StatementHistoryRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetStatementHistoryListIsTransaction());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(1);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_StatementHistory_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetStatementHistoryList(new AffiliatePagingByDateParam());
            var item = result.MainData.FirstOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal("Nov-2019", item.DateText);
        }

        [Fact]
        public void GetStatementHistoryList_WhenCall_ReturnNull()
        {
            // Arrange
            var repository = new Mock<StatementHistoryRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false);

            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(DBNull.Value);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_StatementHistory_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetStatementHistoryList(new AffiliatePagingByDateParam());

            // Assert
            Assert.Equal(0, result.TotalItem);
        }

        [Fact]
        public void GetDailyEarning_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<StatementHistoryRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false).Returns(true);
            dataReader.SetupSequence(d => d.NextResult()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetDailyEarningList());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            repository.Setup(r => r.GetObjectDb("Aff_DailyEarning_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetDailyEarning(new DailyEarningParam());
            var item = result.MainData.FirstOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal("", item.Date);
        }

        [Fact]
        public void GetDailyEarning_WhenCall_ReturnCorrectDataHasWinlostDate()
        {
            // Arrange
            var repository = new Mock<StatementHistoryRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false).Returns(true);
            dataReader.SetupSequence(d => d.NextResult()).Returns(true).Returns(false);
            dataReader.Setup(d => d.FieldCount).Returns(1);
            dataReader.Setup(d => d.GetName(0)).Returns("WinlostDate");

            ReaderHelper.MapReaderData(dataReader, GetDailyEarningListHasWinlostDate());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            repository.Setup(r => r.GetObjectDb("Aff_DailyEarning_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetDailyEarning(new DailyEarningParam());
            var item = result.MainData.FirstOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal("22 November 2019", item.Date);
        }

        [Fact]
        public void GetDailyEarning_WhenCall_ReturnNullData()
        {
            // Arrange
            var repository = new Mock<StatementHistoryRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false).Returns(false);

            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            repository.Setup(r => r.GetObjectDb("Aff_DailyEarning_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetDailyEarning(new DailyEarningParam());

            // Assert
            Assert.Empty(result.MainData);
        }

        [Fact]
        public void GetDailyEarningDetail_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<StatementHistoryRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetDailyEarningDetailList());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            repository.Setup(r => r.GetObjectDb("Aff_DailyEarning_Detail_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetDailyEarningDetail(new DailyEarningParam());

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetDailyEarningDetail_WhenCall_ReturnNullData()
        {
            // Arrange
            var repository = new Mock<StatementHistoryRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.Setup(d => d.Read()).Returns(false);

            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            repository.Setup(r => r.GetObjectDb("Aff_DailyEarning_Detail_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetDailyEarningDetail(new DailyEarningParam());

            // Assert
            Assert.Empty(result);
        }

        private IDictionary<string, object> GetStatementHistoryList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["TransId"] = 37,
                ["Amount"] = 100,
                ["TransType"] = 123,
                ["MonthDate"] = new DateTime(2019, 11, 22),
                ["Status"] = "Approved",
                ["Creator"] = "sa",
                ["Description"] = "abc"
            };
        }

        private IDictionary<string, object> GetStatementHistoryListIsTransaction()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["TransId"] = 37,
                ["Amount"] = 100,
                ["TransType"] = 0,
                ["MonthDate"] = new DateTime(2019, 11, 22),
                ["Status"] = "Approved",
                ["Creator"] = "sa",
                ["Description"] = "abc"
            };
        }

        private IDictionary<string, object> GetDailyEarningList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["Deposit"] = 123,
                ["Withdrawal"] = 123,
                ["MemberWinLost"] = 123,
                ["Bonus"] = 123,
                ["BankingFee"] = 123,
            };
        }

        private IDictionary<string, object> GetDailyEarningListHasWinlostDate()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["Deposit"] = 123,
                ["Withdrawal"] = 123,
                ["MemberWinLost"] = 123,
                ["Bonus"] = 123,
                ["BankingFee"] = DBNull.Value,
                ["WinlostDate"] = new DateTime(2019, 11, 22)
            };
        }

        private IDictionary<string, object> GetDailyEarningDetailList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["CustId"] = 123,
                ["UserName"] = "abc",
                ["IBCUsername"] = "abc",
                ["Winlost"] = 123,
                ["Deposit"] = 123,
                ["Withdrawal"] = 123,
                ["Bonus"] = 123,
                ["BankingFee"] = 123,
            };
        }
    }
}
