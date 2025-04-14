namespace Fanex.Affiliate.User.Core.BusinessService
{
    using DTO;
    using DTO.UserPanel;
    using System.Collections.Generic;

    public interface ISystemService
    {
        void SetCulture(string languageCode);

        string GetFavoriteLanguage();

        IEnumerable<Language> GetLanguageList();

        IEnumerable<Language> GetSupportedLanguages();

        bool IsValid(string langCode);

        string GetFavoriteShortedLanguage();

        IEnumerable<Currency> GetBaseCurrencyList();
    }
}
