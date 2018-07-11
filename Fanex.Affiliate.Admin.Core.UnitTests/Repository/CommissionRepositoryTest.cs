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
            var repository = new Mock<CommissionRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetExpectedCommGroup());
            objectDb.Setup(obj => obj.ExecuteReader(It.IsAny<CommissionSettingParam>())).Returns(dataReader.Object);
            repository.Setup(re => re.GetObjectDb(SpKey, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetCommissionSettingList(new CommissionSettingParam());

            // Assert
            Assert.NotNull(result.CommGroupList);
        }

        private IDictionary<string, object> GetExpectedCommGroup()
        {
            return new Dictionary<string, object>
            {
                ["RowIDX"] = 1,
                ["GroupId"] = 101,
                ["GroupName"] = "Default",
                ["CreatedDate"] = new DateTime(2018, 7, 11),
                ["Creator"] = "Admin",
                ["Description"] = "Description",
                ["QMTurnover"] = 100.50m,
                ["IsAdjustment"] = true
            };
        }
    }
}
