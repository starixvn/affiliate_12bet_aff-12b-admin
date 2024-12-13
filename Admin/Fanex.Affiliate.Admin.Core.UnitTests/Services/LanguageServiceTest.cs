using Fanex.Affiliate.Admin.Core.Criterias;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Data.Repository;
using Moq;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services
{
    public class LanguageServiceTest
    {
        [Fact]
        public void GetLanguageList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var languageService = new Mock<IDynamicRepository>();
            languageService.Setup(l => l.Fetch<Language>(It.IsAny<BaseCriteria>())).Returns(new List<Language> { new Language() });

            var service = CreateLanguageService(languageService.Object);

            // Action
            var result = service.GetLanguageList();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void SetCulture_WhenCall_Excute()
        {
            // Arrange
            var languageCode = "en-US";
            var currentUICulture = new CultureInfo(languageCode);
            var currentCulture = CultureInfo.CreateSpecificCulture(languageCode);

            var service = CreateLanguageService();

            // Action
            service.SetCulture("en-US");

            // Assert
            Assert.Equal(Thread.CurrentThread.CurrentUICulture, currentUICulture);
            Assert.Equal(Thread.CurrentThread.CurrentCulture, currentCulture);
        }

        private static SystemService CreateLanguageService(IDynamicRepository dynamicRepository = null)
        {
            var fakedynamicRepository = dynamicRepository ?? Mock.Of<IDynamicRepository>();

            return new SystemService(fakedynamicRepository);
        }
    }
}
