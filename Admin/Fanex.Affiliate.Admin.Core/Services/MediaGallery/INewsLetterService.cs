using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public interface INewsLetterService
    {
        ResultInfo AddNewsLetter(HttpPostedFileBase file, InsertHtmlFileParam param);

        ResultInfo UpdateNewsLetter(HttpPostedFileBase file, UpdateNewsLetterParam param);

        EntityResult<NewsLetter> GetNewsLetters(PagingParam param);

        NewsLetter GetNewsLetterById(int id);

        ResultInfo DeleteNewsLetters(DeleteNewsLetterParam param);

    }
}
