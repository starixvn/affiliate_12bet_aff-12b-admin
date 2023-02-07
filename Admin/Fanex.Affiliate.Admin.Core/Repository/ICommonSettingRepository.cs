using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Common;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public interface ICommonSettingRepository
    {
        IEnumerable<Product> GetProductList();

        IEnumerable<AffType> GetAffTypeList();

        IEnumerable<Country> GetCountryList();

        IEnumerable<Language> GetLanguageList();
    }
}
