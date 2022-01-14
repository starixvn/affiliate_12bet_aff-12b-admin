using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;

namespace Fanex.Affiliate.Admin.Core.Repository.MediaGallery
{
    public interface IOfferPageRepository
    {
        ResultInfo InsertOfferPage(InsertHtmlFileParam param);

        ResultInfo UpdateOfferPage(UpdateOfferPageParam param);

        ResultInfo DeleteOfferPage(DeleteOfferPageParam param);

        EntityResult<OfferPage> GetOfferPageList(PagingParam param);

        OfferPage GetOfferPageById(int pageId);
    }
}
