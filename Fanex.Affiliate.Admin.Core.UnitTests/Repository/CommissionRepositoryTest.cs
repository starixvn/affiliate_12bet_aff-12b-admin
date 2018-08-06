using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Data;
using Moq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Repository
{
    public class CommissionRepositoryTest
    {
        private const string SpKey = "Aff_Admin_CommGroup_Sel";

        [Fact]
        public void GetCommissionSettingList_WhenCall_ReturnCorrectCommGroupData()
        {
            // Arrange
            var repository = new Mock<CommissionGroupRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetExpectedCommGroup());
            objectDb.Setup(obj => obj.ExecuteReader(It.IsAny<CommissionGroupParam>())).Returns(dataReader.Object);
            repository.Setup(re => re.GetObjectDb(SpKey, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetCommissionGroupList(new CommissionGroupParam());

            // Assert
            Assert.Single(result.CommGroupList);
            Assert.Empty(result.TierSettingList);
            Assert.Empty(result.DeductionSettingList);
        }

        [Fact]
        public void GetCommissionSettingList_WhenCall_ReturnCorrectTierSetting()
        {
            // Arrange
            var repository = new Mock<CommissionGroupRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false).Returns(true);
            dataReader.SetupSequence(d => d.NextResult()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetExpectedTierSetting());
            objectDb.Setup(obj => obj.ExecuteReader(It.IsAny<CommissionGroupParam>())).Returns(dataReader.Object);
            repository.Setup(re => re.GetObjectDb(SpKey, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetCommissionGroupList(new CommissionGroupParam());

            // Assert
            Assert.Empty(result.CommGroupList);
            Assert.Single(result.TierSettingList);
            Assert.Empty(result.DeductionSettingList);
        }

        [Fact]
        public void GetCommissionSettingList_WhenCall_ReturnCorrectDeductionSetting()
        {
            // Arrange
            var repository = new Mock<CommissionGroupRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false).Returns(false).Returns(true);
            dataReader.SetupSequence(d => d.NextResult()).Returns(true).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetExpectedDeductionSetting());
            objectDb.Setup(obj => obj.ExecuteReader(It.IsAny<CommissionGroupParam>())).Returns(dataReader.Object);
            repository.Setup(re => re.GetObjectDb(SpKey, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetCommissionGroupList(new CommissionGroupParam());

            // Assert
            Assert.Empty(result.CommGroupList);
            Assert.Empty(result.TierSettingList);
            Assert.Single(result.DeductionSettingList);
        }

        [Fact]
        public void GetCommissionSettingList_WhenCall_ExecuteWithCorrectParam()
        {
            // Arrange
            var repository = new Mock<CommissionGroupRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false);
            dataReader.SetupSequence(d => d.NextResult()).Returns(false);

            CommissionGroupParam passedParam = null;
            var param = new CommissionGroupParam();
            objectDb.Setup(obj => obj.ExecuteReader(param))
                .Returns(dataReader.Object)
                .Callback<CommissionGroupParam>(p => passedParam = p);
            repository.Setup(re => re.GetObjectDb(SpKey, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetCommissionGroupList(param);

            // Assert
            Assert.Equal(passedParam, param);
        }

        private IDictionary<string, object> GetExpectedCommGroup()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["RowIdx"] = 1,
                ["GroupId"] = 101,
                ["GroupName"] = "Default",
                ["CreatedDate"] = new DateTime(2018, 7, 11),
                ["Creator"] = "Admin",
                ["Description"] = "Description",
                ["QMTurnover"] = 100.50m,
                ["IsAdjustment"] = true
            };
        }

        private IDictionary<string, object> GetExpectedTierSetting()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["GroupId"] = 101,
                ["TierId"] = 1,
                ["MinAmount"] = 0m,
                ["MaxAmount"] = 100m,
                ["MinQm"] = 0,
                ["MaxQm"] = 50,
                ["Commission"] = 0.2m
            };
        }

        private IDictionary<string, object> GetExpectedDeductionSetting()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["GroupId"] = 101,
                ["DeductionId"] = 1,
                ["DeductionName"] = "DeductionName",
                ["ProductIds"] = "1,2,3",
                ["DeductedPercentage"] = 0.7m,
                ["NextDeductedPercentage"] = 0.5m
            };
        }
    }
}
