using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers
{
    public class ProductControllerTest
    {
        private readonly ISessionService SessionService;
        private readonly ISystemService LanguageService;

        public ProductControllerTest()
        {
            SessionService = NSubstituteMockingHelper.FakeSessionService();
            LanguageService = NSubstituteMockingHelper.FakeLanguageService();
        }

        [Fact]
        public void Index_WhenCall_ReturnCorrectView()
        {
            var controller = new ProductController(SessionService, LanguageService);
            NSubstituteMockingHelper.FakeController(controller);

            var result = controller.Index() as ViewResult;

            Assert.NotNull(result);
            Assert.Contains("/Views/Product/Index.cshtml", result.ViewName);
        }
    }
}
