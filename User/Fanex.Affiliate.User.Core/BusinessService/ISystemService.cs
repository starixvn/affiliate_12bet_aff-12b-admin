using Fanex.Affiliate.User.Core.DTO.System;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface ISystemService
    {
        void SetCulture(string languageCode);

        string GetFavoriteLanguage();

        bool IsValid(string langCode, bool isAfterLogin = true);

        IEnumerable<Language> GetSystemLanguages(bool isAfterLogin = true);

        IEnumerable<TargetLanguage> GetSystemTargetLanguages();

        IEnumerable<TargetCountry> GetSystemTargetCountries();
    }
}
