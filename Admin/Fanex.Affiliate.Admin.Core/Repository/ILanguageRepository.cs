using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public interface ILanguageRepository
    {
        IEnumerable<Language> GetLanguageList();
    }
}
