using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;

namespace Fanex.Affiliate.User.Core.Constants
{
    public static class SystemLanguage
    {
        public const string DefaultLanguageCode = "en-US";

        public const string ShortedDefaultCode = "en";

        public const string ShortedEuroCode = "eneuro";

        private static IEnumerable<Language> _languageList = new List<Language>
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
            new Language { Code = "eneuro", NativeName = "English (Europe)", DisplayName = "English (Europe)", EnglishName = "English (Europe)", ShortedCode = "eneuro"}
        };

        private static IDictionary<string, string> _languageNameByCode = new Dictionary<string, string>
        {
            ["en-US"] = "English",
            ["zh-CN"] = "中文简体",
            ["zh-TW"] = "中文繁体",
            ["id-ID"] = "Indonesia",
            ["vi-VN"] = "Tiếng Việt",
            ["th-TH"] = "ภาษาไทย",
            ["ko-KR"] = "한국어",
            ["es-ES"] = "Español",
            ["ja-JP"] = "日本",
            ["hi-IN"] = "Republic of India"
        };

        private static IDictionary<string, string> _shortedLanguageCode = new Dictionary<string, string>
        {
            ["en-US"] = "en",
            ["zh-CN"] = "cs",
            ["zh-TW"] = "ch",
            ["id-ID"] = "id",
            ["vi-VN"] = "vn",
            ["th-TH"] = "th",
            ["ko-KR"] = "ko",
            ["es-ES"] = "es",
            ["ja-JP"] = "jp",
            ["hi-IN"] = "hien"
        };

        private static IDictionary<string, string> _languageCodeByName = new Dictionary<string, string>
        {
            ["english"] = "en-US",
            ["chinese simplified"] = "zh-CN",
            ["chinese traditional"] = "zh-TW",
            ["indonesian"] = "id-ID",
            ["vietnamese"] = "vi-VN",
            ["thai"] = "th-TH",
            ["korean"] = "ko-KR",
            ["spanish"] = "es-ES",
            ["japanese"] = "ja-JP",
            ["republic of india"] = "hi-IN",
            ["english (europe)"] = "eneuro"
        };

        public static IDictionary<string, string> LanguageNameByCode => _languageNameByCode;

        public static IDictionary<string, string> ShortedLanguageCode => _shortedLanguageCode;

        public static IDictionary<string, string> LanguageCodeByName => _languageCodeByName;

        public static IEnumerable<Language> LanguageList => _languageList;
    }
}