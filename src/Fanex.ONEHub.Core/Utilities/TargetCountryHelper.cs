using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Utilities
{
    public static class TargetCountryHelper
    {
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
