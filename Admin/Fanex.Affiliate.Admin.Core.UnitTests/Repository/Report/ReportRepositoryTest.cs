using Fanex.Affiliate.Admin.Core.MetaModels.Report;
using Fanex.Affiliate.Admin.Core.Repository.Report;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Repository.Report
{
    public class ReportRepositoryTest
    {
        [Fact]
        public void GetMainReportData_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<AffiliateReportRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false).Returns(true);
            dataReader.SetupSequence(d => d.NextResult()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetReportData());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(1);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_AffiliateReport_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetMainReportData(new AffiliateReportMainParam());
            var item = result.MainData.FirstOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal("", item.MonthDateString);
        }

        [Fact]
        public void GetMainReportData_WhenCall_ReturnCorrectDataHasFullColumn()
        {
            // Arrange
            var repository = new Mock<AffiliateReportRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false).Returns(true);
            dataReader.SetupSequence(d => d.NextResult()).Returns(true).Returns(false);
            dataReader.Setup(d => d.FieldCount).Returns(4);
            dataReader.Setup(d => d.GetName(0)).Returns("AffId");
            dataReader.Setup(d => d.GetName(1)).Returns("Username");
            dataReader.Setup(d => d.GetName(2)).Returns("MonthDate");
            dataReader.Setup(d => d.GetName(3)).Returns("Description");

            ReaderHelper.MapReaderData(dataReader, GetReportDataFullColumn());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(1);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_AffiliateReport_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetMainReportData(new AffiliateReportMainParam());
            var item = result.MainData.FirstOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal("11/2019", item.MonthDateString);
        }

        [Fact]
        public void GetMainReportData_WhenCall_ReturnNullData()
        {
            // Arrange
            var repository = new Mock<AffiliateReportRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false).Returns(false);

            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(DBNull.Value);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_AffiliateReport_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetMainReportData(new AffiliateReportMainParam());

            // Assert
            Assert.Empty(result.MainData);
        }

        [Fact]
        public void GetDetailReportData_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<AffiliateReportRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false).Returns(true);
            dataReader.SetupSequence(d => d.NextResult()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetReportData());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(1);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_AffiliateReport_Detail_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetDetailReportData(new AffiliateReportMainParam());

            // Assert
            Assert.Single(result.MainData);
        }

        [Fact]
        public void GetDetailReportData_WhenCall_ReturnNullData()
        {
            // Arrange
            var repository = new Mock<AffiliateReportRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false).Returns(false);

            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(DBNull.Value);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_AffiliateReport_Detail_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetDetailReportData(new AffiliateReportMainParam());

            // Assert
            Assert.Empty(result.MainData);
        }

        [Fact]
        public void SearchAffiliateList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<AffiliateReportRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetSearchAffiliateList());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            repository.Setup(r => r.GetObjectDb("Aff_AffiliateUsername_SearchEx", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.SearchAffiliateList("12BETAFF226");

            // Assert
            Assert.Single(result);
        }

        private IDictionary<string, object> GetReportData()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["TotalClick"] = 123,
                ["UniqueClick"] = 123,
                ["Signup"] = 123,
                ["FDM"] = 123,
                ["ActiveMember"] = 123,
                ["QualifiedMember"] = 123,
                ["Deposit"] = 123,
                ["Withdrawal"] = 123,
                ["Turnover"] = 123,
                ["Winlost"] = 123,
                ["Bonus"] = 123,
                ["PlatformFee"] = 123,
                ["OperationalFee"] = 123,
                ["BankingFee"] = 123,
                ["TotalFee"] = 123,
                ["CommissionRate"] = 123,
                ["Earning"] = 123,
                ["AffWithdrawal"] = 123,
                ["AffAdjustment"] = 123,
                ["BFF"] = 123,
            };
        }

        private IDictionary<string, object> GetReportDataFullColumn()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["AffId"] = 1271808,
                ["Username"] = "12BETAFF226",
                ["MonthDate"] = new DateTime(2019, 11, 24),
                ["Description"] = "abc",
                ["TotalClick"] = 123,
                ["UniqueClick"] = 123,
                ["Signup"] = 123,
                ["FDM"] = 123,
                ["ActiveMember"] = 123,
                ["QualifiedMember"] = 123,
                ["Deposit"] = 123,
                ["Withdrawal"] = 123,
                ["Turnover"] = 123,
                ["Winlost"] = 123,
                ["Bonus"] = 123,
                ["PlatformFee"] = 123,
                ["OperationalFee"] = 123,
                ["BankingFee"] = 123,
                ["TotalFee"] = 123,
                ["CommissionRate"] = 123,
                ["Earning"] = 123,
                ["AffWithdrawal"] = 123,
                ["AffAdjustment"] = 123,
                ["BFF"] = 123,
            };
        }

        private IDictionary<string, object> GetSearchAffiliateList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["UserName"] = "12BETAFF226"
            };
        }
    }
}
