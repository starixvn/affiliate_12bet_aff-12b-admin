using Fanex.Affiliate.Admin.Core.DTO.Common;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Common
{
    public interface ISystemService
    {
        void SetCulture(string languageCode);

        IEnumerable<Language> GetInChangeMarkets();

        IEnumerable<Language> GetAllInChangeMarkets();

        IEnumerable<Product> GetProductList();

        IEnumerable<AffType> GetAffTypeList();

        IEnumerable<Country> GetCountryList();

        IEnumerable<TargetCountry> GetTargetCountryListByUser();

        IEnumerable<Language> GetTargetLanguageListByUser();

        IEnumerable<Language> GetAllTargetLanguageListByUser();
    }
}
