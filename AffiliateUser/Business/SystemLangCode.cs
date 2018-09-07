using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AffiliateUser.Business
{
    public static class SystemLangCode
    {
        private static readonly IList<string> LangCodes = new List<string> {
            "en-US",
"zh-CN",
"zh-TW",
"id-ID",
"vi-VN",
"th-TH",
"ko-KR",
"es-ES",
"ja-JP",
"hi-IN"
        };
        public static bool IsValid(string code)
        {
            return !string.IsNullOrEmpty(code) && LangCodes.Contains(code);
        }
    }
}