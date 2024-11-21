namespace Fanex.Affiliate.User.Core.BusinessService
{
    using System.Collections.Generic;
    using DTO;
    using Fanex.Affiliate.User.Core.DTO.System;


    public interface ISystemService
    {
        void SetCulture(string languageCode);

        string GetFavoriteLanguage();

        string GetCurrentLanguage();

        IEnumerable<Language> GetLanguageList(bool? isInternal = null);

        bool IsValid(string langCode);

        // Need Removed

        string GetFavoriteShortedLanguage();

        IEnumerable<TargetLanguage> GetSystemTargetLanguages();

        IEnumerable<TargetLanguage> GetAllSystemTargetLanguages();

        IEnumerable<TargetCountry> GetSystemTargetCountries();

        IEnumerable<TargetLanguage> GetSystemTargetLanguagesByCountry(string targetCountry);
    }
}
