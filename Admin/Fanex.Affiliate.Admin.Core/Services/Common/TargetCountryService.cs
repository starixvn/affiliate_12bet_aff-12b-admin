using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.Services.Common
{
    public class TargetCountryService : ITargetCountryService
    {
        private const byte USING = 2;

        private readonly ITargetCountryRepository _targetCountryRepository;

        public TargetCountryService(ITargetCountryRepository targetCountryRepository)
        {
            _targetCountryRepository = targetCountryRepository;
        }

        public IEnumerable<TargetCountry> GetVisibleTargetCountriesByUser(int userId)
        {
            return _targetCountryRepository.GetVisibleTargetCountriesByUser(userId).ExcludeEmpty();
        }

        public IEnumerable<TargetCountry> GetVisibleTargetCountriesNotUsingByUser(int userId)
        {
            return GetVisibleTargetCountriesByUser(userId)?.Where(m => m.Status != USING);
        }
    }
}
