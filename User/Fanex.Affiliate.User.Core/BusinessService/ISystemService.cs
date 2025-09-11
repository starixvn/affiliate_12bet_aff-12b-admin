namespace Fanex.Affiliate.User.Core.BusinessService
{
    using System.Collections.Generic;
    using DTO;
    using DTO.Systems;
    using DTO.UserPanel;

    public interface ISystemService
    {
        void SetCulture(string languageCode);

        string GetFavoriteLanguage();

        string GetCountry();

        IEnumerable<Language> GetLanguageList();

        IEnumerable<Language> GetSupportedLanguages();

        IEnumerable<Language> GetLanguageByTargetCountry(string targetCountry = "");

        IEnumerable<TargetCountryLanguage> GetSystemTargetCountries();

        bool IsValid(string langCode);

        string GetFavoriteShortedLanguage();

        IEnumerable<Currency> GetBaseCurrencyList();

        Dictionary<string, List<TargetCountryLanguage>> GetTargetCountryLanguages();
    }
}
