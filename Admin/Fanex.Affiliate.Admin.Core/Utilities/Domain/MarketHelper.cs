using Fanex.Affiliate.Admin.Core.DTO.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Utilities.Domain
{
    public static class MarketHelper
    {
        public static string GetVisibleMarketIds(string marketId, IEnumerable<Market> visibleMarkets)
        {
            var markets = visibleMarkets;
            if (!string.IsNullOrWhiteSpace(marketId))
            {
                markets = visibleMarkets.Where(l => l.MarketId.Equals(marketId, StringComparison.OrdinalIgnoreCase));
            }

            return string.Join(",", markets.Select(m => m.MarketId));
        }

        public static bool IsValidMarketIds(string marketIds, IEnumerable<Market> visibleMarkets)
        {
            if (string.IsNullOrWhiteSpace(marketIds))
            {
                return true;
            }

            var selectedMarketIds = marketIds.Split(',');

            return selectedMarketIds.All(m => visibleMarkets.Any(v => v.MarketId.Equals(m, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
