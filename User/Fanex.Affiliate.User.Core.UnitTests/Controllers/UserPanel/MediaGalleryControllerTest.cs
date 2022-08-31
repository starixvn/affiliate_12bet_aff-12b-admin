using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers.UserPanel;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.Utilities;
using NSubstitute;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers.UserPanel
{
    public class MediaGalleryControllerTest
    {
        private readonly ISessionService SessionService;
        private readonly IAppSetting AppSetting;
        private readonly ILanguageService LanguageService;
        private readonly IMarketingService MarketingService;

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
            marketingService.GetTextList(Arg.Compat.Any<MarketingTextParam>()).Returns(new List<MarketingItem>
            {
                new MarketingItem { BannerId = 1, TextId = 1 }
            });
            marketingService.GetCategoryList(Arg.Compat.Any<bool>(), Arg.Compat.Any<bool>()).Returns(Constant.CategoryList);
            marketingService.GetLanguageList().Returns(Constant.LanguageList);
            marketingService.GetCurrencyList(Arg.Compat.Any<bool>()).Returns(Constant.CurrencyType);
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
    }
}
