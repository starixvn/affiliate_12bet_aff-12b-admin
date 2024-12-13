using Fanex.Affiliate.Admin.Core.Criterias;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Data.Repository;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Affiliate
{
    public class AffTypeService : IAffTypeService
    {
        private readonly IDynamicRepository _dynamicRepository;

        public AffTypeService(
            IDynamicRepository dynamicRepository)
        {
            _dynamicRepository = dynamicRepository;
        }

        public IEnumerable<AffTypeInfo> GetAffTypeList()
        {
            const string SpKey = "Aff_AffTypeSel";

            return _dynamicRepository.Fetch<AffTypeInfo>(new BaseCriteria(SpKey));
        }
    }
}
