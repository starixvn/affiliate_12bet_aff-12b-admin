using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Utilities
{
    public static class TargetCountryHelper
    {
        public static string GetVisibleTargetCountryCodes(string targetCountryCode, IEnumerable<TargetCountry> visibleTargetCountries)
        {
            var targetCountries = visibleTargetCountries;
            if (!string.IsNullOrWhiteSpace(targetCountryCode))
            {
                targetCountries = visibleTargetCountries.Where(l => l.TargetCountryCode.MatchWith(targetCountryCode));
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

            return Array.TrueForAll(selectedTargetCountryCodes, s => visibleTargetCountries.Any(v => v.TargetCountryCode.MatchWith(s)));
        }
    }
}
