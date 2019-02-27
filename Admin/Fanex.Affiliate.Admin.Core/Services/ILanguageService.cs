using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public interface ILanguageService
    {
        void SetCulture(string languageCode);

        IEnumerable<Language> GetLanguageList();
    }
}
