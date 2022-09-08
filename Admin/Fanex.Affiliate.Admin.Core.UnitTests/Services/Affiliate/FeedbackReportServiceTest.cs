using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels.FeedbackReport;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.FeedbackReport;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Affiliate
{
    public class FeedbackReportTest
    {
        [Fact]
        public void GetFeedbackReportList_WhenCall_ReturnCorrectAfilliateInfo()
        {
            // Arrange
            var service = new Mock<FeedbackReportService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetExpectedFeedbackReport());
            objectDb.Setup(obj => obj.ExecuteReader(null, null)).Returns(dataReader.Object);
            service.Setup(re => re.GetObjectDb("Aff_Admin_Feedback_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetFeedbackReportList(new FeedbackReportParam());

            // Assert
            Assert.Single(result.MainData);
        }

        [Fact]
        public void GetFeedbackReportList_WhenCall_ReturnNull()
        {
            // Arrange
            var service = new Mock<FeedbackReportService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false).Returns(false);

            objectDb.Setup(obj => obj.ExecuteReader(null, null)).Returns(dataReader.Object);
            service.Setup(re => re.GetObjectDb("Aff_Admin_Feedback_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetFeedbackReportList(new FeedbackReportParam());

            // Assert
            Assert.Empty(result.MainData);
        }

        [Fact]
        public void GetRateList_WhenCall_ReturnCorrectAfilliateInfo()
        {
            // Arrange
            var service = new Mock<FeedbackReportService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetExpectedRate());
            objectDb.Setup(obj => obj.ExecuteReader(null, null)).Returns(dataReader.Object);
            service.Setup(re => re.GetObjectDb("Aff_RateSel", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetRateList();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetRateList_WhenCall_ReturnNull()
        {
            // Arrange
            var service = new Mock<FeedbackReportService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false).Returns(false);

            objectDb.Setup(obj => obj.ExecuteReader(null, null)).Returns(dataReader.Object);
            service.Setup(re => re.GetObjectDb("Aff_RateSel", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetRateList();

            // Assert
            Assert.Empty(result);
        }

        private IDictionary<string, object> GetExpectedFeedbackReport()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["UserName"] = "12BetAff226",
                ["RateName"] = "abc",
                ["Message"] = "test",
                ["SentDate"] = new DateTime(2022, 08, 29)
            };
        }

        private IDictionary<string, object> GetExpectedRate()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["RateId"] = 123,
                ["RateName"] = "None"
            };
        }
    }
}
