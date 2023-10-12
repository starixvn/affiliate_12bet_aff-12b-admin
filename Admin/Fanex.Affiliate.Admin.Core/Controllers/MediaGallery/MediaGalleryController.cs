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
    [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu)]
    public class MediaGalleryController : ReadOnlySessionBaseController
    {
        private const int BannerListCategory = 1;
        private const string CasinoCategory = "Casino";
        private const string SportsbookCategory = "Sportsbook";
        private readonly ICommonSettingService _commonSettingService;
        private readonly ICommonMediaGalleryService _commonMediaGalleryService;
        private readonly IBannerListService _bannerListService;
        private readonly IAppSetting _appSetting;

        public MediaGalleryController(
            ISessionService sessionService,
            ICommonSettingService commonSettingService,
            ICommonMediaGalleryService commonMediaGalleryService,
            IBannerListService bannerListService,
            IAppSetting appSetting) : base(sessionService)
        {
            _commonSettingService = commonSettingService;
            _commonMediaGalleryService = commonMediaGalleryService;
            _bannerListService = bannerListService;
            _appSetting = appSetting;
        }

        [AllowByPassFunctionAuthorize]
        public ActionResult Index(BannerParamFromHome param)
        {
            var sizeParam = new SizeParam
            {
                Languages = BuildQueryLanguages()
            };
            var bannerListCategory = _commonMediaGalleryService.GetCategoryList(BannerListCategory);

            var model = new BannerListViewModel
            {
                CategoryList = bannerListCategory,
                BannerRootUrl = _appSetting.BannerRootUrl,
                LanguageList = _commonSettingService.GetInChangeMarkets(),
                SizeList = _bannerListService.GetSizeList(sizeParam),
                AffTypeList = _commonSettingService.GetAffTypeList(),
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
                Languages = BuildQueryLanguages(model.Language)
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
                Languages = BuildQueryLanguages(model.Language),
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                Size = model.Size,
                FromDate = model.FromDate,
                ToDate = model.ToDate
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
        [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu, IsHost = true)]
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
                Data = ResultInfo.GenerateErr(Lang.InvalidBannerBrowser)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu, IsHost = true)]
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
                Data = ResultInfo.GenerateErr(!isValidBannerId ? "Banner Id is invalid." : Lang.PleaseEnterFileName)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu, IsHost = true)]
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
                Data = ResultInfo.GenerateErr("Please select Banner(s) to delete.")
            };
        }

        private string BuildQueryLanguages(string lang = null)
        {
            var langs = _commonSettingService.GetInChangeMarkets();
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
                Language = Server.HtmlEncode(param.Language),
                AffTypes = param.AffTypes
            };
        }

        private void GetUserInfoParam(UserInfoParam param)
        {
            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;
        }
    }
}
