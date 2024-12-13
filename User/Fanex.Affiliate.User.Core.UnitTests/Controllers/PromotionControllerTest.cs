using Fanex.Affiliate.User.Core.Controllers;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.ViewModels;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers
{
    public class PromotionControllerTest
    {
        private readonly ISessionService sessionService;

        public PromotionControllerTest()
        {
            sessionService = NSubstituteMockingHelper.FakeSessionService();
        }

        [Fact]
        public void Index_WhenCall_ReturnCorrectView()
        {
            var controller = MockingHelper.FakeController(new PromotionController(sessionService, null)) as PromotionController;
            var result = controller.Index() as ViewResult;
            Assert.NotNull(result.Model as ProPageViewModel);
            Assert.Equal("~/Views/Promotion/Index.cshtml", result.ViewName);
        }
    }
}
