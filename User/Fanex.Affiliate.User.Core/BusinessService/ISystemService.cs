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

        IEnumerable<Language> GetLanguageList(string targetCountryCode = "");

        IEnumerable<Language> GetSupportedLanguages(string targetCountryCode = "");

        IEnumerable<TargetCountry> GetSystemTargetCountries();

        bool IsValid(string langCode);

        string GetFavoriteShortedLanguage();

        IEnumerable<Currency> GetBaseCurrencyList();
    }
}
