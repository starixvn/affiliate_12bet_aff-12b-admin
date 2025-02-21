using Fanex.Affiliate.Admin.Core.DTO;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Common
{
    public interface ISystemService
    {
        void SetCulture(string languageCode);

        IEnumerable<Language> GetLanguageList();

        IEnumerable<Product> GetProductList();

        IEnumerable<Currency> GetBaseCurrencies();

        IEnumerable<Currency> GetCurrencies();

        IEnumerable<Currency> GetPaymentCurrencies();
    }
}
