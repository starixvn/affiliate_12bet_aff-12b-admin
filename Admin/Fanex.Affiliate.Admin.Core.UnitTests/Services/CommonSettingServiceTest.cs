using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.Common;
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
    public class commonSettingServiceTest
    {
        [Fact]
        public void GetInChangeMarkets_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var commonSettingRepository = new Mock<ICommonSettingRepository>();
            commonSettingRepository.Setup(l => l.GetLanguageList()).Returns(new List<Language> { new Language { LanguageCode = "en-US", LanguageName = "English", ShortedCode = "en" } });

            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(s => s.UserClaim).Returns(new ClaimsPrincipal(new List<ClaimsIdentity> { new ClaimsIdentity(new List<Claim> {
                new Claim(Function.InChargeMarket, "en-US")
            }) }));

            var service = CreateCommonService(commonSettingRepository.Object, sessionService.Object);

            // Action
            var result = service.GetInChangeMarkets();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetProductList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var commonSettingRepository = new Mock<ICommonSettingRepository>();
            commonSettingRepository.Setup(l => l.GetProductList()).Returns(new List<Product> { new Product () });

            var service = CreateCommonService(commonSettingRepository.Object);

            // Action
            var result = service.GetProductList();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetAffTypeList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var commonSettingRepository = new Mock<ICommonSettingRepository>();
            commonSettingRepository.Setup(l => l.GetAffTypeList()).Returns(new List<DTO.Common.AffType> { new DTO.Common.AffType() });

            var service = CreateCommonService(commonSettingRepository.Object);

            // Action
            var result = service.GetAffTypeList();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetCountryList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var commonSettingRepository = new Mock<ICommonSettingRepository>();
            commonSettingRepository.Setup(l => l.GetCountryList()).Returns(new List<Country> { new Country() });

            var service = CreateCommonService(commonSettingRepository.Object);

            // Action
            var result = service.GetCountryList();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetVisibleTargetCountriesByUser_WhenCall_Excute()
        {
            // Arrange
            var commonSettingRepository = new Mock<ICommonSettingRepository>();
            commonSettingRepository.Setup(l => l.GetVisibleTargetCountriesByUser(It.IsAny<int>())).Returns(new List<TargetCountry> { new TargetCountry() });

            var service = CreateCommonService(commonSettingRepository.Object);

            // Action
            var result = service.GetVisibleTargetCountriesByUser(123);

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetVisibleTargetCountriesNotUsingByUser_WhenCall_Excute()
        {
            // Arrange
            var commonSettingRepository = new Mock<ICommonSettingRepository>();
            commonSettingRepository.Setup(l => l.GetVisibleTargetCountriesByUser(It.IsAny<int>())).Returns(new List<TargetCountry> { new TargetCountry { Status = 2 } });

            var service = CreateCommonService(commonSettingRepository.Object);

            // Action
            var result = service.GetVisibleTargetCountriesNotUsingByUser(123);

            // Assert
            Assert.Empty(result);
        }


        [Fact]
        public void SetCulture_WhenCall_Excute()
        {
            // Arrange
            var languageCode = "en-US";
            var currentUICulture = new CultureInfo(languageCode);
            var currentCulture = CultureInfo.CreateSpecificCulture(languageCode);

            var service = CreateCommonService();

            // Action
            service.SetCulture("en-US");

            // Assert
            Assert.Equal(Thread.CurrentThread.CurrentUICulture, currentUICulture);
            Assert.Equal(Thread.CurrentThread.CurrentCulture, currentCulture);
        }

        private static CommonSettingService CreateCommonService(ICommonSettingRepository commonSettingRepository = null, ISessionService sessionService = null)
        {
            var fakecommonSettingRepository = commonSettingRepository ?? Mock.Of<ICommonSettingRepository>();
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();

            return new CommonSettingService(fakecommonSettingRepository, fakeSessionService);
        }
    }
}
