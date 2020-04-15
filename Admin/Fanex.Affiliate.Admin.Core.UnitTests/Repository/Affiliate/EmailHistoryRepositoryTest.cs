using Fanex.Affiliate.Admin.Core.MetaModels.EmailHistory;
using Fanex.Affiliate.Admin.Core.Repository.Affiliate;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Repository.Affiliate
{
    public class EmailHistoryRepositoryTest
    {
        [Fact]
        public void GetEmailHistoryList_WhenCall_ReturnCorrectAfilliateInfo()
        {
            // Arrange
            var repository = new Mock<EmailHistoryRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(1);
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetExpectedEmailList());
            objectDb.Setup(obj => obj.ExecuteReader()).Returns(dataReader.Object);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_EmailHistory_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetEmailHistoryList(new EmailHistoryParam());
            var item = result.MainData.SingleOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal("04/15/2020 00:00:00", item.SentDateText);
        }

        [Fact]
        public void GetEmailHistoryList_WhenCall_ReturnNull()
        {
            // Arrange
            var repository = new Mock<EmailHistoryRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(DBNull.Value);
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false).Returns(false);

            objectDb.Setup(obj => obj.ExecuteReader()).Returns(dataReader.Object);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_EmailHistory_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetEmailHistoryList(new EmailHistoryParam());

            // Assert
            Assert.Empty(result.MainData);
        }

        private IDictionary<string, object> GetExpectedEmailList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["Affname"] = "12BetAff226",
                ["MID"] = 123,
                ["Sender"] = "123",
                ["Subject"] = "vi-VN",
                ["Email"] = "abc@gmail.com",
                ["CreatedDate"] = new DateTime(2020, 04, 15)
            };
        }
    }
}
