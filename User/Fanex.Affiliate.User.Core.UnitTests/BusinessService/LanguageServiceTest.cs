using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.System;
using Fanex.Affiliate.User.Core.Repository;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    public class LanguageServiceTest
    {
        private readonly ISystemRepository Repo;
        private readonly ISessionService SessionService;
        private readonly IMemoryCacheService CacheServive;

        public LanguageServiceTest()
        {
            Repo = Substitute.For<ISystemRepository>();
            SessionService = NSubtituteMockingHelper.FakeSessionService();
            CacheServive = Substitute.For<IMemoryCacheService>();
        }

        [Fact]
        public void GetFavoriteLanguage_WhenLangFromRequestIsInvalid_ReturnLang()
        {
            var validLang = SystemLanguage.SupportedLanguage.FirstOrDefault().Key;
            var code = SystemLanguage.SupportedLanguage[validLang];
            SetUpContext(code);
            var service = CreateService();

            var result = service.GetFavoriteLanguage();

            Assert.Equal(validLang, result);
        }

        [Fact]
        public void GetFavoriteLanguage_WhenLangFromRequestIsValid_ReturnLang()
        {
            var validLang = SystemLanguage.SupportedLanguage.FirstOrDefault().Key;
            var code = SystemLanguage.SupportedLanguage[validLang];
            SetUpContext(code);

            CacheServive.Get<IEnumerable<Language>>(CacheKey.InternalLanguageList).Returns(CreateLanguages());
            this.SessionService.IsInternal.Returns(true);

            var service = CreateService();

            var result = service.GetFavoriteLanguage();

            Assert.Equal(validLang, result);
        }

        [Fact]
        public void GetFavoriteLanguage_WhenCookieLangIsInvalid_ReturnCookieLang()
        {
            var validLang = SystemLanguage.SupportedLanguage.FirstOrDefault().Key;
            SetUpContext();
            HttpContext.Current.Request.Cookies.Add(new HttpCookie("afffavoritelang", validLang));
            var service = CreateService();

            var result = service.GetFavoriteLanguage();

            Assert.Equal(validLang, result);
        }

        [Fact]
        public void GetFavoriteLanguage_WhenCookieLangIsValid_ReturnCookieLang()
        {
            var validLang = SystemLanguage.SupportedLanguage.FirstOrDefault().Key;
            SetUpContext();
            HttpContext.Current.Request.Cookies.Add(new HttpCookie("afffavoritelang", validLang));
            CacheServive.Get<IEnumerable<Language>>(CacheKey.LanguageList).Returns(CreateLanguages());
            var service = CreateService();

            var result = service.GetFavoriteLanguage();

            Assert.Equal(validLang, result);
        }

        [Fact]
        public void GetFavoriteLanguage_WhenRequestDidHaveLangInfo_ReturnDefaultLanguage()
        {
            var defaultLang = SystemLanguage.DefaultLanguageCode;
            SetUpContext();
            var service = CreateService();

            var result = service.GetFavoriteLanguage();

            Assert.Equal(defaultLang, result);
        }

        [Fact]
        public void GetSystemLanguages_WhenHaveInfoInCache_NotQueryFromRepo()
        {
            var service = CreateService();
            CacheServive.Get<IEnumerable<Language>>(CacheKey.LanguageList).Returns(CreateLanguages());

            service.GetSystemLanguages();

            Repo.DidNotReceiveWithAnyArgs().GetLanguageList(false);
        }

        [Fact]
        public void GetSystemLanguage_WhenNotHaveInfoInCacheAndRepo_ReturnEmptyList()
        {
            var service = CreateService();
            CacheServive.Get<IEnumerable<Language>>(CacheKey.LanguageList).ReturnsNull();
            Repo.GetLanguageList(false).ReturnsNullForAnyArgs();

            var result = service.GetSystemLanguages();

            Repo.ReceivedWithAnyArgs().GetLanguageList(false);
            Assert.Empty(result);
        }

        [Fact]
        public void GetSystemLange_WhenRepoHaveInfoButCacheIsNot_SaveInfoInCache()
        {
            var service = CreateService();
            CacheServive.Get<IEnumerable<Language>>(CacheKey.LanguageList).ReturnsNull();
            Repo.GetLanguageList(false).ReturnsForAnyArgs(CreateLanguages());

            var result = service.GetSystemLanguages();

            Repo.ReceivedWithAnyArgs().GetLanguageList(false);
            CacheServive.ReceivedWithAnyArgs().Set(null, default(IEnumerable<Language>));
            Assert.NotEmpty(result);
        }

        [Fact]
        public void SetCulture_WhenCall_NotThrowException()
        {
            var service = CreateService();

            service.SetCulture("en");

            Assert.True(true);
        }

        private static void SetUpContext(string lang = null)
        {
            var query = String.IsNullOrWhiteSpace(lang) ? null : $"lang={lang}";
            var request = new HttpRequest("111", "http://127.0.0.1:8080/controller/action", query);
            var response = new HttpResponse(new StringWriter());
            HttpContext.Current = new HttpContext(request, response);
        }

        private static IEnumerable<Language> CreateLanguages()
            => new List<Language>
            {
                new Language { DisplayName="VietNam", MarketId="vi-VN", MarketName="VietNam", MarketNativeName="Viet Nam", ShortedCode="vn" },
                new Language { DisplayName="England", MarketId="en-US", MarketName="England", MarketNativeName="England", ShortedCode="en" }
            };

        private SystemService CreateService()
            => new SystemService(Repo, SessionService, CacheServive);
    }
}
