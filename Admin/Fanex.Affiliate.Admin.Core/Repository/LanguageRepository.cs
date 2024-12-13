using Fanex.Affiliate.Admin.Core.Criterias;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Data.Repository;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly IDynamicRepository _dynamicRepository;

        public LanguageRepository(
            IDynamicRepository dynamicRepository)
        {
            _dynamicRepository = dynamicRepository;
        }
        public IEnumerable<Language> GetLanguageList()
        {
            const string SpKey = "Aff_Market_Sel";

            return _dynamicRepository.Fetch<Language>(new BaseCriteria(SpKey));
        }
    }
}
