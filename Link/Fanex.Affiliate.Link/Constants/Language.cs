using System.Collections.Generic;

namespace Fanex.Affiliate.Link.Constants
{
    public static class Language
    {
        private const string ENGLISH_SHORTED_CODE = "en";
        private const string BENGALI_SHORTED_CODE = "bd";
        private const string INDIA_SHORTED_CODE = "te";

        public static readonly IReadOnlyDictionary<string, string> SupportedLanguageCodeOnV1 = new Dictionary<string, string>
        {
            ["en"] = ENGLISH_SHORTED_CODE,
            ["eneuro"] = ENGLISH_SHORTED_CODE,
            ["ch"] = ENGLISH_SHORTED_CODE,
            ["bn"] = BENGALI_SHORTED_CODE,
            ["hien"] = ENGLISH_SHORTED_CODE,
            ["hitg"] = INDIA_SHORTED_CODE,
            ["id"] = "id",
            ["jp"] = ENGLISH_SHORTED_CODE,
            ["kh"] = ENGLISH_SHORTED_CODE,
            ["ko"] = "ko",
            ["th"] = "th",
            ["vn"] = "vn",
            ["cs"] = "cs"
        };
    }
}