using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.ExchangeRate;
using Fanex.Affiliate.Admin.Core.MetaModels.ExchangeRate;
using Fanex.Affiliate.Admin.Core.Repository.ExchangeRate;
using Fanex.Affiliate.Admin.Core.Services.ExchangeRate;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.ExchangeRate
{
    public class ExchangeRateServiceTest
    {
        [Fact]
        public void GetExchangeRateList_WhenCall_ExcuteWithCorrectData()
        {
            // Arrange
            var date = new DateTime();
            DateTime? passedDate = null;
            var exchangeRateRepository = new Mock<IExchangeRateRepository>();
            exchangeRateRepository.Setup(ex => ex.GetExchangeRateList(date)).Returns(new List<ExchangeRateInfo> { new ExchangeRateInfo() }).Callback<DateTime>(d => passedDate = d);

            var service = CreateExchangeRateService(exchangeRateRepository.Object);

            // Action
            var result = service.GetExchangeRateList(date);

            // Assert
            Assert.Single(result);
            Assert.Equal(date, passedDate);
        }

        [Fact]
        public void UpdateExchangeRate_WhenCall_ExcuteWithCorrectData()
        {
            // Arrange
            var param = new ExchangeRateParam();
            ExchangeRateParam passedParam = null;
            var exchangeRateRepository = new Mock<IExchangeRateRepository>();
            exchangeRateRepository.Setup(ex => ex.UpdateExchangeRate(param)).Returns(new ResultInfo()).Callback<ExchangeRateParam>(d => passedParam = d);

            var service = CreateExchangeRateService(exchangeRateRepository.Object);

            // Action
            var result = service.UpdateExchangeRate(param);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(param, passedParam);
        }
        private static ExchangeRateService CreateExchangeRateService(IExchangeRateRepository exchangeRateRepository = null)
        {
            var fakeExchangeRateRepository = exchangeRateRepository ?? Mock.Of<IExchangeRateRepository>();

            return new ExchangeRateService(fakeExchangeRateRepository);
        }
    }
}
