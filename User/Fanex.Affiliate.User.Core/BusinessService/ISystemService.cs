namespace Fanex.Affiliate.User.Core.BusinessService
{
    using System.Collections.Generic;
    using DTO;
    using DTO.UserPanel;
    using DTO.Systems;

    public interface ISystemService
    {
        void SetCulture(string languageCode);

        string GetFavoriteLanguage();

        IEnumerable<Language> GetLanguageList();

        IEnumerable<Language> GetSupportedLanguages();

        IEnumerable<Language> GetLanguageByTargetCountry(string targetCountry = "");

        IEnumerable<TargetCountry> GetSystemTargetCountries();

        bool IsValid(string langCode);

        string GetFavoriteShortedLanguage();

        IEnumerable<Currency> GetBaseCurrencyList();
    }
}
