using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.Constants
{
    public static class AppContact
    {
        private const string Telegram = "Telegram";

        public const string FirstAppContactName = "Skype";

        public static readonly IReadOnlyDictionary<string, string>  SecondAppContactList = new Dictionary<string, string>
        {
            [SystemLanguage.EnglishLanguageCode] = "Facebook",
            [SystemLanguage.BangladeshLanguageCode] = "WhatsApp",
            [SystemLanguage.IndiaLanguageCode] = "WhatsApp",
            [SystemLanguage.IndonesiaLanguageCode] = "WhatsApp",
            [SystemLanguage.JapanLanguageCode] = "Facebook",
            [SystemLanguage.CambodianLanguageCode] = "Facebook",
            [SystemLanguage.KoreaLanguageCode] = Telegram,
            [SystemLanguage.ThailandLanguageCode] = "Line",
            [SystemLanguage.VietNamLanguageCode] = Telegram,
            [SystemLanguage.ChineseLanguageCode] = "QQ",
            [SystemLanguage.TaiwanLanguageCode] = "QQ",
        };

        public static readonly IReadOnlyDictionary<string, string> ThirdAppContactList = new Dictionary<string, string>
        {
            [SystemLanguage.BangladeshLanguageCode] = Telegram,
            [SystemLanguage.IndiaLanguageCode] = Telegram,
            [SystemLanguage.CambodianLanguageCode] = Telegram,
            [SystemLanguage.ChineseLanguageCode] = Telegram
        };
    }
}
