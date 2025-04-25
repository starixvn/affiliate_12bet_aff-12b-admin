using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.Constants
{
    public static class AppContact
    {
        private const string Telegram = "Telegram";
        private const string WhatsApp = "WhatsApp";
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
            [SystemLanguage.ThailandLanguageCode] = "Line",
            [SystemLanguage.VietNamLanguageCode] = Telegram,
            [SystemLanguage.ChineseLanguageCode] = Telegram,
            [SystemLanguage.TaiwanLanguageCode] = Telegram,
        };

        public static readonly IReadOnlyDictionary<string, string> SecondAppContactList = new Dictionary<string, string>
        {
            [SystemLanguage.EnglishLanguageCode] = "Facebook",
            [SystemLanguage.BangladeshLanguageCode] = Telegram,
            [SystemLanguage.IndiaLanguageCode] = Telegram,
            [SystemLanguage.JapanLanguageCode] = "Facebook",
            [SystemLanguage.CambodianLanguageCode] = "Facebook",
            [SystemLanguage.ChineseLanguageCode] = "QQ",
            [SystemLanguage.TaiwanLanguageCode] = "QQ",
        };

        public static readonly IReadOnlyDictionary<string, string> ThirdAppContactList = new Dictionary<string, string>
        {
            [SystemLanguage.EnglishLanguageCode] = "Line",
        };

        private static readonly IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> RegexValidations = new Dictionary<string, IReadOnlyDictionary<string, string>>
        {
            [Telegram] = new Dictionary<string, string>
            {
                ["Default"] = "^@\\d{1,15}$",
            },
            ["Line"] = new Dictionary<string, string>
            {
                ["Default"] = "^@\\d{1,15}$",
            },
            [WhatsApp] = new Dictionary<string, string>
            {
                [SystemLanguage.BangladeshLanguageCode] = "^\\+880\\d{1,12}$",
                [SystemLanguage.IndiaLanguageCode] = "^\\+91\\d{1,12}$",
                [SystemLanguage.IndonesiaLanguageCode] = "^\\+62\\d{1,12}$",
                ["Default"] = DefaultRegexValue,
            }
        };

        private static readonly IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> PlaceHolders = new Dictionary<string, IReadOnlyDictionary<string, string>>
        {
            [Telegram] = new Dictionary<string, string>
            {
                ["Default"] = "@xxxxxxxx",
            },
            ["Line"] = new Dictionary<string, string>
            {
                ["Default"] = "@xxxxxxxx",
            },
            [WhatsApp] = new Dictionary<string, string>
            {
                [SystemLanguage.BangladeshLanguageCode] = "+880xxxxxxxx",
                [SystemLanguage.IndiaLanguageCode] = "+91xxxxxxxx",
                [SystemLanguage.IndonesiaLanguageCode] = "+62xxxxxxxx",
                ["Default"] = string.Empty,
            }
        };

        public static string GetRegexByAppAndLanguage(string contactAppName, string language)
        {
            return GetValueDicByAppAndLanguage(RegexValidations, contactAppName, language, DefaultRegexValue);
        }

        public static string GetPlaceHolderByAppAndLanguage(string contactAppName, string language)
        {
            return GetValueDicByAppAndLanguage(PlaceHolders, contactAppName, language);
        }

        private static string GetValueDicByAppAndLanguage(IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> dictionary, string contactAppName, string language, string defaultValue = "")
        {
            if (dictionary.TryGetValue(contactAppName, out IReadOnlyDictionary<string, string>  dictionaryByLanguage))
            {
                if (dictionaryByLanguage.TryGetValue(language, out string value))
                {
                    return value;
                }

                return dictionaryByLanguage["Default"];
            }
            
            return defaultValue;
        }
    }
}
