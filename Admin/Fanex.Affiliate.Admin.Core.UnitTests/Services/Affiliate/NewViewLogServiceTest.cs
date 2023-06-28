using Fanex.Affiliate.Admin.Core.DTO.NewViewLog;
using Fanex.Affiliate.Admin.Core.MetaModels.NewViewLog;
using Fanex.Affiliate.Admin.Core.Services.NewViewLog;
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
    public class NewViewLogServiceTest
    {
        [Fact]
        public void GetLogActionList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var service = new Mock<NewViewLogService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetLogActionList());
            objectDb.Setup(o => o.ExecuteReader(null, null)).Returns(dataReader.Object);
            service.Setup(r => r.GetObjectDb("Aff_LogAction_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetLogActionList();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetNewViewLogList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new NewViewLogParam();
            var service = new Mock<NewViewLogService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            service.Setup(r => r.GetObjectDb("Aff_Admin_ViewLog_Sel", 120)).Returns(objectDb.Object);

            // Action
            service.Object.GetNewViewLogList(param);

            // Assert
            objectDb.Verify(obj => obj.Query<NewViewLogInfo>(null, null), Times.Once);
        }

        [Fact]
        public void GetNewViewLogDetailList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var service = new Mock<NewViewLogService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetNewViewLogDetailList());
            objectDb.Setup(o => o.ExecuteReader(null, null)).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(1);
            service.Setup(r => r.GetObjectDb("Aff_Admin_AffiliateLog_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetNewViewLogDetailList(new NewViewLogParam());
            var item = result.MainData.FirstOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.NotEmpty(item.OldData);
            Assert.Empty(item.NewData);
            Assert.Equal("12/27/2019 12:00:00 AM", item.DateText);
        }

        [Fact]
        public void GetNewViewLogDetailList_WhenCall_ReturnCorrectDataHasEmpty()
        {
            // Arrange
            var service = new Mock<NewViewLogService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetNewErroredViewLogDetailList());
            objectDb.Setup(o => o.ExecuteReader(null, null)).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(1);
            service.Setup(r => r.GetObjectDb("Aff_Admin_AffiliateLog_Sel", 120)).Returns(objectDb.Object);

            MockingHelper.SetUpEnviroment();

            // Action
            var result = service.Object.GetNewViewLogDetailList(new NewViewLogParam());
            var item = result.MainData.FirstOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Empty(item.OldData);
            Assert.Empty(item.NewData);
            Assert.Equal("12/27/2019 12:00:00 AM", item.DateText);
        }

        private IDictionary<string, object> GetNewViewLogDetailList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["LogId"] = 123,
                ["UpdatedName"] = "sa",
                ["Date"] = new DateTime(2019, 12, 27),
                ["Action"] = "Action",
                ["OldData"] = "<root><row /></root>",
                ["NewData"] = "<root></root>",
                ["IP"] = "::1"
            };
        }

        private IDictionary<string, object> GetNewErroredViewLogDetailList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["LogId"] = 123,
                ["UpdatedName"] = "sa",
                ["Date"] = new DateTime(2019, 12, 27),
                ["Action"] = "Action",
                ["OldData"] = "",
                ["NewData"] = "<",
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
