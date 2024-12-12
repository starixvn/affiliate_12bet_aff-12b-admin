using System;
using System.Linq;
using Fanex.Affiliate.User.Core.Constants;

namespace Fanex.Affiliate.User.Core.Extensions
{
    public static class StringExtension
    {
        public static bool MatchWith(this string value, string strToMatch)
        {
            return string.Equals(value, strToMatch, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsSupportedP2pProduct(this string value)
        {
            return !SystemLanguage.UnsupportedP2pProductLanguages.Any(l => string.Equals(l, value, StringComparison.OrdinalIgnoreCase));
        }
    }
}
