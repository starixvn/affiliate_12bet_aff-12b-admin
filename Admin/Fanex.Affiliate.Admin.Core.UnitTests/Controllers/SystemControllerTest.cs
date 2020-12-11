using Fanex.Affiliate.Admin.Core.Controllers;
using Fanex.Affiliate.Admin.Core.Extensions;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers
{
    public class SystemControllerTest
    {
        [Fact]
        public void GetServerNowDate_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var controller = new SystemController();

            // Action
            var result = controller.GetServerNowDate() as JsonNetResult;
            var serverNowDate = result.Data as string;

            // Assert
            Assert.NotEmpty(serverNowDate);
        }
    }
}
