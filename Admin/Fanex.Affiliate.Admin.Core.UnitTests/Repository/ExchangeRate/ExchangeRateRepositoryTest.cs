using Fanex.Affiliate.Admin.Core.MetaModels.ExchangeRate;
using Fanex.Affiliate.Admin.Core.Repository.ExchangeRate;
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

namespace Fanex.Affiliate.Admin.Core.UnitTests.Repository.ExchangeRate
{
    public class ExchangeRateRepositoryTest
    {
        private const string SpKeyExchangeSel = "Aff_ExchangeSel";
        private const string SpKeyExchangeUpdate = "Aff_ExchangeUpdate";

        [Fact]
        public void GetExchangeRateList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var date = new DateTime();
            var repository = new Mock<ExchangeRateRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetExchangeRateList());
            objectDb.Setup(obj => obj.ExecuteReader()).Returns(dataReader.Object);
            repository.Setup(re => re.GetObjectDb(SpKeyExchangeSel, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetExchangeRateList(date);

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void UpdateExchangeRate_WhenCallWithValidParam_UpdateSuccessfully()
        {
            // Arrange
            var repository = new Mock<ExchangeRateRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb(SpKeyExchangeUpdate, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdateExchangeRate(new ExchangeRateParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateExchangeRate_WhenCallWithInvalidParam_UpdateIsFailed()
        {
            // Arrange
            var message = "Update is failed.";
            var repository = new Mock<ExchangeRateRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            repository.Setup(re => re.GetObjectDb(SpKeyExchangeUpdate, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdateExchangeRate(new ExchangeRateParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        private IDictionary<string, object> GetExchangeRateList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["ExchangeId"] = 3,
                ["Currency"] = "US$",
                ["ActualRate"] = 1,
                ["BuyingRate"] = 1
            };
        }
    }
}
