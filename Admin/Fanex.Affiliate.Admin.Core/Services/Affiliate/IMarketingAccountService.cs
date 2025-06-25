using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Services.Affiliate
{
    public interface IMarketingAccountService
    {
        MarketingAccount GetMarketingAccountInfo(int affId);

        ResultInfo CreateMarketingAccount(CreateMarketingAccountModel model);
    }
}
