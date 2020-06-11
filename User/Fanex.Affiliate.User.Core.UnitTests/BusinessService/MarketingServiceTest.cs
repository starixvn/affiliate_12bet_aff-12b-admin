using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    public class MarketingServiceTest
    {
        private readonly IMarketingRepository Repo;
        private readonly IMemoryCacheService CacheService;
        private readonly ILanguageService LanguageService;

        public MarketingServiceTest()
        {
            Repo = Substitute.For<IMarketingRepository>();
            CacheService = Substitute.For<IMemoryCacheService>();
            LanguageService = NSubtituteMockingHelper.FakeLanguageService();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetCategoryList_WhenHaveInfoInCache_NotCallRepoToQueryInfo(bool isMediaPage)
        {
            var service = CreateService();
            CacheService.Get<IEnumerable<CategoryType>>(null).ReturnsForAnyArgs(Constant.CategoryList);

            service.GetCategoryList(isMediaPage, false);

            Repo.DidNotReceiveWithAnyArgs().GetCategoryList(isMediaPage, false);
        }

        [Fact]
        public void GetCategoryList_WhenNotHaveInfoInBothCacheAndRepo_ReturnEmptyList()
        {
            var service = CreateService();
            CacheService.Get<IEnumerable<CategoryType>>(null).ReturnsNullForAnyArgs();
            Repo.GetCategoryList(false, false).ReturnsNullForAnyArgs();

            var result = service.GetCategoryList(false, false);

            Repo.ReceivedWithAnyArgs().GetCategoryList(false, false);
            CacheService.ReceivedWithAnyArgs().Set(null, default(IEnumerable<CategoryType>));
            Assert.Empty(result);
        }

        [Fact]
        public void GetGetLanguageList_WhenCall_CallLanguageService()
        {
            var service = CreateService();

            service.GetLanguageList(false);

            LanguageService.Received().GetSystemLanguages(false);
        }

        [Fact]
        public void GetCurrecyList_WhenNotHaveInfoInCache_CallInfoFromRepo()
        {
            var service = CreateService();
            CacheService.Get<IEnumerable<CurrencyType>>(CacheKey.CurrencyList).ReturnsNull();
            Repo.GetCurrencyList(false).ReturnsForAnyArgs(Constant.CurrencyType);

            service.GetCurrencyList(false);

            Repo.Received().GetCurrencyList(false);
            CacheService.Received().Set(CacheKey.CurrencyList, Arg.Compat.Any<IEnumerable<CurrencyType>>());
        }

        [Fact]
        public void GetCurrecyList_WhenNotHaveInfoInBothCacheAndRepo_ReturnEmptyList()
        {
            var service = CreateService();
            CacheService.Get<IEnumerable<CurrencyType>>(CacheKey.CurrencyList).ReturnsNull();
            Repo.GetCurrencyList(false).ReturnsNullForAnyArgs();

            var result = service.GetCurrencyList(false);

            Assert.Empty(result);
        }

        [Fact]
        public void GetSizeList_WhenCall_RepoCallGetSize()
        {
            var service = CreateService();
            var param = new SizeParam
            {
                AffType = "asia",
                Category = "cate",
                Currency = "USD",
                DeviceType = 1,
                ImageType = "png",
                Language = "vi-VN",
                SubCategory = "sub"
            };

            service.GetSizeList(param, false);

            Repo.Received().GetSizeList(param, false);
        }

        [Fact]
        public void GetBannerTypeList_WhenCacheNotHaveBannerTypeInfo_RepoCall()
        {
            var service = CreateService();
            CacheService.Get<IEnumerable<BannerType>>(CacheKey.BannerTypeList).ReturnsNullForAnyArgs();
            Repo.GetBannerTypeList(false).ReturnsForAnyArgs(Constant.BannerTypeList);

            var result = service.GetBannerTypeList(false);

            Repo.Received().GetBannerTypeList(false);
            CacheService.Received().Set(CacheKey.BannerTypeList, Arg.Compat.Any<IEnumerable<BannerType>>());
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetBannerTypeList_WhenBothCacheAndRepoNotHaveBannerTypeInfo_ReturnEmptyList()
        {
            var service = CreateService();
            CacheService.Get<IEnumerable<BannerType>>(CacheKey.BannerTypeList).ReturnsNull();
            Repo.GetBannerTypeList(false).ReturnsNullForAnyArgs();

            var result = service.GetBannerTypeList(false);

            Repo.Received().GetBannerTypeList(false);
            CacheService.DidNotReceiveWithAnyArgs().Set(CacheKey.BannerTypeList, Arg.Compat.Any<IEnumerable<BannerType>>());
            Assert.Empty(result);

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
            Repo.GetDomainList(0, false).ReturnsNullForAnyArgs();

            var result = service.GetDomainList(0, false);

            Repo.Received().GetDomainList(0, false);
            CacheService.DidNotReceiveWithAnyArgs().Set(CacheKey.DomainList, Arg.Compat.Any<IEnumerable<Domain>>(), Number.Ten);
            Assert.Empty(result);
        }

        [Fact]
        public void GenerateLinkList_WhenNotHaveSelectedLanguage_ReturnEmptyList()
        {
            var service = CreateService();
            var param = CreateLinkGenerationParam();
            param.SelectedLanguage = null;

            var result = service.GenerateLinkList(param);

            Assert.Empty(result);
        }

        [Fact]
        public void GenerateLinkList_WhenNotExistLanguageListFromSite_ReturnOneItem()
        {
            var service = CreateService();
            var param = CreateLinkGenerationParam();
            param.LanguageList = null;

            var result = service.GenerateLinkList(param);

            Assert.Equal(1, result.Count);
        }

        [Fact]
        public void GenerateLinkList_WhenLanglistFromSiteNotEmpty_ReturnNotEmpty()
        {
            var service = CreateService();
            var param = CreateLinkGenerationParam();

            var result = service.GenerateLinkList(param);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetBannerList_WhenCall_RepoCall()
        {
            var service = CreateService();
            var param = CreateMarketingBannerParam();
            var bannerList = CreateMarketingItemList();
            Repo.GetBannerList(param).Returns(bannerList);

            var result = service.GetBannerList(param);

            Repo.Received().GetBannerList(param);
            Assert.Equal(bannerList, result);
        }

        [Fact]
        public void GetTextLink_WhenCall_RepoCall()
        {
            var service = CreateService();
            var param = CreateMarketingTextParam();
            var textList = CreateMarketingItemList();
            Repo.GetTextList(param).Returns(textList);
            SetUpContext();

            var result = service.GetTextList(param);

            Repo.Received().GetTextList(param);
            Assert.Equal(textList, result);
        }

        [Fact]
        public void GetLandingPageList_WhenCall_RepoCall()
        {
            var servcice = CreateService();

            servcice.GetLandingPageList(false);

            Repo.Received().GetLandingPageList(false);
        }

        private MarketingTextParam CreateMarketingTextParam()
            => new MarketingTextParam
            {
                AffId = 1,
                Category = "cate",
                Currency = "USD",
                DeviceType = 1,
                IsInternal = false,
                Language = "English",
                Nrows = 19,
                PageIndex = 1,
                PageSize = 20,
                SelectedDomain = new Domain { DomainId = 1, DomainName = "abc.com", IsSsl = false },
                SelectedDomainId = 1,
                SelectedPage = "SignUp",
                TextLinkTemplate = "{scheme}://{domain}/{custid}/{target}/{lang}/index.html",
                TotalPages = 30,
                ScriptUrl = "banner.com"
            };

        private MarketingBannerParam CreateMarketingBannerParam()
            => new MarketingBannerParam
            {
                AffId = 1,
                AffType = "asia",
                BannerDomain = "abc.com",
                Category = "cate",
                Currency = "USD",
                DeviceType = 1,
                ImageType = "png",
                IsInternal = false,
                Language = "English",
                Nrows = 19,
                PageIndex = 1,
                PageSize = 20,
                SelectedDomain = new Domain { DomainId = 1, DomainName = "abc.com", IsSsl = true },
                SelectedDomainId = 1,
                SelectedPage = "SignUp",
                Size = "1020x90",
                SubCategory = "sub",
                TextLinkTemplate = "{scheme}://{domain}/{custid}/{target}/{lang}/index.html",
                TotalPages = 30
            };

        private IEnumerable<MarketingItem> CreateMarketingItemList()
            => new List<MarketingItem>
            {
                new MarketingItem
                {
                    BannerId = 1,
                    Category = "cate",
                    Currency = "USD",
                    DeviceType = "mobile",
                    FileName = "abc.png",
                    Height = 1090,
                    ImageType = "png",
                    Language = "English",
                    LastUpdate = DateTime.Now,
                    Message = "abc",
                    RedirectLink = "abc.com",
                    ScriptSrc = "banner.com/script",
                    SelectedDomainName = "SignUp",
                    Size = "1090x90",
                    TextId = 2,
                    Width = 90
                }
            };

        private LinkGenerationParam CreateLinkGenerationParam()
            => new LinkGenerationParam
            {
                AffId = 1,
                DomainId = 1,
                IsInternal = false,
                LanguageList = new List<SelectListItem>
                {
                    new SelectListItem {Text = "VietNam", Selected = true, Value="vi-VN" },
                    new SelectListItem {Text = "England", Selected = true, Value = "en-UK" }
                },
                PreferredLanguage = "vi-VN",
                SelectedDomain = new Domain { DomainId = 1, DomainName = "abc.com", IsSsl = false },
                SelectedDomainId = 1,
                SelectedLanguage = "vi-VN",
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
            => new MarketingService(Repo, CacheService, LanguageService);
    }
}
