using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;

namespace Fanex.Affiliate.Admin.Core.Repository.MediaGallery
{
    public interface INewsLetterRepository
    {
        ResultInfo InsertNewsLetter(InsertHtmlFileParam param);

        ResultInfo UpdateNewsLetter(UpdateNewsLetterParam param);

        ResultInfo DeleteNewsLetter(DeleteNewsLetterParam param);

        EntityResult<NewsLetter> GetNewsLetters(PagingParam param);

        NewsLetter GetNewsLetterById(int id);
    }
}
