using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.Controllers.UserPanel;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing;
using NSubstitute;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers.UserPanel
{
    public class AffiliateLinkControllerTest
    {
        private readonly ISessionService _sessionService;
        private readonly IAppSetting _appSetting;
        private readonly ISystemService _languageService;
        private readonly IMarketingService _marketingService;
        private readonly IQrCodeGeneratorService _qrCodeService;

        private const string BASE64_IMAGE = "base64Image";

        private static Image Image => Image.FromStream(new MemoryStream(Convert.FromBase64String(Constant.LogoImageBase64)));

        public AffiliateLinkControllerTest()
        {
            _sessionService = NSubtituteMockingHelper.FakeSessionService();
            _appSetting = Substitute.For<IAppSetting>();
            _languageService = NSubtituteMockingHelper.FakeLanguageService();
            _marketingService = NSubtituteMockingHelper.FakeMarkengtingService();
            _qrCodeService = Substitute.For<IQrCodeGeneratorService>();
        }

        [Fact]
        public void Index_WhenCall_ReturnCorrectView()
        {
            using (var image = Image)
            {
                _qrCodeService.GetImage(Arg.Compat.Any<string>()).Returns(image);
                _qrCodeService.GenerateBase64QrCode(Arg.Compat.Any<string>(), Arg.Compat.Any<Image>(), Arg.Compat.Any<bool>())
                    .Returns(BASE64_IMAGE);

                var controller = CreateAffiliateLinkController();
                var result = controller.Index() as ViewResult;
                var model = result.Model as AffiliateLinkViewModel;

                Assert.Contains("~/Views/UserPanel/AccountOverview/AffiliateLink/Index.cshtml", result.ViewName);
                Assert.Equal(Constant.LinkList.Count(), model.QrCodeList.Count);
            }
        }

        [Fact]
        public void Index_WhenCallWithDomainEmpty_ReturnCorrectView()
        {
            using (var image = Image)
            {
                var marketingService = Substitute.For<IMarketingService>();

                marketingService.GetTargetProductList(Arg.Compat.Any<bool>()).Returns(Constant.TargetProductList);
                marketingService.GenerateLinkList(Arg.Compat.Any<LinkGenerationParam>()).Returns(Constant.LinkList);

                _qrCodeService.GetImage(Arg.Compat.Any<string>()).Returns(image);
                _qrCodeService.GenerateBase64QrCode(Arg.Compat.Any<string>(), Arg.Compat.Any<Image>(), Arg.Compat.Any<bool>())
                    .Returns(BASE64_IMAGE);

                var controller = CreateAffiliateLinkController();
                var result = controller.Index() as ViewResult;
                var model = result.Model as AffiliateLinkViewModel;

                Assert.Contains("~/Views/UserPanel/AccountOverview/AffiliateLink/Index.cshtml", result.ViewName);
                Assert.Empty(model.QrCodeList);
            }
        }

        [Theory]
        [InlineData(default(string), default(string), 123)]
        [InlineData("EN", "en-US", 123)]
        [InlineData("", "", null)]
        public void GenerateLink_WhenCall_ReturnCorrectView(string selectedCountry, string selectedLanguage, int? selectedDomainId)
        {
            using (var image = Image)
            {
                _qrCodeService.GetImage(Arg.Compat.Any<string>()).Returns(image);
                _qrCodeService.GenerateBase64QrCode(Arg.Compat.Any<string>(), Arg.Compat.Any<Image>(), Arg.Compat.Any<bool>())
                    .Returns(BASE64_IMAGE);

                var controller = CreateAffiliateLinkController();
                var result = controller.GenerateLink(selectedCountry, selectedLanguage, DefaultValue.String, selectedDomainId) as ViewResult;

                Assert.Equal("~/Views/UserPanel/AccountOverview/Partial/_QrCodeLinkList.cshtml", result.ViewName);
            }
        }

        private AffiliateLinkController CreateAffiliateLinkController()
        {
            var controller = new AffiliateLinkController(_sessionService, _appSetting, _languageService, _marketingService, _qrCodeService);
            controller = NSubtituteMockingHelper.FakeController(controller) as AffiliateLinkController;
            controller.HttpContext.Server.MapPath(Arg.Compat.Any<string>()).Returns(String.Empty);

            return controller;
        }
    }
}
