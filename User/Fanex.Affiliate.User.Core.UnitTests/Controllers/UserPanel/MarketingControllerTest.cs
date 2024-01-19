using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers.UserPanel;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.Utilities;
using NSubstitute;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers.UserPanel
{
    public class MarketingControllerTest
    {
        private readonly ISessionService SessionService;
        private readonly IAppSetting AppSetting;
        private readonly ISystemService LanguageService;
        private readonly IMarketingService MarketingService;

        public MarketingControllerTest()
        {
            SessionService = NSubtituteMockingHelper.FakeSessionService();
            AppSetting = Substitute.For<IAppSetting>();
            LanguageService = NSubtituteMockingHelper.FakeLanguageService();
            MarketingService = NSubtituteMockingHelper.FakeMarkengtingService();
        }

        [Fact]
        public void GetLinkTo_WhenCall_ReturnCorrectData()
        {
            MarketingService.GetSpecialLandingPageList(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>()).Returns(new List<LandingPage>());
            var controller = CreateMarketingController();

            SessionService.UserSession.Returns(Constant.Session);
            var result = controller.GetLinkTo(string.Empty) as JsonResult;

            Assert.NotNull(result.Data);
        }

        private MarketingController CreateMarketingController()
            => new MarketingController(SessionService, LanguageService, MarketingService);
    }
}
