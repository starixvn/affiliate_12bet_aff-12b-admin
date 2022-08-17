using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using System.Collections.Generic;

namespace Fanex.Affiliate.DataService.Importer.BigGaming
{
    public class BgFishService : BigGamingService
    {
        protected override int ProductId => Products.BgFishId;
        protected override string ProductName => "BG Fish";
        protected override string ProductMethod => _appSetting.GetValue("BgFishMethod");

        protected override IDictionary<string, string> DataFieldNames => new Dictionary<string, string>
        {
            { "loginId", "loginId" },
            { "betId", "betId"},
            { "validAmount", "validAmount" },
            { "payout", "payout" },
            { "orderTime", "orderTime" }
        };

        public BgFishService(IApiClientFactory apiClientFactory = null, IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
            : base(apiClientFactory, appSetting, thirdPartyProductRepository)
        {
        }
    }
}
