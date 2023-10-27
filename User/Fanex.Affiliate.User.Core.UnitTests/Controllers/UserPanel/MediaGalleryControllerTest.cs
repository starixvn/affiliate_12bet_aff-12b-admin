using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers.UserPanel;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.Utilities;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers.UserPanel
{
    public class MediaGalleryControllerTest
    {
        private readonly ISessionService SessionService;
        private readonly IAppSetting AppSetting;
        private readonly ISystemService LanguageService;
        private readonly IMarketingService MarketingService;

        private static IEnumerable<SizeType> SizeTypes => new List<SizeType>
        {
            new SizeType { SizeId = 1, Size = "1080x1920" },
            new SizeType { SizeId = 2, Size = "640x256" },
        };

        private static IEnumerable<Banner> MarketingItems => new List<Banner>
        {
            new Banner {BannerId =1 },
            new Banner {BannerId  = 2 }
        };

        public MediaGalleryControllerTest()
        {
            SessionService = NSubtituteMockingHelper.FakeSessionService();
            AppSetting = Substitute.For<IAppSetting>();
            LanguageService = NSubtituteMockingHelper.FakeLanguageService();
            MarketingService = NSubtituteMockingHelper.FakeMarkengtingService();
        }

        [Fact]
        public void Index_WhenCall_ReturnCorrectView()
        {
            var controller = new MediaGalleryController(SessionService, AppSetting, LanguageService, MarketingService);
            var result = controller.Index() as ViewResult;
            Assert.Equal("~/Views/UserPanel/AccountOverview/MediaGallery/Index.cshtml", result.ViewName);
        }

        [Fact]
        public void Index_WhenCallWithDomainEmpty_ReturnCorrectView()
        {
            var marketingService = Substitute.For<IMarketingService>();

            marketingService.GetTargetProductList(Arg.Compat.Any<bool>()).Returns(Constant.TargetProductList);
            marketingService.GetLanguageList().Returns(Constant.LanguageList);
            marketingService.GenerateLinkList(Arg.Compat.Any<LinkGenerationParam>()).Returns(Constant.LinkList);
            var controller = new MediaGalleryController(SessionService, AppSetting, LanguageService, marketingService);

            var result = controller.Index() as ViewResult;

            Assert.Contains("MediaGallery/Index.cshtml", result.ViewName);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void CountNumberOfCopies_WhenCall_ReturnCorrectData(bool isInternal)
        {
            SessionService.IsInternal.ReturnsForAnyArgs(isInternal);
            var marketingService = Substitute.For<IMarketingService>();
            marketingService.CountNumberOfCopies(Arg.Compat.Any<CopiedBannerCounterParam>()).Returns(new ResultInfo { IsOk = true });
            var controller = new MediaGalleryController(SessionService, AppSetting, LanguageService, marketingService);

            var result = controller.CountNumberOfCopies(new CopiedBannerCounterParam { BannerId = 123 }) as JsonResult;
            var data = result.Data as ResultInfo;

            Assert.Equal(!isInternal, data.IsOk);
        }

        [Fact]
        public void GetSize_WhenCall_ReturnCorrectJson()
        {
            MarketingService.GetSizeList(Arg.Compat.Any<SizeParam>(), Arg.Compat.Any<bool>())
                .Returns(SizeTypes);
            var controller = new MediaGalleryController(SessionService, AppSetting, LanguageService, MarketingService);
            var param = new SizeParam { Language = Constant.DefaultLanguage };

            var result = controller.GetSize(param) as JsonResult;
            var model = result.Data as IEnumerable<SizeType>;

            Assert.Collection(model, t => Assert.Contains(SizeTypes, s => s.SizeId == t.SizeId), t => Assert.Contains(SizeTypes, s => s.SizeId == t.SizeId));
            Assert.Equal(JsonRequestBehavior.AllowGet, result.JsonRequestBehavior);
        }

        [Fact]
        public void GetBanner_WhenCallWithNoSelectedDomain_ReturnEmptyModelItem()
        {
            MarketingService.GetDomainList(Arg.Compat.Any<int>(), Arg.Compat.Any<bool>()).Returns(new List<Domain>());
            var controller = new MediaGalleryController(SessionService, AppSetting, LanguageService, MarketingService);
            var param = new MarketingBannerParam();

            var result = controller.GetBanner(param) as ViewResult;
            var model = result.Model as IEnumerable<Banner>;

            Assert.Contains("AccountOverview/Partial/_BannerList.cshtml", result.ViewName);
            Assert.Empty(model);
        }

        [Fact]
        public void GetBanner_WhenCall_ReturnCorrectModel()
        {
            var bannerItems = MarketingItems;
            MarketingService.GetBannerList(Arg.Compat.Any<MarketingBannerParam>()).Returns(bannerItems);
            var controller = new MediaGalleryController(SessionService, AppSetting, LanguageService, MarketingService);
            var domainId = Constant.DomainList.First().DomainId;
            var param = new MarketingBannerParam { SelectedDomainId = domainId };

            var result = controller.GetBanner(param) as ViewResult;
            var model = result.Model as IEnumerable<Banner>;

            Assert.Contains("AccountOverview/Partial/_BannerList.cshtml", result.ViewName);
            Assert.NotEmpty(model);
        }
    }
}
