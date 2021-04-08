using Fanex.Affiliate.User.Core.Constants;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Constants
{
    public class SystemLanguageTest
    {
        [Theory]
        [InlineData("Abc", "EN")]
        [InlineData("vi-VN", "VN")]
        public void GetShortedCodeForTracking_WhenCall_ReturnCorrectData(string currentLanguage, string shortedCode)
        {
            //Arrange

            //Action
            var result = SystemLanguage.GetShortedCodeForTracking(currentLanguage);

            //Assert
            Assert.Equal(shortedCode, result);
        }
    }
}
