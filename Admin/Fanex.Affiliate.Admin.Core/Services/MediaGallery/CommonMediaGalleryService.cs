using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Criterias.MediaGallery;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Caching;
using Fanex.Data.Repository;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public class CommonMediaGalleryService : BaseDynamicService, ICommonMediaGalleryService
    {
        private readonly ICacheService _cacheService;

        public CommonMediaGalleryService(
            IDynamicRepository dynamicRepository,
            ICacheService cacheService) : base(dynamicRepository)
        {
            _cacheService = cacheService;
        }

        public IEnumerable<Category> GetCategoryList(byte typeId, byte userType)
        {
            var criteria = new MediaCategoryCriteria
            {
                TypeId = typeId,
                TargetId = userType,
            };

            return FetchTData<Category>(criteria);
        }

        public IEnumerable<MediaGalleryFilterItem> GetCurrencyList()
        {
            var mediaCurrencyList = _cacheService.Get<IEnumerable<MediaGalleryFilterItem>>(CacheKey.MediaCurrencyList);

            if (mediaCurrencyList == null)
            {
                const string SpKey = "Aff_Admin_MediaCurrency_Sel";

                mediaCurrencyList = FetchTData<MediaGalleryFilterItem>(SpKey);
                _cacheService.Set(CacheKey.MediaCurrencyList, mediaCurrencyList);
            }

            return mediaCurrencyList;
        }
    }
}
