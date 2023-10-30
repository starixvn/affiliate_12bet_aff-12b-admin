using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.Utilities;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Utilities
{
    public class SystemHelperTest
    {

        [Fact]
        public void BuildAllowQueryLanguages_WhenCallWithEmpty_ReturnFullInChargeMarket()
        {
            var expect = "us-en,vi-vn";

            var result = SystemHelper.BuildAllowQueryLanguages(string.Empty, GetInChargeMarkets());

            Assert.Equal(expect, result);
        }

        [Fact]
        public void BuildAllowQueryLanguages_WhenCallWithSelectedLang_ReturnOnlySelectedMarket()
        {
            var lang = "us-en";

            var result = SystemHelper.BuildAllowQueryLanguages(lang, GetInChargeMarkets());

            Assert.Equal(lang, result);
        }

        [Fact]
        public void BuildAllowQueryLanguages_WhenCallWithNoMatchLanguage_ReturnEmptyString()
        {
            var lang = "abc";

            var result = SystemHelper.BuildAllowQueryLanguages(lang, GetInChargeMarkets());

            Assert.Empty(result);
        }

        private static IEnumerable<Language> GetInChargeMarkets()
            => new List<Language>
            {
                new Language{LanguageCode = "us-en"},
                new Language{LanguageCode = "vi-vn"}
            };
    }
}
