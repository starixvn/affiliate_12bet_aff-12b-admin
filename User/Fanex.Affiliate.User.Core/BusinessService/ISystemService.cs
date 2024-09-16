using Fanex.Affiliate.User.Core.DTO.System;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface ISystemService
    {
        void SetCulture(string languageCode);

        string GetFavoriteLanguage();

        string GetFavoriteShortedLanguage();

        bool IsValid(string langCode, bool isAfterLogin = true);

        IEnumerable<TargetLanguage> GetSystemTargetLanguages();

        IEnumerable<TargetLanguage> GetAllSystemTargetLanguages();

        IEnumerable<TargetCountry> GetSystemTargetCountries();

        IEnumerable<TargetLanguage> GetSystemTargetLanguagesByCountry(string targetCountry);
    }
}
