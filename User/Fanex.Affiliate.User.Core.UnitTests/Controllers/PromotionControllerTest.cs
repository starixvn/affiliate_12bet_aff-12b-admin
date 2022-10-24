using Fanex.Affiliate.User.Core.Controllers;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers
{
    public class PromotionControllerTest
    {
        private readonly ISessionService sessionService;

        public PromotionControllerTest()
        {
            sessionService = NSubtituteMockingHelper.FakeSessionService();
        }

        [Fact]
        public void Index_WhenCall_ReturnCorrectView()
        {
            var controller = new PromotionController(sessionService, null);
            var result = controller.Index() as ViewResult;
            Assert.Equal(Constant.DefaultShortedLanguageCode, result.Model as string);
            Assert.Equal("~/Views/Promotion/Index.cshtml", result.ViewName);
        }
    }
}
