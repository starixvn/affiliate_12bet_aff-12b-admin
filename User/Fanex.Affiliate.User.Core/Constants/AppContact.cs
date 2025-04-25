using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.Constants
{
    public static class AppContact
    {
        public const string Telegram = "Telegram";
        public const string WhatsApp = "WhatsApp";
        public const string Line = "Line";
        public const string Facebook = "Facebook";
        public const string QQ = "QQ";
        private const string Default = "Default";
        private const string DefaultRegexValue = "^.*$";

        public static readonly IReadOnlyDictionary<string, string> FirsAppContactList = new Dictionary<string, string>
        {
            [SystemLanguage.EnglishLanguageCode] = Telegram,
            [SystemLanguage.BangladeshLanguageCode] = WhatsApp,
            [SystemLanguage.IndiaLanguageCode] = WhatsApp,
            [SystemLanguage.IndonesiaLanguageCode] = WhatsApp,
            [SystemLanguage.JapanLanguageCode] = Telegram,
            [SystemLanguage.CambodianLanguageCode] = Telegram,
            [SystemLanguage.KoreaLanguageCode] = Telegram,
            [SystemLanguage.ThailandLanguageCode] = Line,
            [SystemLanguage.VietNamLanguageCode] = Telegram,
            [SystemLanguage.ChineseLanguageCode] = Telegram,
            [SystemLanguage.TaiwanLanguageCode] = Telegram,
        };

        public static readonly IReadOnlyDictionary<string, string> SecondAppContactList = new Dictionary<string, string>
        {
            [SystemLanguage.EnglishLanguageCode] = Facebook,
            [SystemLanguage.BangladeshLanguageCode] = Telegram,
            [SystemLanguage.IndiaLanguageCode] = Telegram,
            [SystemLanguage.JapanLanguageCode] = Facebook,
            [SystemLanguage.CambodianLanguageCode] = Facebook,
            [SystemLanguage.ChineseLanguageCode] = QQ,
            [SystemLanguage.TaiwanLanguageCode] = QQ,
        };

        public static readonly IReadOnlyDictionary<string, string> ThirdAppContactList = new Dictionary<string, string>
        {
        };

        private static readonly IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> RegexValidations = new Dictionary<string, IReadOnlyDictionary<string, string>>
        {
            [Telegram] = new Dictionary<string, string>
            {
                [Default] = "^@\\d{9,15}$"
            },
            ["Line"] = new Dictionary<string, string>
            {
                [Default] = "^@\\d{9,15}$"
            },
            [WhatsApp] = new Dictionary<string, string>
            {
                [SystemLanguage.BangladeshLanguageCode] = "^\\+880\\d{6,12}$",
                [SystemLanguage.IndiaLanguageCode] = "^\\+91\\d{7,13}$",
                [SystemLanguage.IndonesiaLanguageCode] = "^\\+62\\d{7,13}$",
                [Default] = DefaultRegexValue
            }
        };

        private static readonly IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> PlaceHolders = new Dictionary<string, IReadOnlyDictionary<string, string>>
        {
            [Telegram] = new Dictionary<string, string>
            {
                [Default] = "@XXXXXXXXXX"
            },
            ["Line"] = new Dictionary<string, string>
            {
                [Default] = "@XXXXXXXXXX"
            },
            [WhatsApp] = new Dictionary<string, string>
            {
                [SystemLanguage.BangladeshLanguageCode] = "+880XXXXXXXXXX",
                [SystemLanguage.IndiaLanguageCode] = "+91XXXXXXXXXX", 
                [SystemLanguage.IndonesiaLanguageCode] = "+62XXXXXXXXX",
                [Default] = string.Empty
            }
        };

        public static string GetRegexByAppAndLanguage(string contactAppName, string language)
        {
            if (string.IsNullOrEmpty(contactAppName) || string.IsNullOrEmpty(language))
            {
                return DefaultRegexValue;
            }
            return GetValueDicByAppAndLanguage(RegexValidations, contactAppName, language, DefaultRegexValue);
        }

        public static string GetPlaceHolderByAppAndLanguage(string contactAppName, string language)
        {
            if (string.IsNullOrEmpty(contactAppName) || string.IsNullOrEmpty(language))
            {
                return string.Empty;
            }
            return GetValueDicByAppAndLanguage(PlaceHolders, contactAppName, language);
        }

        private static string GetValueDicByAppAndLanguage(
            IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> dictionary,
            string contactAppName,
            string language,
            string defaultValue = "")
        {
            if (!dictionary.TryGetValue(contactAppName, out var dictionaryByLanguage))
            {
                return defaultValue;
            }

            return dictionaryByLanguage.TryGetValue(language, out var value)
                ? value
                : dictionaryByLanguage[Default];
        }
    }
}
