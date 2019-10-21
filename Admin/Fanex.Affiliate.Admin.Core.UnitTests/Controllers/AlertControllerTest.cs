using Fanex.Affiliate.Admin.Core.Controllers;
using Fanex.Affiliate.Admin.Core.MetaModels;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers
{
    public class AlertControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var controller = new AlertController();

            // Action
            var viewResult = controller.Index(new AlertParam()) as ViewResult;
            var viewName = viewResult.ViewName;

            // Assert
            Assert.Equal("", viewName);
        }
    }
}
