using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.Repository
{
    public interface ILanguageRepository
    {
        IEnumerable<LanguageType> GetLanguageList(bool isInternal);
    }
}
