using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers.UserPanel;
using Fanex.Affiliate.User.Core.CoreService;
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
    public class AffiliateLinkControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnCorrectView()
        {
            var sessionService = Substitute.For<ISessionService>();
            var appSetting = Substitute.For<IAppSetting>();
            var languageService = Substitute.For<ILanguageService>();
            var marketingService = NSubtituteMockingHelper.FakeMarkengtingService();
            var controller = new AffiliateLinkController(sessionService, appSetting, languageService, marketingService);

            sessionService.UserSession.Returns(Constant.Session);
            var result = controller.Index() as ViewResult;

            Assert.Contains("AffiliateLink/Index.cshtml", result.ViewName);
        }
    }
}
