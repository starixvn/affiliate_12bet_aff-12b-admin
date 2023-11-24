
using Fanex.Affiliate.Link.DTO;
using System.Collections.Generic;

namespace Fanex.Affiliate.Link.DataAccess.Interface
{
    interface IAffiliateDataAccess
    {
        string GetCommGroupByAffId(int affId);

        string GetLanguageCodeByAffId(int affId);

        IEnumerable<TargetCountry> GetSystemTargetCountries();
    }
}
