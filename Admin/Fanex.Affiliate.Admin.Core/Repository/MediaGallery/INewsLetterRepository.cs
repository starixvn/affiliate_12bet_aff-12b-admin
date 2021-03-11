using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.Repository.MediaGallery
{
    public interface INewsLetterRepository
    {
        ResultInfo InsertNewsLetter(InsertNewsLetterParam param);

        ResultInfo UpdateNewsLetter(UpdateNewsLetterParam param);

        ResultInfo DeleteNewsLetter(DeleteNewsLetterParam param);

        EntityResult<NewsLetter> GetNewsLetters(SelectNewsLetterParam param);

        NewsLetter GetNewsLetterById(int id);
    }
}
