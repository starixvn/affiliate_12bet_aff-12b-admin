using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;

namespace Fanex.Affiliate.DataService.Importer.BigGaming
{
    public class BgFishService : BigGamingService
    {
        protected override int ProductId => Products.BgFishId;
        protected override string ProductName => "BG Fish";
        protected override string ProductMethod => _appSetting.GetValue("BgFishMethod");
        protected override string[] DataFieldNames
            => new[]
            {
                "loginId",  "betId", "validAmount", "payout", "orderTime",
            };

        public BgFishService(IApiClientFactory apiClientFactory = null, IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
            : base(apiClientFactory, appSetting, thirdPartyProductRepository)
        {
        }
    }
}
