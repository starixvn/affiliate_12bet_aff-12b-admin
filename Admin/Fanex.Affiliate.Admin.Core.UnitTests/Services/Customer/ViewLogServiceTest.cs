using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Customer
{
    public class ViewLogServiceTest
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetViewLogList_WhenCall_ReturnCorrectData(bool isNewViewLog)
        {
            // Arrange
            var service = new Mock<ViewLogService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetViewLogList());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(1);
            service.Setup(r => r.GetObjectDb("Aff_Admin_AffiliateLog_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetViewLogList(new ViewLogParam { IsNewViewLog = isNewViewLog });
            var item = result.MainData.FirstOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal("12/27/2019 12:00:00 AM", item.DateText);
        }

        [Fact]
        public void GetNewViewLogList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var service = new Mock<ViewLogService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetNewErroredViewLogList());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(1);
            service.Setup(r => r.GetObjectDb("Aff_Admin_AffiliateLog_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetViewLogList(new ViewLogParam { IsNewViewLog = true });
            var item = result.MainData.FirstOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Empty(item.OldData);
            Assert.Empty(item.NewData);
            Assert.Equal("12/27/2019 12:00:00 AM", item.DateText);
        }

        [Fact]
        public void GetLogActionList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var service = new Mock<ViewLogService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetLogActionList());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            service.Setup(r => r.GetObjectDb("Aff_LogAction_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetLogActionList();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetViewLogList_WhenCall_ReturnNull()
        {
            // Arrange
            var service = new Mock<ViewLogService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false);

            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(DBNull.Value);
            service.Setup(r => r.GetObjectDb("Aff_Admin_AffiliateLog_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetViewLogList(new ViewLogParam());

            // Assert
            Assert.Equal(0, result.TotalItem);
        }

        private IDictionary<string, object> GetViewLogList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["LogId"] = 123,
                ["UpdatedName"] = "sa",
                ["Date"] = new DateTime(2019, 12, 27),
                ["Action"] = "Action",
                ["OldData"] = "<root><row /></root>",
                ["NewData"] = "<root></root>",
                ["Description"] = "Description",
                ["IP"] = "::1"
            };
        }

        private IDictionary<string, object> GetNewErroredViewLogList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["LogId"] = 123,
                ["UpdatedName"] = "sa",
                ["Date"] = new DateTime(2019, 12, 27),
                ["Action"] = "Action",
                ["OldData"] = "",
                ["NewData"] = "<",
                ["Description"] = "Description",
                ["IP"] = "::1"
            };
        }

        private IDictionary<string, object> GetLogActionList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["ActionId"] = 123,
                ["ActionName"] = "Action"
            };
        }
    }
}
