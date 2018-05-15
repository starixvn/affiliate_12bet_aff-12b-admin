using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.Constants
{
    public static class SystemLanguage
    {
        public const string DefaultLanguageCode = "en-US";

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

        public static IDictionary<string, string> LanguageNameByCode => _languageNameByCode;

        public static IDictionary<string, string> ShortedLanguageCode => _shortedLanguageCode;

        public const string ShortedDefaultCode = "en";

        public const string ShortedEuroCode = "eneuro";
    }
}