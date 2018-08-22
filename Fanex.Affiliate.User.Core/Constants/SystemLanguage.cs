using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.Resources;

namespace Fanex.Affiliate.User.Core.Constants
{
    public static class SystemLanguage
    {
        public const string DefaultLanguageCode = "en-US";

        public const string VietNamLanguageCode = "vi-VN";

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

        public static string GetLanguageText(string value)
        {
            return value.Equals("english (europe)", System.StringComparison.OrdinalIgnoreCase) ? Lang.EnglishEU
                  : Lang.ResourceManager.GetString(value.Replace(" ", "")) ?? value;

        }
    }
}