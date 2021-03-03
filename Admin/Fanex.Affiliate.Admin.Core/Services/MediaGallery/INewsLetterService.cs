using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public interface INewsLetterService
    {
        ResultInfo AddNewsLetter(HttpPostedFileBase file, InsertNewsLetterParam param);

        ResultInfo UpdateNewsLetter(HttpPostedFileBase file, UpdateNewsLetterParam param);

        EntityResult<NewsLetter> GetNewsLetters(SelectNewsLetterParam param);

        NewsLetter GetNewsLetterById(int id);

        ResultInfo DeleteNewsLetters(DeleteNewsLetterParam param);

    }
}
