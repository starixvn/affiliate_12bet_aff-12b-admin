using Fanex.Affiliate.Admin.Core.DTO.Common;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Common
{
    public interface ICommonSettingService
    {
        void SetCulture(string languageCode);

        IEnumerable<Language> GetInChangeMarkets();

        IEnumerable<Product> GetProductList();

        IEnumerable<AffType> GetAffTypeList();

        IEnumerable<Country> GetCountryList();
    }
}
