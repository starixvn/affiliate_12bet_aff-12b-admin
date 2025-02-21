using Fanex.Affiliate.Admin.Core.Criterias;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Data.Repository;
using Moq;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Common
{
    public class SystemServiceTest
    {
        [Fact]
        public void GetLanguageList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(l => l.Fetch<Language>(It.IsAny<BaseCriteria>(), It.IsAny<string>())).Returns(new List<Language> { new Language() });

            var service = CreatedynamicRepository(dynamicRepository.Object);

            // Action
            var result = service.GetLanguageList();

            // Assert
            dynamicRepository.Verify(d => d.Fetch<Language>(It.Is<BaseCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.Single(result);
        }

        [Fact]
        public void GetBaseCurrencies_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(l => l.Fetch<Currency>(It.IsAny<BaseCriteria>(), It.IsAny<string>())).Returns(new List<Currency> { new Currency() });

            var service = CreatedynamicRepository(dynamicRepository.Object);

            // Action
            var result = service.GetBaseCurrencies();

            // Assert
            dynamicRepository.Verify(d => d.Fetch<Currency>(It.Is<BaseCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.Single(result);
        }

        [Fact]
        public void GetCurrencies_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(l => l.Fetch<Currency>(It.IsAny<BaseCriteria>(), It.IsAny<string>())).Returns(new List<Currency> { new Currency() });

            var service = CreatedynamicRepository(dynamicRepository.Object);

            // Action
            var result = service.GetCurrencies();

            // Assert
            dynamicRepository.Verify(d => d.Fetch<Currency>(It.Is<BaseCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.Single(result);
        }

        [Fact]
        public void GetPaymentCurrencies_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(l => l.Fetch<Currency>(It.IsAny<BaseCriteria>(), It.IsAny<string>())).Returns(new List<Currency> { new Currency() });

            var service = CreatedynamicRepository(dynamicRepository.Object);

            // Action
            var result = service.GetPaymentCurrencies();

            // Assert
            dynamicRepository.Verify(d => d.Fetch<Currency>(It.Is<BaseCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.Single(result);
        }

        [Fact]
        public void GetProductList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(l => l.Fetch<Product>(It.IsAny<BaseCriteria>(), It.IsAny<string>())).Returns(new List<Product> { new Product() });

            var service = CreatedynamicRepository(dynamicRepository.Object);

            // Action
            var result = service.GetProductList();

            // Assert
            dynamicRepository.Verify(d => d.Fetch<Product>(It.Is<BaseCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.Single(result);
        }

        [Fact]
        public void SetCulture_WhenCall_Excute()
        {
            // Arrange
            var languageCode = "en-US";
            var currentUICulture = new CultureInfo(languageCode);
            var currentCulture = CultureInfo.CreateSpecificCulture(languageCode);

            var service = CreatedynamicRepository();

            // Action
            service.SetCulture("en-US");

            // Assert
            Assert.Equal(Thread.CurrentThread.CurrentUICulture, currentUICulture);
            Assert.Equal(Thread.CurrentThread.CurrentCulture, currentCulture);
        }

        private static SystemService CreatedynamicRepository(IDynamicRepository dynamicRepository = null)
        {
            var fakedynamicRepository = dynamicRepository ?? Mock.Of<IDynamicRepository>();

            return new SystemService(fakedynamicRepository);
        }
    }
}
