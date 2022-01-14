using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public interface IOfferAffService
    {
        ResultInfo InsertOfferAff(InsertOfferAffParam param);

        ResultInfo DeleteOfferAff(DeleteOfferAffParam param);

        EntityResult<OfferAff> GetOfferAffs(SelectOfferAffParam param);
    }
}
