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
    public class PlayerDetailServiceTest
    {
        [Fact]
        public void GetPlayerDetailList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var service = new Mock<PlayerDetailService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetPlayerDetailList());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(1);
            service.Setup(r => r.GetObjectDb("Aff_Admin_PlayersDetail_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetPlayerDetailList(new PlayerDetailParam());
            var item = result.MainData.FirstOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal("11/24/2019 00:00:00", item.RegisteredDateText);
        }

        [Fact]
        public void GetPlayerDetailList_WhenCall_ReturnNull()
        {
            // Arrange
            var service = new Mock<PlayerDetailService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false);

            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(DBNull.Value);
            service.Setup(r => r.GetObjectDb("Aff_Admin_PlayersDetail_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetPlayerDetailList(new PlayerDetailParam());

            // Assert
            Assert.Equal(0, result.TotalItem);
        }

        private IDictionary<string, object> GetPlayerDetailList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["IbcUserName"] = "12BetAff226",
                ["UserName"] = "12BetAff226",
                ["Deposit"] = 1234,
                ["RegisteredDate"] = new DateTime(2019, 11, 24),
                ["Withdrawal"] = 37,
                ["BankingFee"] = 123,
                ["Bonus"] = 123,
                ["Winloss"] = 123,
                ["Turnover"] = 123
            };
        }
    }
}
