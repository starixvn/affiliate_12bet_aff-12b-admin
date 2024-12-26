namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    using System;
    using System.Collections.Generic;
    using DTO;
    using DTO.UserPanel;
    using Enum;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.Constants;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
    using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
    using Fanex.Affiliate.User.Core.Repository.UserPanel;
    using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
    using Fanex.Affiliate.User.Core.Utilities;
    using MetalModels;
    using NSubstitute;
    using NSubstitute.ReturnsExtensions;
    using Xunit;

    public class MarketingServiceTest
    {
        private readonly IMarketingRepository Repo;
        private readonly IMemoryCacheService CacheService;
        private readonly ISystemService LanguageService;
        private readonly IAppSetting AppSetting;

        public MarketingServiceTest()
        {
            Repo = Substitute.For<IMarketingRepository>();
            CacheService = Substitute.For<IMemoryCacheService>();
            LanguageService = NSubstituteMockingHelper.FakeLanguageService();
            AppSetting = Substitute.For<IAppSetting>();
        }

        [Fact]
        public void GetCategoryList_WhenHaveInfoInCache_NotCallRepoToQueryInfo()
        {
            var service = CreateService();
            CacheService.Get<IEnumerable<CategoryType>>(null).ReturnsForAnyArgs(Constant.CategoryList);

            service.GetCategoryList(false, AffiliateLinkType.AffiliateLink, false);

            Repo.DidNotReceiveWithAnyArgs().GetCategoryList(null, false);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetCategoryList_WhenNotHaveInfoInBothCacheAndRepo_ReturnEmptyList(bool isMediaPage)
        {
            var service = CreateService();
            CacheService.Get<IEnumerable<CategoryType>>(null).ReturnsNullForAnyArgs();
            Repo.GetCategoryList(null, false).ReturnsNullForAnyArgs();

            var result = service.GetCategoryList(isMediaPage, AffiliateLinkType.AffiliateLink, false);

            Repo.ReceivedWithAnyArgs().GetCategoryList(null, false);
            CacheService.ReceivedWithAnyArgs().Set(null, default(IEnumerable<CategoryType>));
            Assert.Empty(result);
        }

        [Fact]
        public void GetCurrencyList_WhenNotHaveInfoInCache_CallInfoFromRepo()
        {
            var service = CreateService();
            CacheService.Get<IEnumerable<CurrencyType>>(CacheKey.CurrencyList).ReturnsNull();
            Repo.GetCurrencyList(false).ReturnsForAnyArgs(Constant.CurrencyType);

            service.GetCurrencyList(false);

            Repo.Received().GetCurrencyList(false);
            CacheService.Received().Set(CacheKey.CurrencyList, Arg.Compat.Any<IEnumerable<CurrencyType>>());
        }

        [Fact]
        public void GetCurrencyList_WhenNotHaveInfoInBothCacheAndRepo_ReturnEmptyList()
        {
            var service = CreateService();
            CacheService.Get<IEnumerable<CurrencyType>>(CacheKey.CurrencyList).ReturnsNull();
            Repo.GetCurrencyList(false).ReturnsNullForAnyArgs();

            var result = service.GetCurrencyList(false);

            Assert.Empty(result);
        }

        [Fact]
        public void GetCurrencyList_WhenHaveInfoInCache_NotCallInfoFromRepo()
        {
            var service = CreateService();
            CacheService.Get<IEnumerable<CurrencyType>>(CacheKey.CurrencyList).Returns(Constant.CurrencyType);

            service.GetCurrencyList(false);

            Repo.DidNotReceive().GetCurrencyList(false);
            CacheService.DidNotReceive().Set(CacheKey.CurrencyList, Arg.Compat.Any<IEnumerable<CurrencyType>>());
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
                SubCategory = "sub",
                TargetId = AffiliateLinkType.AffiliateLink
            };

            service.GetSizeList(param, false);

            Repo.Received().GetSizeList(param, false);
        }

        [Fact]
        public void GetAvailableDomainList_WhenCacheNotHaveDomainInfo_RepoCall()
        {
            var service = CreateService();
            CacheService.Get<IEnumerable<Domain>>(CacheKey.DomainList).ReturnsNull();
            Repo.GetAvailableDomainList(0, true, false).ReturnsForAnyArgs(Constant.DomainList);

            var result = service.GetAvailableDomainList(0, AffiliateLinkType.AffiliateLink, false);

            Repo.Received().GetAvailableDomainList(0, true, false);
            CacheService.Received().Set(CacheKey.DomainList, Arg.Compat.Any<IEnumerable<Domain>>(), Number.Ten);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetAvailableDomainList_WhenBothCacheAndRepoNotHaveDomainInfo_ReturnEmptyList()
        {
            var service = CreateService();
            CacheService.Get<IEnumerable<Domain>>(CacheKey.SubDomainList).ReturnsNull();
            Repo.GetAvailableDomainList(0, true, false).ReturnsNullForAnyArgs();

            var result = service.GetAvailableDomainList(0, AffiliateLinkType.SubAffiliateLink, false);

            Repo.Received().GetAvailableDomainList(0, false, false);
            CacheService.Received().Set(CacheKey.SubDomainList, Arg.Compat.Any<IEnumerable<Domain>>(), Number.Ten);
            Assert.Empty(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GenerateLinkList_WhenSelectAllLanguagesAndLanguagesInSystemNotEmpty_ReturnNotEmptyList(bool isSsl)
        {
            var service = CreateService();
            LanguageService.GetLanguageList().Returns(GetLanguageList());
            var param = CreateLinkGenerationParam(isSsl);
            param.SelectedLanguage = null;

            var result = service.GenerateLinkList(param);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void GenerateLinkList_WhenSelectedLanguageNotEmpty_ReturnEmpty()
        {
            var service = CreateService();
            LanguageService.GetLanguageList().Returns(GetLanguageList());
            var param = CreateLinkGenerationParam();
            param.SelectedLanguage = "abc";

            var result = service.GenerateLinkList(param);

            Assert.Empty(result);
        }

        [Fact]
        public void GenerateLinkList_WhenSelectedLanguageNotEmpty_ReturnNotEmpty()
        {
            var service = CreateService();
            LanguageService.GetLanguageList().Returns(GetLanguageList());
            var param = CreateLinkGenerationParam();

            var result = service.GenerateLinkList(param);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void GenerateLinkList_WhenSelectedLanguageEmpty_ReturnEmpty()
        {
            var service = CreateService();
            LanguageService.GetLanguageList().Returns(new List<Language>());
            var param = CreateLinkGenerationParam();

            var result = service.GenerateLinkList(param);

            Assert.Empty(result);
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
            LanguageService.GetLanguageList().Returns(GetLanguageList());

            var result = service.GetBannerList(param);

            Repo.Received().GetBannerList(param);
            Assert.Equal(bannerList, result);
        }

        [Fact]
        public void GetBannerList_WhenCall_RepoCallReturnNull()
        {
            var service = CreateService();
            var param = CreateMarketingBannerParam();
            Repo.GetBannerList(param).Returns(new List<MarketingItem>());
            LanguageService.GetLanguageList().Returns(GetLanguageList());

            var result = service.GetBannerList(param);

            Repo.Received().GetBannerList(param);
            Assert.Empty(result);
        }

        [Fact]
        public void GetLandingPageList_WhenCall_RepoCall()
        {
            var service = CreateService();

            service.GetLandingPageList(string.Empty, false);

            Repo.Received().GetLandingPageList(false);
        }

        [Fact]
        public void GetLandingPageList_WhenCallWithSubAff_ReturnNotEmptyData()
        {
            var service = CreateService();

            var result = service.GetLandingPageList(string.Empty, false, AffiliateLinkType.SubAffiliateLink);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetTextLinkTemplate_WhenCallWithTargetToMember_ReturnCorrectData()
        {
            AppSetting.TextLinkTemplate.Returns("TargetToMember");
            var service = CreateService();

            var result = service.GetTextLinkTemplate(AffiliateLinkType.AffiliateLink);

            Assert.Equal("TargetToMember", result);
        }

        [Fact]
        public void GetTextLinkTemplate_WhenCallWithTargetToSubAff_ReturnCorrectData()
        {
            AppSetting.SubAffTextLinkTemplate.Returns("TargetToSubAff");
            var service = CreateService();

            var result = service.GetTextLinkTemplate(AffiliateLinkType.SubAffiliateLink);

            Assert.Equal("TargetToSubAff", result);
        }

        [Fact]
        public void GetTextLinkTemplate_WhenCallWithInvalidTarget_ReturnCorrectData()
        {
            AppSetting.TextLinkTemplate.Returns("InvalidTarget");
            var service = CreateService();

            var result = service.GetTextLinkTemplate(0);

            Assert.Equal("InvalidTarget", result);
        }

        [Theory]
        [InlineData(ErrCodes.OneHundredOne, "", false)]
        [InlineData(ErrCodes.OneHundredTwo, "", false)]
        [InlineData(ErrCodes.OneHundredThree, "", false)]
        [InlineData(123, "Abc", false)]
        [InlineData(0, "", true)]
        public void AddMarketingChannel_WhenCall_RepoCall(int code, string errMsg, bool isOk)
        {
            var resultInfoExpected = new ResultInfo
            {
                Code = code,
                Message = errMsg,
                IsOk = isOk
            };

            var service = CreateService();
            Repo.AddMarketingChannel(Arg.Any<int>(), Arg.Any<string>(), Arg.Any<string>()).Returns(resultInfoExpected);

            var result = service.AddMarketingChannel(1, "xyz");

            Repo.Received().AddMarketingChannel(1, "xyz", "xyz");
            Assert.Equal(isOk, result.IsOk);
        }

        [Fact]
        public void GetChannels_WhenCall_RepoCall()
        {
            var service = CreateService();

            service.GetChannels(123);

            Repo.Received().GetMarketingChannels(123);
        }

        [Theory]
        [InlineData(ErrCodes.OneHundredOne, "", false)]
        [InlineData(ErrCodes.OneHundredTwo, "", false)]
        [InlineData(123, "Abc", false)]
        [InlineData(0, "", true)]
        public void ToggleMarketingLink_WhenCall_RepoCall(int code, string errMsg, bool isOk)
        {
            var resultInfoExpected = new ResultInfo
            {
                Code = code,
                Message = errMsg,
                IsOk = isOk
            };
            var service = CreateService();
            var param = new MarketingLinkParam();
            Repo.ToggleMarketingLink(Arg.Any<MarketingLinkParam>()).Returns(resultInfoExpected);

            var result = service.ToggleMarketingLink(param);

            Repo.Received().ToggleMarketingLink(param);
            Assert.Equal(isOk, result.IsOk);
        }

        [Fact]
        public void GetUsingMarketingLinks_WhenCall_RepoCall()
        {
            var service = CreateService();

            service.GetUsingMarketingLinks(123);

            Repo.Received().GetUsingMarketingLink(123);
        }

        private MarketingBannerParam CreateMarketingBannerParam()
            => new MarketingBannerParam
            {
                AffId = 1,
                AffType = "asia",
                BannerDomain = "abc.com",
                Category = "cate",
                Currency = "USD",
                DeviceType = 1,
                IsInternal = false,
                Language = "English",
                NRows = 19,
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

        private IEnumerable<MarketingItem> CreateMarketingItemList(string language)
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
                 Language = language,
                 LastUpdate = DateTime.Now,
                 Message = "abc",
                 RedirectLink = "abc.com",
                 ScriptSrc = "banner.com/script",
                 SelectedDomainName = "SignUp",
                 Size = "1090x90",
                 TextId = 2,
                 Width = 90
             },
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
                 Size = string.Empty,
                 TextId = 2,
                 Width = 90
             }
            };

        private LinkGeneratingParam CreateLinkGenerationParam(bool isSsl = false)
            => new LinkGeneratingParam
            {
                AffId = 1,
                DomainId = 1,
                IsInternal = false,
                PreferredLanguage = "vi-VN",
                SelectedDomain = new Domain { DomainId = 1, DomainName = "abc.com", IsSsl = isSsl },
                SelectedDomainId = 1,
                SelectedLanguage = "vi-VN",
                SelectedPage = "SignUp",
                TextLinkTemplate = "{scheme}://{domain}/{custid}/{target}/{lang}/index.html"
            };

        private IEnumerable<Language> GetLanguageList()
            => new List<Language>
            {
             new Language{DisplayName="VietNam", Code="vi-VN", EnglishName="VietNam", NativeName="Viet Nam", ShortedCode="vn"},
             new Language { DisplayName="English", Code="en-US", EnglishName="English", NativeName="English", ShortedCode="en" }
            };

        private MarketingService CreateService()
            => new MarketingService(Repo, CacheService, AppSetting, LanguageService);
    }
}
