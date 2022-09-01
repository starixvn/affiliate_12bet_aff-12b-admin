using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Moq;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Threading;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services
{
    public class LanguageServiceTest
    {
        [Fact]
        public void GetInChangeMarkets_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var languageRepository = new Mock<ILanguageRepository>();
            languageRepository.Setup(l => l.GetLanguageList()).Returns(new List<Language> { new Language { LanguageCode = "en-US", LanguageName = "English", ShortedCode = "en" } });

            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(s => s.UserClaim).Returns(new ClaimsPrincipal(new List<ClaimsIdentity> { new ClaimsIdentity(new List<Claim> {
                new Claim(Function.InChargeMarket, "en-US")
            }) }));

            var service = CreateLanguageService(languageRepository.Object, sessionService.Object);

            // Action
            var result = service.GetInChangeMarkets();

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
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();

            return new LanguageService(fakeLanguageRepository, fakeSessionService);
        }
    }
}
