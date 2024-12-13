namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using DTO;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.Constants;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.Repository;
    using TestHelper;
    using NSubstitute;
    using NSubstitute.ReturnsExtensions;
    using Xunit;

    public class LanguageServiceTest
    {
        private readonly ISystemRepository Repo;
        private readonly ISessionService SessionService;
        private readonly IMemoryCacheService CacheServive;

        public LanguageServiceTest()
        {
            Repo = Substitute.For<ISystemRepository>();
            SessionService = NSubstituteMockingHelper.FakeSessionService();
            CacheServive = Substitute.For<IMemoryCacheService>();
        }

        [Fact]
        public void GetFavoriteLanguage_WhenLangFromRequestIsValid_ReturnLang()
        {
            var validLang = SystemLanguage.SupportedLanguage.FirstOrDefault().Key;
            var code = SystemLanguage.SupportedLanguage[validLang];
            CacheServive.Get<IEnumerable<Language>>(CacheKey.LanguageList).Returns(CreateLanguages());
            SetUpContext(code);
            var service = CreateService();

            var result = service.GetFavoriteLanguage();

            Assert.Equal(validLang, result);
        }

        [Fact]
        public void GetFavoriteLanguage_WhenCookieLangIsValid_ReturnCookieLang()
        {
            var validLang = SystemLanguage.SupportedLanguage.FirstOrDefault().Key;
            CacheServive.Get<IEnumerable<Language>>(CacheKey.LanguageList).Returns(CreateLanguages());
            SetUpContext();
            HttpContext.Current.Request.Cookies.Add(new HttpCookie("afffavoritelangN8B", validLang));
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

            service.GetLanguageList();

            Repo.DidNotReceiveWithAnyArgs().GetLanguageList(false);
        }

        [Fact]
        public void GetSystemLanguage_WhenNotHaveInfoInCacheAndRepo_ReturnEmptyList()
        {
            var service = CreateService();
            CacheServive.Get<IEnumerable<Language>>(CacheKey.LanguageList).ReturnsNull();
            Repo.GetLanguageList(false).ReturnsNullForAnyArgs();

            var result = service.GetLanguageList();

            Repo.ReceivedWithAnyArgs().GetLanguageList(false);
            Assert.Empty(result);
        }

        [Fact]
        public void GetSystemLange_WhenRepoHaveInfoButCacheIsNot_SaveInfoInCache()
        {
            var service = CreateService();
            CacheServive.Get<IEnumerable<Language>>(CacheKey.LanguageList).ReturnsNull();
            Repo.GetLanguageList(false).ReturnsForAnyArgs(CreateLanguages());

            var result = service.GetLanguageList();

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
                new Language
                {
                    DisplayName = "VietNam", Code = "vi-VN", EnglishName = "VietNam", NativeName = "Viet Nam",
                    ShortedCode = "vn"
                },
                new Language
                {
                    DisplayName = "American", Code = "en-US", EnglishName = "American", NativeName = "American",
                    ShortedCode = "en"
                }
            };

        private SystemService CreateService()
            => new SystemService(Repo, SessionService, CacheServive);
    }
}
