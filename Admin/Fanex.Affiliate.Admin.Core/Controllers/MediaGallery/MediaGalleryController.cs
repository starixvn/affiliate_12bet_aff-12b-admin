using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.ViewModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.Extensions;
using System.Web;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Controllers.MediaGallery
{
    [FunctionAuthorize(FunctionName = Function.Media)]
    public class MediaGalleryController : BaseController
    {
        private const int BannerListCategory = 1;
        private const int CasinoCategoryId = 51;
        private const int SportsbookCategoryId = 1;
        private readonly ILanguageService _languageService;
        private readonly ICommonMediaGalleryService _commonMediaGalleryService;
        private readonly IBannerListService _bannerListService;
        private readonly IAffTypeService _affTypeService;
        private readonly IAppSetting _appSetting;

        public MediaGalleryController(
            ISessionService sessionService,
            ILanguageService languageService,
            ICommonMediaGalleryService commonMediaGalleryService,
            IBannerListService bannerListService,
            IAffTypeService affTypeService,
            IAppSetting appSetting) : base(sessionService)
        {
            _languageService = languageService;
            _commonMediaGalleryService = commonMediaGalleryService;
            _bannerListService = bannerListService;
            _affTypeService = affTypeService;
            _appSetting = appSetting;
        }

        public ActionResult Index(BannerParamFromHome param)
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.BannerList, Url = Url.Content("~/Portal/MediaGallery"), IsActive = true },
                new SubMenu { Name = Lang.TextList, Url = Url.Content("~/Portal/TextList") },
                new SubMenu { Name = Lang.LandingPageList, Url = Url.Content("~/Portal/LandingPageList") }
            };

            var sizeParam = new SizeParam
            {
                ImageType = param.ImageType
            };

            var model = new BannerListViewModel
            {
                CategoryList = _commonMediaGalleryService.GetCategoryList(BannerListCategory).Where(c => c.CategoryId == c.ParentCategoryId),
                LanguageList = _languageService.GetLanguageList(),
                CurrencyList = _commonMediaGalleryService.GetCurrencyList(),
                ImageTypeList = _commonMediaGalleryService.GetImageTypeList(),
                SizeList = _bannerListService.GetSizeList(sizeParam),
                AffTypeList = _affTypeService.GetAffTypeList(),
                SubMenuList = subMenu,
                Param = param
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetSubCategoryCasinoList()
        {
            return new JsonNetResult
            {
                Data = _commonMediaGalleryService.GetCategoryList(BannerListCategory).Where(c => c.ParentCategoryId == CasinoCategoryId && c.CategoryId != c.ParentCategoryId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetSubCategorySportsbookList()
        {
            return new JsonNetResult
            {
                Data = _commonMediaGalleryService.GetCategoryList(BannerListCategory).Where(c => c.ParentCategoryId == SportsbookCategoryId && c.CategoryId != c.ParentCategoryId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetBannerRootUrl()
        {
            return new JsonNetResult
            {
                Data = _appSetting.BannerRootUrl
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetSizeList(SizeParam param)
        {
            return new JsonNetResult
            {
                Data = _bannerListService.GetSizeList(param)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetBannerList(BannerParam param)
        {
            return new JsonNetResult
            {
                Data = _bannerListService.GetBannerList(param)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetBannerDetail(int selectedBannerId)
        {
            return new JsonNetResult
            {
                Data = _bannerListService.GetBannerDetail(selectedBannerId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Media, IsHost = true)]
        public ActionResult InsertBanner(HttpPostedFileBase file, InsertUpdateBannerParam param)
        {
            if (ModelState.IsValid && file != null)
            {
                var encodeParam = EncodeParam(param);
                GetUserInfoParam(encodeParam);

                return new JsonNetResult
                {
                    Data = _bannerListService.InsertBanner(file, encodeParam)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = Lang.InvalidBannerBrowser
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Media, IsHost = true)]
        public ActionResult UpdateBanner(HttpPostedFileBase file, InsertUpdateBannerParam param)
        {
            var isValidBannerId = param.BannerId >= Validation.MinId;
            if (ModelState.IsValid && isValidBannerId && !string.IsNullOrEmpty(param.InputFileName))
            {
                var encodeParam = EncodeParam(param);
                GetUserInfoParam(encodeParam);

                return new JsonNetResult
                {
                    Data = _bannerListService.UpdateBanner(file, encodeParam)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = !isValidBannerId ? "BannerId is invalid." : "You must enter a file name for banner."
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Media, IsHost = true)]
        public ActionResult DeleteBanners(DeleteBannerParam param)
        {
            if (ModelState.IsValid)
            {
                GetUserInfoParam(param);

                return new JsonNetResult
                {
                    Data = _bannerListService.DeleteBanners(param)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "Please select Banners to delete."
                }
            };
        }

        private InsertUpdateBannerParam EncodeParam(InsertUpdateBannerParam param)
        {
            return new InsertUpdateBannerParam
            {
                BannerId = param.BannerId,
                InputFileName = Server.HtmlEncode(param.InputFileName?.Trim()),
                Category = Server.HtmlEncode(param.Category),
                SubCategory = Server.HtmlEncode(param.SubCategory),
                Language = Server.HtmlEncode(param.Language),
                Currency = Server.HtmlEncode(param.Currency),
                Size = Server.HtmlEncode(param.Size),
                ImageType = Server.HtmlEncode(param.ImageType),
                AffType = param.AffType,
                DeviceType = param.DeviceType
            };
        }

        private void GetUserInfoParam(UserInfoParam param)
        {
            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;
        }
    }
}
