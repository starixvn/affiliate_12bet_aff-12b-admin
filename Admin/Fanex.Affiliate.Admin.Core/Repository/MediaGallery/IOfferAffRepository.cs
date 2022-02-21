using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;

namespace Fanex.Affiliate.Admin.Core.Repository.MediaGallery
{
    public interface IOfferAffRepository
    {
        ResultInfo InsertOfferAff(InsertOfferAffParam param);

        ResultInfo DeleteOfferAffs(DeleteOfferAffParam param);

        EntityResult<OfferAff> GetOfferAffList(SelectOfferAffParam param);
    }
}
