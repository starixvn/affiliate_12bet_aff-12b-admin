namespace Fanex.Affiliate.Admin.Core.Utilities.Market
{
    using DTO;
    using DTO.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class MarketHelper
    {
        public static bool IsValidMarketIds(string marketIds, IEnumerable<Market> visibleMarkets)
        {
            if (string.IsNullOrWhiteSpace(marketIds))
            {
                return true;
            }

            var selectedMarketIds = marketIds.Split(',').ToList();

            return selectedMarketIds.TrueForAll(m => visibleMarkets.Any(v => v.MarketId.Equals(m, StringComparison.OrdinalIgnoreCase)));
        }

        public static IEnumerable<DomainInfo> HandleDomainInfos(IDictionary<string, string> marketDics, IEnumerable<DomainInfo> domains)
        {
            foreach (var domain in domains)
            {
                domain.BlockMarketName = GetBlockMarketName(marketDics, domain.BlockMarket);
            }

            return domains;
        }

        public static string GetBlockMarketName(IDictionary<string, string> marketDics, string blockMarket)
        {
            if (string.IsNullOrWhiteSpace(blockMarket))
            {
                return string.Empty;
            }

            return string.Join(",", blockMarket.Split(',')
                .Where(marketDics.ContainsKey)
                .Select(id => marketDics[id]));
        }

    }
}
