using Fanex.Affiliate.Admin.Core.Controllers;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Moq;
using NSubstitute;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers
{
    public class LoginControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var controller = new LoginController();
            var request = MockingHelper.GetHttpRequest();

            controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
                ctx.HttpContext.Request == request
            );

            // Action
            var viewResult = controller.Index() as ViewResult;
            var viewName = viewResult.ViewName;

            // Assert
            Assert.Empty(viewName);
        }
    }
}
