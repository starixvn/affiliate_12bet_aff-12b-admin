using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public interface IAffiliateRepository
    {
        AffLeftPanelInfo GetInfo(string username);

        ResultInfo UpdateInfo(LeftPanelUpdateInfoParam param);
    }
}
