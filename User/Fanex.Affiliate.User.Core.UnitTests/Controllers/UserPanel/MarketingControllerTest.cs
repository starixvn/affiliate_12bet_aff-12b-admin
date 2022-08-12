using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers.UserPanel;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.Extensions;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers.UserPanel
{
    public class MarketingControllerTest
    {
        private readonly ISessionService SessionService;
        private readonly IAppSetting AppSetting;
        private readonly ILanguageService LanguageService;
        private readonly IMarketingService MarketingService;
        private readonly IQrCodeGeneratorService QrCodeService;

        private const string BASE64_IMAGE = "base64Image";
        private static IEnumerable<SizeType> SizeTypes => new List<SizeType>
        {
            new SizeType { SizeId = 1, Size = "1080x1920" },
            new SizeType { SizeId = 2, Size = "640x256" },
        };

        private static IEnumerable<MarketingItem> MarketingItems => new List<MarketingItem>
        {
            new MarketingItem {BannerId =1 },
            new MarketingItem {BannerId  = 2 }
        };

        private static Image Image => Image.FromStream(new MemoryStream(Convert.FromBase64String(Constant.LogoImageBase64)));

        public MarketingControllerTest()
        {
            SessionService = NSubtituteMockingHelper.FakeSessionService();
            AppSetting = Substitute.For<IAppSetting>();
            LanguageService = NSubtituteMockingHelper.FakeLanguageService();
            MarketingService = NSubtituteMockingHelper.FakeMarkengtingService();
            QrCodeService = Substitute.For<IQrCodeGeneratorService>();
        }

        [Fact]
        public void Index_WhenCall_ReturnCorrectView()
        {
            using (var image = Image)
            {
                QrCodeService.GetImage(Arg.Compat.Any<string>()).Returns(image);
                QrCodeService.GenerateBase64QrCode(Arg.Compat.Any<string>(), Arg.Compat.Any<Image>(), Arg.Compat.Any<bool>())
                    .Returns(BASE64_IMAGE);

                var controller = new MarketingController(SessionService, AppSetting, LanguageService, MarketingService, QrCodeService);
                controller = NSubtituteMockingHelper.FakeController(controller) as MarketingController;
                controller.HttpContext.Server.MapPath(Arg.Compat.Any<string>()).Returns(String.Empty);
                var result = controller.Index() as ViewResult;
                var model = result.Model as MarketingViewModel;

                Assert.Contains("AccountOverview/Partial/_GeneratedQrCode.cshtml", result.ViewName);
                Assert.Equal(Constant.LinkList.Count, model.QrCodeList.Count);
            }
        }

        [Fact]
        public void Index_WhenCallWithDomainEmpty_ReturnCorrectView()
        {
            using (var image = Image)
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

                QrCodeService.GetImage(Arg.Compat.Any<string>()).Returns(image);
                QrCodeService.GenerateBase64QrCode(Arg.Compat.Any<string>(), Arg.Compat.Any<Image>(), Arg.Compat.Any<bool>())
                    .Returns(BASE64_IMAGE);

                var controller = new MarketingController(SessionService, AppSetting, LanguageService, marketingService, QrCodeService);
                controller = NSubtituteMockingHelper.FakeController(controller) as MarketingController;
                controller.HttpContext.Server.MapPath(Arg.Compat.Any<string>()).Returns(String.Empty);
                var result = controller.Index() as ViewResult;
                var model = result.Model as MarketingViewModel;

                Assert.Contains("AccountOverview/Partial/_GeneratedQrCode.cshtml", result.ViewName);
                Assert.Empty(model.QrCodeList);
            }
        }

        [Theory]
        [InlineData("test")]
        [InlineData("CasinoBonus")]
        public void GetSubCategory_WhenCall_ReturnCorrectNumberOfSubCategory(string category)
        {
            var subCate = Constant.CategoryList.Where(c => c.Category.MatchWith(category) && !string.IsNullOrEmpty(c.SubCategory));
            var controller = CreateMarketingController();
            var result = controller.GetSubCategory(category) as JsonResult;
            var model = result.Data as IEnumerable<SelectListItem>;
            Assert.Equal(subCate.Count(), model.Count());
            Assert.Equal(JsonRequestBehavior.AllowGet, result.JsonRequestBehavior);
        }

        [Fact]
        public void GetSize_WhenCall_ReturnCorrectJson()
        {
            var controller = CreateMarketingController();
            MarketingService.GetSizeList(Arg.Compat.Any<SizeParam>(), Arg.Compat.Any<bool>())
                .Returns(SizeTypes);
            var param = new SizeParam { Language = Constant.DefaultLanguage };

            var result = controller.GetSize(param) as JsonResult;
            var model = result.Data as IEnumerable<SizeType>;

            Assert.Collection(model, t => Assert.Contains(SizeTypes, s => s.SizeId == t.SizeId), t => Assert.Contains(SizeTypes, s => s.SizeId == t.SizeId));
            Assert.Equal(JsonRequestBehavior.AllowGet, result.JsonRequestBehavior);
        }

        [Fact]
        public void GetBanner_WhenCallWithNoSelectedDomain_ReturnEmptyModelItem()
        {
            var controller = CreateMarketingController();
            MarketingService.GetDomainList(Arg.Compat.Any<int>(), Arg.Compat.Any<bool>()).Returns(new List<Domain>());
            var param = new MarketingBannerParam();

            var result = controller.GetBanner(param) as ViewResult;
            var model = result.Model as IEnumerable<MarketingItem>;

            Assert.Contains("AccountOverview/Partial/_BannerList.cshtml", result.ViewName);
            Assert.Empty(model);
        }

        [Fact]
        public void GetBanner_WhenCall_ReturnCorrectModel()
        {
            var controller = CreateMarketingController();
            var bannerItems = MarketingItems;
            MarketingService.GetBannerList(Arg.Compat.Any<MarketingBannerParam>()).Returns(bannerItems);
            var domainId = Constant.DomainList.First().DomainId;
            var param = new MarketingBannerParam { SelectedDomainId = domainId };

            var result = controller.GetBanner(param) as ViewResult;
            var model = result.Model as IEnumerable<MarketingItem>;

            Assert.Contains("AccountOverview/Partial/_BannerList.cshtml", result.ViewName);
            Assert.NotEmpty(model);
        }

        [Fact]
        public void GetText_WhenCallWithNoSelectedDomain_ReturnEmptyModelList()
        {
            var controller = CreateMarketingController();
            MarketingService.GetDomainList(Arg.Compat.Any<int>(), Arg.Compat.Any<bool>()).Returns(new List<Domain>());
            var param = new MarketingTextParam();

            var result = controller.GetText(param) as ViewResult;
            var model = result.Model as IEnumerable<MarketingItem>;

            Assert.Equal("~/Views/UserPanel/AccountOverview/Partial/_TextList.cshtml", result.ViewName);
            Assert.Empty(model);
        }

        [Fact]
        public void GetText_WhenCallWithSelectedDomain_ReturnNotEmptyList()
        {
            var controller = CreateMarketingController();
            var items = MarketingItems;
            MarketingService.GetTextList(Arg.Compat.Any<MarketingTextParam>()).Returns(items);
            var domainId = Constant.DomainList.First().DomainId;
            var param = new MarketingTextParam { SelectedDomainId = domainId };

            var result = controller.GetText(param) as ViewResult;
            var model = result.Model as IEnumerable<MarketingItem>;

            Assert.Equal("~/Views/UserPanel/AccountOverview/Partial/_TextList.cshtml", result.ViewName);
            Assert.NotEmpty(model);
        }

        [Theory]
        [InlineData(default(string))]
        [InlineData("en-US")]
        public void GenerateLink_WhenCall_ReturnCorrectView(string selectedLanguage)
        {
            using (var image = Image)
            {
                QrCodeService.GetImage(Arg.Compat.Any<string>()).Returns(image);
                QrCodeService.GenerateBase64QrCode(Arg.Compat.Any<string>(), Arg.Compat.Any<Image>(), Arg.Compat.Any<bool>())
                    .Returns(BASE64_IMAGE);

                var controller = CreateMarketingController();
                controller = NSubtituteMockingHelper.FakeController(controller) as MarketingController;
                controller.HttpContext.Server.MapPath(Arg.Compat.Any<string>()).Returns(String.Empty);
                var result = controller.GenerateLink(selectedLanguage, default(string), default(int)) as ViewResult;

                Assert.Equal("~/Views/UserPanel/AccountOverview/Partial/_TextLink.cshtml", result.ViewName);
            }
        }

        private MarketingController CreateMarketingController()
            => new MarketingController(SessionService, AppSetting, LanguageService, MarketingService, QrCodeService);
    }
}
