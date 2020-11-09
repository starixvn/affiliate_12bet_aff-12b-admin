using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
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
            var languageRepository = new Mock<ILanguageRepository>();
            var sessionService = new Mock<ISessionService>();
            languageRepository.Setup(l => l.GetLanguageList()).Returns(new List<Language> { new Language() });

            var service = CreateLanguageService(languageRepository.Object, sessionService.Object);

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

        private static LanguageService CreateLanguageService(ILanguageRepository languageRepository = null, ISessionService sessionService = null)
        {
            var fakeLanguageRepository = languageRepository ?? Mock.Of<ILanguageRepository>();
            var fakeSessionService = sessionService ?? Mock.Of<ISessionService>();

            return new LanguageService(fakeLanguageRepository, fakeSessionService);
        }
    }
}
