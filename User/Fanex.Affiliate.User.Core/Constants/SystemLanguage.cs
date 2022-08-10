using Fanex.Affiliate.User.Core.Resources;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.Constants
{
    public static class SystemLanguage
    {
        private static IDictionary<string, string> _supportedLanguageForGoogleAnalytics = new Dictionary<string, string>
        {
            ["en-US"] = "en",
            ["zh-CN"] = "cs",
            ["zh-TW"] = "ch",
            ["th-TH"] = "th",
            ["vi-VN"] = "vn",
            ["id-ID"] = "id",
            ["hi-IN"] = "hi",
            //["km-KH"] = "kh",
            ["ko-KR"] = "ko",
            ["ja-JP"] = "jp"
        };
        public static IDictionary<string, string> SupportedLanguage => _supportedLanguageForGoogleAnalytics;

        public const string DefaultLanguageCode = "en-US";

        public const string VietNamLanguageCode = "vi-VN";

        public const string IndonesiaLanguageCode = "id-ID";

        public const string IndiaLanguageCode = "hi-IN";

        public const string ShortedDefaultCode = "en";

        public const string ShortedEuroCode = "eneuro";

        public static string GetShortedCodeForTracking(string currentLanguage)
        {
            if (_supportedLanguageForGoogleAnalytics.TryGetValue(currentLanguage, out string result) && !string.IsNullOrEmpty(result))
            {
                return result.ToUpperInvariant();
            }

            return ShortedDefaultCode.ToUpperInvariant();
        }

        public static string GetLanguageText(string value)
        {
            return value.Equals("english (europe)", System.StringComparison.OrdinalIgnoreCase) ? Lang.EnglishEU
                  : Lang.ResourceManager.GetString(value.Replace(" ", "")) ?? value;
        }
    }
}