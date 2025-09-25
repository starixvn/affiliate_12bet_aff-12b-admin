namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    using Criterias;
    using Data.Repository;
    using DTO;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.Constants;
    using Fanex.Affiliate.User.Core.CoreService;
    using NSubstitute;
    using NSubstitute.ReturnsExtensions;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using TestHelper;
    using Xunit;

    public class SystemServiceTest
    {
        private readonly IDynamicRepository Repository;
        private readonly ISessionService SessionService;
        private readonly IMemoryCacheService CacheServive;
        private readonly IIpLocationService IpLocationService;

        public SystemServiceTest()
        {
            Repository = Substitute.For<IDynamicRepository>();
            SessionService = NSubstituteMockingHelper.FakeSessionService();
            CacheServive = Substitute.For<IMemoryCacheService>();
            IpLocationService = Substitute.For<IIpLocationService>();
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

            Repository.DidNotReceiveWithAnyArgs().Fetch<Language>(Arg.Any<GetLanguageListCriteria>());
        }

        [Fact]
        public void GetSystemLanguage_WhenNotHaveInfoInCacheAndRepo_ReturnEmptyList()
        {
            CacheServive.Get<IEnumerable<Language>>(CacheKey.LanguageList).ReturnsNull();
            Repository.Fetch<Language>(Arg.Any<GetLanguageListCriteria>()).ReturnsForAnyArgs(new List<Language>());
            var service = CreateService();

            var result = service.GetLanguageList();

            Assert.Empty(result);
        }

        [Fact]
        public void GetSystemLange_WhenRepoHaveInfoButCacheIsNot_SaveInfoInCache()
        {
            CacheServive.Get<IEnumerable<Language>>(CacheKey.LanguageList).ReturnsNull();
            Repository.Fetch<Language>(Arg.Any<GetLanguageListCriteria>()).ReturnsForAnyArgs(CreateLanguages());
            var service = CreateService();

            var result = service.GetLanguageList();

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
            => new SystemService(Repository, SessionService, CacheServive, IpLocationService);
    }
}
