using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers;
using Fanex.Affiliate.User.Core.CoreService;
using NSubstitute;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers
{
    public class FaqControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnCorrectView()
        {
            var sessionService = Substitute.For<ISessionService>();
            var languageService = Substitute.For<ILanguageService>();
            var controller = new FaqController(sessionService, languageService);

            var result = controller.Index() as ViewResult;

            Assert.Contains("Views/Faq/Index.cshtml", result.ViewName);
        }
    }
}
