using System.Web.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers.UserPanel;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Moq;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers.UserPanel
{
    public class AccountOverViewControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnCorrectView()
        {
            // Arrange
            var controller = CreateController();

            // Action
            var viewResult = controller.Index() as ViewResult;

            // Assert
            Assert.Equal("~/Views/UserPanel/AccountOverView/Index.cshtml", viewResult.ViewName);
        }

        private static AccountOverviewController CreateController(
            ISessionService sessionService = null,
            ILanguageService languageService = null,
            IAccountOverviewRepository accountOverviewRepository = null)
        {
            sessionService = sessionService ?? MockingHelper.GetSessionService();
            languageService = languageService ?? Mock.Of<ILanguageService>();
            accountOverviewRepository = accountOverviewRepository ?? Mock.Of<IAccountOverviewRepository>();

            return new AccountOverviewController(sessionService, languageService, accountOverviewRepository);
        }
    }
}