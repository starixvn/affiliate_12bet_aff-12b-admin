using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public interface IAffiliateRepository
    {
        AffiliateBasicInfo GetAffiliateBasicInfo(int? affId, string affName = null);

        ResultInfo ApproveAffiliate(ApproveAffiliateParam param);
    }
}
