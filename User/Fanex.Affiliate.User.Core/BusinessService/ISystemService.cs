namespace Fanex.Affiliate.User.Core.BusinessService
{
    using DTO;
    using DTO.UserPanel;
    using System.Collections.Generic;

    public interface ISystemService
    {
        void SetCulture(string languageCode);

        string GetFavoriteLanguage();

        IEnumerable<Language> GetLanguageList(bool? isInternal = null);

        IEnumerable<Language> GetSupportedLanguages(bool? isInternal = null);

        bool IsValid(string langCode);

        string GetFavoriteShortedLanguage();

        IEnumerable<Currency> GetBaseCurrencyList();
    }
}
