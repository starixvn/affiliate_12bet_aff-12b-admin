using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public interface ICommonMediaGalleryService
    {
        IEnumerable<Category> GetCategoryList(byte typeId);

        IEnumerable<MediaGalleryFilterItem> GetCurrencyList();

        IEnumerable<MediaGalleryFilterItem> GetImageTypeList();
    }
}
