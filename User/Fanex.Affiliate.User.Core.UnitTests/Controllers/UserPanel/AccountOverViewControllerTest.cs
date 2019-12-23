using System.Web.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers.UserPanel;
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
            var controller = new AccountOverviewController(MockingHelper.GetSessionService(), Mock.Of<ILanguageService>());

            // Action
            var viewResult = controller.Index() as ViewResult;

            // Assert
            Assert.Equal("~/Views/UserPanel/AccountOverView/Index.cshtml", viewResult.ViewName);
        }
    }
}