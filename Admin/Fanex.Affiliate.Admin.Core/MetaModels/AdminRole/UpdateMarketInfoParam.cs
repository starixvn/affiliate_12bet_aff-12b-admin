using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.MetaModels.AdminRole
{
    public class UpdateMarketInfoParam : UserInfoParam
    {
        public IEnumerable<MarketInfoParam> MarketInfoList { get; set; }

        public string MarketInfoXml { get; set; }
    }
}
