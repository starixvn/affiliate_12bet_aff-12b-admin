using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.Utilities;
using System.Web;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Utilities
{
    public class ClaimUtitlityTest
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void HasClaimOnlyInternalLogin_WhenCall_ReturnCorrect(bool isNull)
        {
            MockingHelper.SetUpEnviroment("test", false);
            if (isNull)
            {
                HttpContext.Current.User = null;
            }

            var hasClaim = ClaimUtitlity.HasClaim("test");

            Assert.False(hasClaim);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void HasWriteClaim_WhenCall_ReturnFalse(bool isNull)
        {
            MockingHelper.SetUpEnviroment("test", true);
            if (isNull)
            {
                HttpContext.Current.User = null;
            }

            var hasClaim = ClaimUtitlity.HasWriteClaim("test");

            Assert.False(hasClaim);
        }
    }
}
