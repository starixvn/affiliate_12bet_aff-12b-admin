using Fanex.Affiliate.User.Core.Extensions;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Extensions
{
    public class StringExtensionTest
    {
        [Theory]
        [InlineData("abc", true, "abc")]
        [InlineData("abc123", false, "abc***")]
        [InlineData("", false, "")]
        public void ReplaceAHalfOfString_WhenCall_ReturnCorrect(string value, bool hasPermission, string result)
        {
            var replaceedString = value.ReplaceAHalfOfString(hasPermission);

            Assert.Equal(result, replaceedString);
        }
    }
}
