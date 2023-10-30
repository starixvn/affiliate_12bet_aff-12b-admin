using Fanex.Affiliate.Admin.Core.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Utilities
{
    public static class SystemHelper
    {
        public static string BuildAllowQueryLanguages(string language, IEnumerable<Language> inChangeMarkets)
        {
            var langs = inChangeMarkets;
            if (!string.IsNullOrWhiteSpace(language))
            {
                langs = inChangeMarkets.Where(l => l.LanguageCode.Equals(language, StringComparison.OrdinalIgnoreCase));
            }

            return string.Join(",", langs.Select(m => m.LanguageCode));
        }

        public static string GetVisibleTargetCountryCodes(string targetCountryCode, IEnumerable<TargetCountry> visibleTargetCountries)
        {
            var targetCountries = visibleTargetCountries;
            if (!string.IsNullOrWhiteSpace(targetCountryCode))
            {
                targetCountries = visibleTargetCountries.Where(l => l.TargetCountryCode.Equals(targetCountryCode, StringComparison.OrdinalIgnoreCase));
            }

            return string.Join(",", targetCountries.Select(m => m.TargetCountryCode));
        }

        public static bool IsValidTargetCountryCodes(string targetCountryCodes, IEnumerable<TargetCountry> visibleTargetCountries)
        {
            if (string.IsNullOrWhiteSpace(targetCountryCodes))
            {
                return true;
            }

            var selectedTargetCountryCodes = targetCountryCodes.Split(',');

            return selectedTargetCountryCodes.All(m => visibleTargetCountries.Any(v => v.TargetCountryCode.Equals(m, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
