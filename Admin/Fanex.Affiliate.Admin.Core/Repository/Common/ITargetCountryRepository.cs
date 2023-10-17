using Fanex.Affiliate.Admin.Core.DTO.Common;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Repository.Common
{
    public interface ITargetCountryRepository
    {
        IEnumerable<TargetCountry> GetVisibleTargetCountriesByUser(int userId);
    }
}
