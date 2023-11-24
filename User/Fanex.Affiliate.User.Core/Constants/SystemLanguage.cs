using Fanex.Affiliate.User.Core.Extensions;
using Fanex.Affiliate.User.Core.Resources;
using System.Collections.Generic;
using System.Threading;

namespace Fanex.Affiliate.User.Core.Constants
{
    public static class SystemLanguage
    {
        private const string _English_LanguageCode = "en-US";
        private const string _Bangladesh_LanguageCode = "bn-BD";
        private const string _India_LanguageCode = "hi-IN";
        private const string _Indonesia_LanguageCode = "id-ID";
        private const string _Japan_LanguageCode = "ja-JP";
        private const string _Cambodia_LanguageCode = "km-KH";
        private const string _Korea_LanguageCode = "ko-KR";
        private const string _Thailand_LanguageCode = "th-TH";
        private const string _VietNam_LanguageCode = "vi-VN";
        private const string _Chinese_LanguageCode = "zh-CN";
        private const string _Taiwan_LanguageCode = "zh-TW";

        private static readonly IDictionary<string, string> _supportedLanguageForGoogleAnalytics = new Dictionary<string, string>
        {
            [_English_LanguageCode] = "en",
            [_Bangladesh_LanguageCode] = "bn",
            [_India_LanguageCode] = "hi",
            [_Indonesia_LanguageCode] = "id",
            [_Japan_LanguageCode] = "jp",
            [_Cambodia_LanguageCode] = "kh",
            [_Korea_LanguageCode] = "ko",
            [_Thailand_LanguageCode] = "th",
            [_VietNam_LanguageCode] = "vn",
            [_Chinese_LanguageCode] = "cs",
            [_Taiwan_LanguageCode] = "ch",
        };

        private static readonly IReadOnlyDictionary<string, string> _supportedLanguageForSEO = new Dictionary<string, string>
        {
            [_English_LanguageCode] = "en",
            [_Chinese_LanguageCode] = "zh",
            [_Taiwan_LanguageCode] = "zh-MY",
            [_Thailand_LanguageCode] = "th",
            [_VietNam_LanguageCode] = "vi",
            [_Indonesia_LanguageCode] = "id",
            [_India_LanguageCode] = "en-IN",
            [_Korea_LanguageCode] = "ko",
        };

        public static IDictionary<string, string> SupportedLanguage => _supportedLanguageForGoogleAnalytics;

        public const string DefaultLanguageCode = _English_LanguageCode;

        public const string VietNamLanguageCode = _VietNam_LanguageCode;

        public const string CambodianLanguageCode = _Cambodia_LanguageCode;

        public const string IndonesiaLanguageCode = _Indonesia_LanguageCode;

        public const string IndiaLanguageCode = _India_LanguageCode;

        public const string ShortedDefaultCode = "en";

        public const string ShortedEuroCode = "eneuro";

        public const string SelectedAllLanguages = "";

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
            return value.MatchWith("english (europe)") ? Lang.EnglishEU
                  : Lang.ResourceManager.GetString(value.Replace(" ", "")) ?? value;
        }

        public static string GetSEOHtmlTagLangAttribute()
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var htmlTagSeoName = _supportedLanguageForSEO.TryGetValue(currentCulture.Name, out string result)
                ? result
                : ShortedDefaultCode;
            return htmlTagSeoName;
        }

        public static string GetCanonicalLangName()
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var canonicalLangName = _supportedLanguageForSEO.TryGetValue(currentCulture.Name, out string _)
                ? currentCulture.TwoLetterISOLanguageName
                : ShortedDefaultCode;
            return canonicalLangName;
        }
    }
}