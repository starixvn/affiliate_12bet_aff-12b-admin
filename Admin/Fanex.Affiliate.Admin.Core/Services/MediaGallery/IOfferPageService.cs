using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using System.Collections.Generic;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public interface IOfferPageService
    {
        ResultInfo InsertOfferPage(HttpPostedFileBase file, InsertHtmlFileParam param);

        ResultInfo UpdateOfferPage(HttpPostedFileBase file, UpdateOfferPageParam param);

        ResultInfo DeleteOfferPages(DeleteOfferPageParam param);

        EntityResult<OfferPage> GetOfferPageList(PagingParam param);

        IEnumerable<OfferPageBasicInfo> GetValidEndDateOfferPages();

        OfferPage GetOfferPageById(int pageId);
    }
}
