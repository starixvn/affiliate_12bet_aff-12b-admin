using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Fanex.Affiliate.Admin.Core.ViewModels.MediaGallery;
using Microsoft.Practices.ObjectBuilder2;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.MediaGallery
{
    [FunctionAuthorize(FunctionName = Function.Media)]
    public class MediaGalleryController : ReadOnlySessionBaseController
    {
        private const int BannerListCategory = 1;
        private const string CasinoCategory = "Casino";
        private const string SportsbookCategory = "Sportsbook";
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

        [AllowByPassFunctionAuthorize]
        public ActionResult Index(BannerParamFromHome param)
        {
            var sizeParam = new SizeParam
            {
                ImageType = param.ImageType,
                Languages = BuildQueryLanguages()
            };
            var bannerListCategory = _commonMediaGalleryService.GetCategoryList(BannerListCategory);

            var model = new BannerListViewModel
            {
                CategoryList = bannerListCategory.Where(c => string.IsNullOrEmpty(c.SubCategoryName)),
                SubCategoryList = bannerListCategory.Where(c => !string.IsNullOrEmpty(c.SubCategoryName)),
                SubCategoryCasinoList = bannerListCategory.Where(c => string.Equals(c.CategoryName, CasinoCategory, StringComparison.CurrentCultureIgnoreCase)
                                        && !string.IsNullOrEmpty(c.SubCategoryName)),
                SubCategorySportsbookList = bannerListCategory.Where(c => string.Equals(c.CategoryName, SportsbookCategory, StringComparison.CurrentCultureIgnoreCase)
                                        && !string.IsNullOrEmpty(c.SubCategoryName)),
                BannerRootUrl = _appSetting.BannerRootUrl,
                LanguageList = _languageService.GetInChangeMarkets(),
                CurrencyList = _commonMediaGalleryService.GetCurrencyList(),
                ImageTypeList = _commonMediaGalleryService.GetImageTypeList(),
                SizeList = _bannerListService.GetSizeList(sizeParam),
                AffTypeList = _affTypeService.GetAffTypeList(),
                Param = param
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetSizeList(ImageSizeViewModel model)
        {
            var param = new SizeParam
            {
                AffType = model.AffType,
                Category = model.Category,
                Currency = model.Currency,
                DeviceType = model.DeviceType,
                ImageType = model.ImageType,
                Languages = BuildQueryLanguages(model.Language),
                SubCategory = model.SubCategory
            };

            return new JsonNetResult
            {
                Data = _bannerListService.GetSizeList(param)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetBannerList(GetBannerViewModel model)
        {
            var param = new BannerParam
            {
                AffType = model.AffType,
                Category = model.Category,
                Currency = model.Currency,
                DeviceType = model.DeviceType,
                ImageType = model.ImageType,
                Languages = BuildQueryLanguages(model.Language),
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                Size = model.Size,
                SubCategory = model.SubCategory
            };

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
        public ActionResult InsertBanner(HttpPostedFileBase[] files, InsertUpdateMultipleBannersParam param)
        {
            if (ModelState.IsValid && files != null)
            {
                var encodeParam = EncodeParam(param);
                GetUserInfoParam(encodeParam);

                return new JsonNetResult
                {
                    Data = _bannerListService.InsertBanner(files, encodeParam)
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
        public ActionResult UpdateBanner(HttpPostedFileBase files, InsertUpdateMultipleBannersParam param)
        {
            var isValidBannerId = param.BannerId >= Validation.MinId;
            if (ModelState.IsValid && isValidBannerId && (!string.IsNullOrEmpty(param.InputFileNames) || files != null))
            {
                var encodeParam = EncodeParam(param);
                GetUserInfoParam(encodeParam);

                return new JsonNetResult
                {
                    Data = _bannerListService.UpdateBanner(files, encodeParam)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = !isValidBannerId ? "Banner Id is invalid." : Lang.PleaseEnterFileName
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
                    Message = "Please select Banner(s) to delete."
                }
            };
        }

        private string BuildQueryLanguages(string lang = null)
        {
            var langs = _languageService.GetInChangeMarkets();
            if (!string.IsNullOrWhiteSpace(lang))
            {
                langs = langs.Where(l => l.LanguageName.Equals(lang, StringComparison.OrdinalIgnoreCase));
            }

            return langs.JoinStrings(",", l => l.LanguageName);
        }

        private InsertUpdateMultipleBannersParam EncodeParam(InsertUpdateMultipleBannersParam param)
        {
            return new InsertUpdateMultipleBannersParam
            {
                BannerId = param.BannerId,
                InputFileNames = param.InputFileNames,
                Categories = Server.HtmlEncode(param.Categories),
                SubCategories = Server.HtmlEncode(param.SubCategories),
                Language = Server.HtmlEncode(param.Language),
                Currencies = Server.HtmlEncode(param.Currencies),
                AffTypes = param.AffTypes,
                DeviceTypes = param.DeviceTypes
            };
        }

        private void GetUserInfoParam(UserInfoParam param)
        {
            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;
        }
    }
}
