using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.Utilities;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    public class MarketingServiceTest
    {
        private readonly IMarketingRepository Repo;
        private readonly IAppSetting AppSetting;
        private readonly IMemoryCacheService CacheService;
        private readonly ISystemService LanguageService;

        public MarketingServiceTest()
        {
            Repo = Substitute.For<IMarketingRepository>();
            AppSetting = Substitute.For<IAppSetting>();
            CacheService = Substitute.For<IMemoryCacheService>();
            LanguageService = NSubtituteMockingHelper.FakeLanguageService();
        }

        [Fact]
        public void GetCategoryList_WhenHaveInfoInCache_NotCallRepoToQueryInfo()
        {
            var service = CreateService();
            CacheService.Get<IEnumerable<TargetProduct>>(null).ReturnsForAnyArgs(Constant.TargetProductList);

            service.GetTargetProductList(false);

            Repo.DidNotReceiveWithAnyArgs().GetTargetProductList(false);
        }

        [Fact]
        public void GetCategoryList_WhenNotHaveInfoInBothCacheAndRepo_ReturnEmptyList()
        {
            var service = CreateService();
            CacheService.Get<IEnumerable<TargetProduct>>(null).ReturnsNullForAnyArgs();
            Repo.GetTargetProductList(false).ReturnsNullForAnyArgs();

            var result = service.GetTargetProductList(false);

            Repo.ReceivedWithAnyArgs().GetTargetProductList(false);
            CacheService.ReceivedWithAnyArgs().Set(null, default(IEnumerable<TargetProduct>));
            Assert.Empty(result);
        }

        [Fact]
        public void GetGetLanguageList_WhenCall_CallLanguageService()
        {
            var service = CreateService();

            service.GetLanguageList();

            LanguageService.Received().GetSystemLanguages();
        }

        [Fact]
        public void GetSizeList_WhenCall_RepoCallGetSize()
        {
            var service = CreateService();
            var param = new SizeParam
            {
                AffType = "asia",
                TargetProduct = "cate",
                Language = "vi-VN"
            };

            service.GetSizeList(param, false);

            Repo.Received().GetSizeList(param, false);
        }

        [Fact]
        public void GetDomainList_WhenCacheNotHaveDomainInfo_RepoCall()
        {
            var service = CreateService();
            CacheService.Get<IEnumerable<Domain>>(CacheKey.DomainList).ReturnsNull();
            Repo.GetDomainList(0, false).ReturnsForAnyArgs(Constant.DomainList);

            var result = service.GetDomainList(0, false);

            Repo.Received().GetDomainList(0, false);
            CacheService.Received().Set(CacheKey.DomainList, Arg.Compat.Any<IEnumerable<Domain>>(), Number.Ten);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetDomainList_WhenBothCacheAndRepoNotHaveDomainInfo_ReturnEmptyList()
        {
            var service = CreateService();
            CacheService.Get<IEnumerable<Domain>>(CacheKey.DomainList).ReturnsNull();
            Repo.GetDomainList(0, false).ReturnsForAnyArgs(new List<Domain>());

            var result = service.GetDomainList(0, false);

            Repo.Received().GetDomainList(0, false);
            CacheService.Received().Set(CacheKey.DomainList, Arg.Compat.Any<IEnumerable<Domain>>(), Number.Ten);
            Assert.Empty(result);
        }

        [Fact]
        public void GetDomainList_WhenCacheHaveDomainInfo_RepoNotCall()
        {
            var service = CreateService();
            CacheService.Get<IEnumerable<Domain>>(CacheKey.DomainList).Returns(Constant.DomainList);

            var result = service.GetDomainList(0, false);

            Repo.DidNotReceive().GetDomainList(0, false);
            CacheService.DidNotReceive().Set(CacheKey.DomainList, Arg.Compat.Any<IEnumerable<Domain>>(), Number.Ten);
            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData("vi-VN")]
        [InlineData("th-TH")]
        public void GenerateLinkList_WhenSelectAllLanguagesAndLanguagesInSystemNotEmpty_ReturnNotEmptyList(string preferredLanguage)
        {
            var service = CreateService();
            var param = CreateLinkGenerationParam(preferredLanguage);
            param.SelectedLanguage = null;

            var result = service.GenerateLinkList(param);

            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData("vi-VN")]
        [InlineData("th-TH")]
        public void GenerateLinkList_WhenNotExistLanguageListFromSite_ReturnOneItem(string preferredLanguage)
        {
            var service = CreateService();
            var param = CreateLinkGenerationParam(preferredLanguage);
            param.LanguageList = null;
            param.SelectedLanguage = null;

            var result = service.GenerateLinkList(param);

            Assert.Equal(1, result.Count);
        }

        [Theory]
        [InlineData("vi-VN")]
        [InlineData("th-TH")]
        public void GenerateLinkList_WhenLanglistFromSiteNotEmpty_ReturnNotEmpty(string preferredLanguage)
        {
            var service = CreateService();
            var param = CreateLinkGenerationParam(preferredLanguage);

            var result = service.GenerateLinkList(param);

            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData("English")]
        [InlineData("Thai")]
        public void GetBannerList_WhenCall_RepoCall(string language)
        {
            var service = CreateService();
            var param = CreateMarketingBannerParam();
            var bannerList = CreateMarketingItemList(language);
            Repo.GetBannerList(param).Returns(bannerList);

            var result = service.GetBannerList(param);

            Repo.Received().GetBannerList(param);
            Assert.Equal(bannerList, result);
        }

        [Fact]
        public void GetLandingPageList_WhenCall_RepoCall()
        {
            var servcice = CreateService();

            servcice.GetLandingPageList(Constant.GroupName, false);

            Repo.Received().GetLandingPageList(false);
        }

        [Fact]
        public void CountNumberOfCopies_WhenCall_RepoCall()
        {
            var param = new CopiedBannerCounterParam();
            var servcice = CreateService();

            servcice.CountNumberOfCopies(param);

            Repo.Received().CountNumberOfCopies(param);
        }

        private MarketingBannerParam CreateMarketingBannerParam()
            => new MarketingBannerParam
            {
                AffId = 1,
                AffType = "asia",
                BannerDomain = "abc.com",
                TargetProduct = "cate",
                IsInternal = false,
                Language = "English",
                Nrows = 19,
                PageIndex = 1,
                PageSize = 20,
                SelectedDomain = new Domain { DomainId = 1, DomainName = "abc.com", IsSsl = true },
                SelectedDomainId = 1,
                SelectedPage = "SignUp",
                Size = "1020x90",
                TextLinkTemplate = "{scheme}://{domain}/{custid}/{target}/{lang}/index.html",
                TotalPages = 30
            };

        private IEnumerable<Banner> CreateMarketingItemList(string language)
            => new List<Banner>
            {
                new Banner
                {
                    BannerId = 1,
                    TargetProduct = "cate",
                    FileName = "abc.png",
                    Height = 1090,
                    Language = language,
                    LastUpdate = DateTime.Now,
                    Message = "abc",
                    RedirectLink = "abc.com",
                    ScriptSrc = "banner.com/script",
                    SelectedDomainName = "SignUp",
                    Size = "1090x90",
                    Width = 90
                },
                new Banner
                {
                    BannerId = 1,
                    TargetProduct = "cate",
                    FileName = "abc.png",
                    Height = 1090,
                    Language = "English",
                    LastUpdate = DateTime.Now,
                    Message = "abc",
                    RedirectLink = "abc.com",
                    ScriptSrc = "banner.com/script",
                    SelectedDomainName = "SignUp",
                    Size = string.Empty,
                    Width = 90
                }
            };

        private LinkGenerationParam CreateLinkGenerationParam(string language)
            => new LinkGenerationParam
            {
                AffId = 1,
                DomainId = 1,
                IsInternal = false,
                LanguageList = new List<SelectListItem>
                {
                    new SelectListItem {Text = "VietNam", Selected = true, Value="vi-VN" },
                    new SelectListItem {Text = "Thai", Selected = true, Value = "th-TH" }
                },
                PreferredLanguage = language,
                SelectedDomain = new Domain { DomainId = 1, DomainName = "abc.com", IsSsl = false },
                SelectedDomainId = 1,
                SelectedLanguage = language,
                SelectedPage = "SignUp",
                TextLinkTemplate = "{scheme}://{domain}/{custid}/{target}/{lang}/index.html"
            };

        private static void SetUpContext(string lang = null)
        {
            var query = String.IsNullOrWhiteSpace(lang) ? null : $"lang={lang}";
            var request = new HttpRequest("111", "http://127.0.0.1:8080/controller/action", query);
            var response = new HttpResponse(new StringWriter());
            HttpContext.Current = new HttpContext(request, response);
        }

        private MarketingService CreateService()
            => new MarketingService(Repo, CacheService, LanguageService, AppSetting);
    }
}
