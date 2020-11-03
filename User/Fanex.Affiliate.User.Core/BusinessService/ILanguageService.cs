using Fanex.Affiliate.User.Core.DTO;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface ILanguageService
    {
        void SetCulture(string languageCode);

        string GetFavoriteLanguage();

        bool IsValid(string langCode, bool isAfterLogin = true);

        IEnumerable<Language> GetSystemLanguages(bool isAfterLogin = true);
    }
}
