using Fanex.Affiliate.Admin.Core.DTO.Common;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Repository.Common
{
    public interface ISystemRepository
    {
        IEnumerable<Product> GetProductList();

        IEnumerable<AffType> GetAffTypeList();

        IEnumerable<Country> GetCountryList();

        IEnumerable<Language> GetLanguageList();

        IEnumerable<TargetCountry> GetTargetCountryList(int userId);

        IEnumerable<Language> GetTargetLanguageList(int userId);
    }
}
