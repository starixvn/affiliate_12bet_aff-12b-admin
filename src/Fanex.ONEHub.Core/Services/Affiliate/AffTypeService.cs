using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Caching;
using Fanex.Data.Repository;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Affiliate
{
    public class AffTypeService : BaseDynamicService, IAffTypeService
    {
        private readonly ICacheService _cacheService;

        public AffTypeService(
            IDynamicRepository dynamicRepository,
            ICacheService cacheService) : base(dynamicRepository)
        {
            _cacheService = cacheService;
        }

        public IEnumerable<AffTypeInfo> GetAffTypeList()
        {
            var affTypeList = _cacheService.Get<IEnumerable<AffTypeInfo>>(CacheKey.AffTypeList);

            if (affTypeList == null)
            {
                const string SpKey = "Aff_AffTypeSel";

                affTypeList = FetchTData<AffTypeInfo>(SpKey);
                _cacheService.Set(CacheKey.AffTypeList, affTypeList);
            }

            return affTypeList;
        }
    }
}
