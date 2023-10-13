using Fanex.Affiliate.Admin.Core.DTO.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Utilities.Domain
{
    public static class TargetCountryHelper
    {
        public static string GetVisibleTargetCountryIds(string targetCountryId, IEnumerable<TargetCountry> visibleTargetCountries)
        {
            var targetCountries = visibleTargetCountries;
            if (!string.IsNullOrWhiteSpace(targetCountryId))
            {
                targetCountries = visibleTargetCountries.Where(l => l.TargetCountryId.Equals(targetCountryId, StringComparison.OrdinalIgnoreCase));
            }

            return string.Join(",", targetCountries.Select(m => m.TargetCountryId));
        }

        public static bool IsValidTargetCountryIds(string targetCountryIds, IEnumerable<TargetCountry> visibleTargetCountries)
        {
            if (string.IsNullOrWhiteSpace(targetCountryIds))
            {
                return true;
            }

            var selectedTargetCountryIds = targetCountryIds.Split(',');

            return selectedTargetCountryIds.All(m => visibleTargetCountries.Any(v => v.TargetCountryId.Equals(m, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
