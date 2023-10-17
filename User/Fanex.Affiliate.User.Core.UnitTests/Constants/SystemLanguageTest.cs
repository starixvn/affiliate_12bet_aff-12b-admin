using Fanex.Affiliate.User.Core.Constants;
using System.Globalization;
using System.Threading;
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

        [Theory]
        [InlineData("Abc", "en")]
        [InlineData("vi-VN", "vi")]
        public void GetSEOHtmlTagLangAttribute_WhenCall_ReturnCorrectData(string currentLanguage, string shortedCode)
        {
            //Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(currentLanguage);

            //Action
            var result = SystemLanguage.GetSEOHtmlTagLangAttribute();

            //Assert
            Assert.Equal(shortedCode, result);
        }

        [Theory]
        [InlineData("Abc", "en")]
        [InlineData("vi-VN", "vi")]
        public void GetCanonicalLangName_WhenCall_ReturnCorrectData(string currentLanguage, string shortedCode)
        {
            //Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(currentLanguage);

            //Action
            var result = SystemLanguage.GetCanonicalLangName();

            //Assert
            Assert.Equal(shortedCode, result);
        }
    }
}
