namespace Fanex.Affiliate.Admin.Core.Constants
{
    using System.Collections.Generic;

    public static class AppContact
    {
        public const string Telegram = "Telegram";
        public const string WhatsApp = "WhatsApp";
        public const string Line = "Line";
        public const string Facebook = "Facebook";
        public const string QQ = "QQ";
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

        private static readonly IReadOnlyDictionary<string, string> RegexValidations = new Dictionary<string, string>
        {
            [Telegram] = "^@.+$",
            ["Line"] = "^@.+$",
            [WhatsApp] = "^\\+\\d{10,}$",
        };

        private static readonly IReadOnlyDictionary<string, string> PlaceHolders = new Dictionary<string, string>
        {
            [Telegram] = "@XXXXXXXXXX",
            ["Line"] = "@XXXXXXXXXX",
            [WhatsApp] = "+XXXXXXXXXX",
        };

        public static string GetRegexByApp(string contactAppName)
        {
            if (string.IsNullOrEmpty(contactAppName))
            {
                return DefaultRegexValue;
            }
            return GetValueDicByAppName(RegexValidations, contactAppName);
        }

        public static string GetPlaceHolderByApp(string contactAppName)
        {
            if (string.IsNullOrEmpty(contactAppName))
            {
                return string.Empty;
            }
            return GetValueDicByAppName(PlaceHolders, contactAppName);
        }

        private static string GetValueDicByAppName(
            IReadOnlyDictionary<string, string> dictionary,
            string contactAppName,
            string defaultValue = "")
        {
            if (dictionary.TryGetValue(contactAppName, out var value))
            {
                return value;
            }

            return defaultValue;
        }
    }
}
