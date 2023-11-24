using Fanex.Affiliate.Link.DTO;
using System.Collections.Generic;

namespace Fanex.Affiliate.Link.Services.Interface
{
    public interface IAffiliateService
    {
        string GetCommGroupByAffId(int affId);

        string GetLanguageCodeByAffId(int affId);

        IEnumerable<TargetCountry> GetSystemTargetCountries();
    }
}