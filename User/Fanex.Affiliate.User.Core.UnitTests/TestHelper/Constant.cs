using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.Auth;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using System.Collections;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.UnitTests.TestHelper
{
    public static class Constant
    {
        public const string DefaultLanguage = "en-US";

        public const int DefaultAffId = 123;

        public const string AffTypeAsian = "2";

        public const string AffTypeEuro = "4";

        public const string DefaultUserName = "Aff12BetTest";

        public const string DefaultMailContact = "abc@yahoo.com";

        public const string DefaultMailCc = "xyz@yahoo.com";

        public const string DefaultMailUser = "user@yahoo.com";

        public const string DefaultRootDomain = "http://abcxyz.com";

        public const string Captcha = "1234";

        public const string Browser = "Chrome";

        public static IEnumerable<Language> LanguageList = new List<Language>
        {
            new Language {Code = "en", DisplayName="English", EnglishName="English", NativeName="English", ShortedCode="en" },
            new Language {Code ="vn", DisplayName="VietNam", EnglishName= "VietNamese", NativeName="Tieng viet", ShortedCode = "vn" }
        };

        public static IEnumerable<LanguageType> LanguageTypeList = new List<LanguageType>
        {
            new LanguageType {DisplayName = "English", MarketId = "england", MarketName = "England", MarketNativeName = "England", ShortedCode = "en" }
        };

        public static IEnumerable<CategoryType> CategoryList = new List<CategoryType>
        {
            new CategoryType {Category = "test", SubCategory= "testSub1" },
            new CategoryType { Category = "test", SubCategory = "testSub2" },
            new CategoryType { Category = "test1", SubCategory = "testSub3" },
            new CategoryType { Category = "test3" }
        };

        public static IEnumerable<CurrencyType> CurrencyType = new List<CurrencyType>
        {
            new CurrencyType {Currency = "VND", CurrencyId=1 },
            new CurrencyType { CurrencyId = 2, Currency = "USD" }
        };

        public static IEnumerable<Domain> DomainList = new List<Domain>
        {
            new Domain { DomainId = 1, DomainName = "abc.com", IsSsl = true },
            new Domain { DomainId = 2, DomainName = "1245.com", IsSsl = false }
        };

        public static IEnumerable<LandingPage> LandingPageList = new List<LandingPage>
        {
            new LandingPage { PageCode = "SignUp", PageName = "SingUp" },
            new LandingPage { PageCode = "Index", PageName = "Index" }
        };

        public static UserSession Session = new UserSession
        {
            AffId = 1,
            AffType = "asian",
            GroupName = "admin",
            OnUserId = 1,
            PreferredLanguage = "vn",
            UserName = "test"
        };
    }
}
