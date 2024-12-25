using Fanex.Affiliate.Admin.Core.Criterias;
using Fanex.Affiliate.Admin.Core.Criterias.MediaGallery;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Data.Repository;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public class CommonMediaGalleryService : BaseDynamicService, ICommonMediaGalleryService
    {
        public CommonMediaGalleryService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

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
            const string SpKey = "Aff_Admin_MediaCurrency_Sel";

            return FetchTData<MediaGalleryFilterItem>(new BaseCriteria(SpKey));
        }
    }
}
