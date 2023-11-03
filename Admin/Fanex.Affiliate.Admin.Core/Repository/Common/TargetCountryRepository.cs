using Fanex.Affiliate.Admin.Core.DTO.Common;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Repository.Common
{
    public class TargetCountryRepository : BaseRepository, ITargetCountryRepository
    {
        public IEnumerable<TargetCountry> GetVisibleTargetCountriesByUser(int userId)
        {
            const string SpKey = "Aff_Domain_BlockTargetCountry_Sel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                return objectDb.Query<TargetCountry>(new { userId });
            }
        }
    }
}
