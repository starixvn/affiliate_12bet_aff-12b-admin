using Fanex.Affiliate.Admin.Core.DTO;
using Microsoft.Practices.ObjectBuilder2;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Utilities
{
    public static class LanguageHelper
    {
        public static string BuildAllowQueryLanguages(string language, IEnumerable<Language> inChangeMarkets)
        {
            var langs = inChangeMarkets;
            if (!String.IsNullOrWhiteSpace(language))
            {
                langs = inChangeMarkets.Where(l => l.LanguageCode.Equals(language, StringComparison.OrdinalIgnoreCase));
            }

            return langs.JoinStrings(",", l => l.LanguageCode);
        }
    }
}
