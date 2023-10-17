using Fanex.Affiliate.Admin.Core.DTO.Common;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Common
{
    public interface ITargetCountryService
    {
        IEnumerable<TargetCountry> GetVisibleTargetCountriesByUser(int userId);

        IEnumerable<TargetCountry> GetVisibleTargetCountriesNotUsingByUser(int userId);
    }
}
