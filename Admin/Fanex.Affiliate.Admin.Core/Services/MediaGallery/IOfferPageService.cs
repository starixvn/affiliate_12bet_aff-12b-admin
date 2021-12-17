using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using System.Collections.Generic;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public interface IOfferPageService
    {
        ResultInfo InsertOfferPage(HttpPostedFileBase file, InsertOfferPageParam param);

        ResultInfo UpdateOfferPage(HttpPostedFileBase file, UpdateOfferPageParam param);

        ResultInfo DeleteOfferPage(DeleteOfferPageParam param);

        EntityResult<OfferPage> GetOfferPageList(SelectOfferPageParam param);

        IEnumerable<OfferPageBasicInfo> GetValidEndDateOfferPages();

        OfferPage GetOfferPageById(int pageId);
    }
}
