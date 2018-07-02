using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;

namespace Fanex.Affiliate.User.Core.Constants
{
    public static class SystemLanguage
    {
        public const string DefaultLanguageCode = "en-US";

        public const string ShortedDefaultCode = "en";

        public const string ShortedEuroCode = "eneuro";

        public static IEnumerable<Language> LanguageList => new List<Language>
        {
            new Language { Code = "en-US", NativeName = "English", DisplayName = "English", EnglishName = "English", ShortedCode = "en"},
            new Language { Code = "zh-CN", NativeName = "中文简体", DisplayName = "Chinese Simplified", EnglishName = "Chinese Simplified", ShortedCode = "cs"},
            new Language { Code = "zh-TW", NativeName = "中文繁体", DisplayName = "Chinese Traditional", EnglishName = "Chinese Traditional", ShortedCode = "ch"},
            new Language { Code = "id-ID", NativeName = "Indonesia", DisplayName = "Indonesian", EnglishName = "Indonesian", ShortedCode = "id"},
            new Language { Code = "vi-VN", NativeName = "Tiếng Việt", DisplayName = "Vietnamese", EnglishName = "Vietnamese", ShortedCode = "vn"},
            new Language { Code = "th-TH", NativeName = "ภาษาไทย", DisplayName = "Thai", EnglishName = "Thai", ShortedCode = "th"},
            new Language { Code = "ko-KR", NativeName = "한국어", DisplayName = "Korean", EnglishName = "Korean", ShortedCode = "ko"},
            new Language { Code = "es-ES", NativeName = "Español", DisplayName = "Spanish", EnglishName = "Spanish", ShortedCode = "es"},
            new Language { Code = "ja-JP", NativeName = "日本", DisplayName = "Japanese", EnglishName = "Japanese", ShortedCode = "jp"},
            new Language { Code = "hi-IN", NativeName = "Republic of India", DisplayName = "Republic of India", EnglishName = "Republic of India", ShortedCode = "hien"},
            new Language { Code = "en-GB", NativeName = "English (Europe)", DisplayName = "English (Europe)", EnglishName = "English (Europe)", ShortedCode = "eneuro"}
        };

        public static string GetDisplayText(string value)
        {
            switch (value.ToLower())
            {
                case "bonus": return Resources.Lang.Bonus;
                case "tennis": return Resources.Lang.Tennis;
                case "football": return Resources.Lang.Football;
                case "poker": return Resources.Lang.Poker;
                case "image": return Resources.Lang.Image;
                case "flash": return Resources.Lang.Flash;
                case "english": return Resources.Lang.English;
                case "chinese traditional": return Resources.Lang.ChineseTraditio;
                case "chinese simplified": return Resources.Lang.ChineseSimplifi;
                case "thai": return Resources.Lang.Thai;
                case "spanish": return Resources.Lang.Spanish;
                case "indonesian": return Resources.Lang.Indonesian;
                case "vietnamese": return Resources.Lang.Vietnamese;
                case "english (europe)": return Resources.Lang.EnglishEU;
                case "korean": return Resources.Lang.Korean;
                case "japanese": return Resources.Lang.Japanese;
                case "casino": return Resources.Lang.Casino;
                case "basketball": return Resources.Lang.Basketball;
                case "tennins": return Resources.Lang.Tennis;
                case "rmb": return Resources.Lang.RMB;
                case "myr": return Resources.Lang.MYR;
                case "thb": return Resources.Lang.THB;
                case "usd": return Resources.Lang.USD;
                case "eur": return Resources.Lang.EUR;
                case "gbp": return Resources.Lang.GBP;
                case "aud": return Resources.Lang.AUD;
                case "chf": return Resources.Lang.CHF;
                case "number game": return Resources.Lang.NumberGame;
                case "sportsbook": return Resources.Lang.Sportbook;
                case "racebook": return Resources.Lang.Racebook;
                case "sb bonus": return Resources.Lang.SBBonus;
                case "soccer": return Resources.Lang.Soccer;
                case "casino bonus": return Resources.Lang.CasinoBonus;
                case "baccarat": return Resources.Lang.Baccarat;
                case "blackjack": return Resources.Lang.Blackjack;
                case "slot": return Resources.Lang.Slot;
                case "roulette": return Resources.Lang.Roulette;
                case "sic bo": return Resources.Lang.Sicbo;

                default:
                    return value;
            }
        }
    }
}