using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;

namespace Fanex.Affiliate.DataService.Importer.BigGaming
{
    public class BgLiveCasinoService : BigGamingService
    {
        protected override int ProductId => Products.BgLiveCasino;
        protected override string ProductName => "BG Live Casino";
        protected override string ProductMethod => _appSetting.GetValue("BgLiveCasinoMethod");
        protected override string[] DataFieldNames
            => new[]
            {
                "loginId",  "orderId", "validBet", "payment", "orderTime", "orderstatus",
            };

        public BgLiveCasinoService(IApiClientFactory apiClientFactory = null, IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
            : base(apiClientFactory, appSetting, thirdPartyProductRepository)
        {
        }
    }
}
