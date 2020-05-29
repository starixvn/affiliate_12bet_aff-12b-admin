using Fanex.Affiliate.User.Core.DTO;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface ILanguageService
    {
        void SetCulture(string languageCode);

        string GetFavoriteLanguage();

        IEnumerable<Language> GetSystemLanguages(); 
    }
}
