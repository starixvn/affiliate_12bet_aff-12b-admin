using System.Web.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Moq;
using Xunit;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.DTO.UserPanel;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers.UserPanel
{
    public class AccountOverViewControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnCorrectView()
        {
            // Arrange
            var expectResult = new LastLoginInfo();
            var repository = new Mock<IAccountOverviewRepository>();
            repository.Setup(r => r.GetLastLoginInfo(It.IsAny<int>())).Returns(expectResult);
            var controller = new AccountOverviewController(MockingHelper.GetSessionService(), Mock.Of<ILanguageService>(), repository.Object);

            // Action
            var viewResult = controller.Index() as ViewResult;

            // Assert
            Assert.Equal("~/Views/UserPanel/AccountOverView/Index.cshtml", viewResult.ViewName);
            Assert.Equal(expectResult, viewResult.Model);
        }
    }
}