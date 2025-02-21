using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;

namespace Fanex.Affiliate.Admin.Core.Services.Affiliate
{
    public interface IAffiliateService
    {
        AffiliateBasicInfo GetAffiliateBasicInfo(int? affId, string affName = null);

        (ResultInfo, string) ApproveAffiliate(ApproveAffiliateParam param);

        EntityResult<AffiliateInfo> GetAffiliateList(AffiliateInfoParam param);

        ResultInfo ToggleFavorite(FavoriteParam param);

        ResultInfo ChangePassword(PasswordParam param);
    }
}
