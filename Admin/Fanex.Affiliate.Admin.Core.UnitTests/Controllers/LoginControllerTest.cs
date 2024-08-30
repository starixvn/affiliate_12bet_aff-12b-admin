using Fanex.Affiliate.Admin.Core.Controllers;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Moq;
using System.Security.Principal;
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

        [Fact]
        public void Index_WhenCallWithAuthoried_RedirectView()
        {
            // Arrange
            var controller = new LoginController();
            var request = new Mock<HttpRequestBase>();
            request.Setup(p => p.IsAuthenticated).Returns(true);

            var mockUser = new Mock<IPrincipal>();
            mockUser.Setup(m => m.Identity.IsAuthenticated).Returns(true);

            controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
                ctx.HttpContext.Request == request.Object
                && ctx.HttpContext.User == mockUser.Object
            );

            // Action
            var viewResult = controller.Index() as RedirectResult;

            // Assert
            Assert.Equal("~/Portal/Home", viewResult.Url);
        }
    }
}
