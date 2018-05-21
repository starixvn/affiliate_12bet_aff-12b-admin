namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Constants;
    using DTO.UserPanel;
    using Filters;
    using ViewModels.UserPanel.Marketing;

    public partial class UserPanelController
    {
        [HttpGet]
        [AjaxOnly]
        public ActionResult MediaGallery()
        {
            var bannerTypeList = _marketingService.GetBannerTypeList(SessionService.IsInternal);

            var viewModel = new MediaGalleryViewModel
            {
                CategoryList = GetDataByType(bannerTypeList, BannerTypeName.Category),
                LanguageList = GetDataByType(bannerTypeList, BannerTypeName.Language),
                CurrencyList = GetDataByType(bannerTypeList, BannerTypeName.Currency),
                ImageTypeList = GetDataByType(bannerTypeList, BannerTypeName.ImageType),
                SizeList = GetDataByType(bannerTypeList, BannerTypeName.Size)
            };

            return View(viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult TextBox()
        {
            return View();
        }

        private IEnumerable<SelectListItem> GetDataByType(IEnumerable<BannerType> bannerTypeList, string type)
        {
            return bannerTypeList.Where(p => string.Compare(p.Type, type, true) == 0)
                .Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Value });
        }
    }
}
