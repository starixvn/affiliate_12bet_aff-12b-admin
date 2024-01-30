using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Resources;
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
        private readonly ISystemService _systemService;
        private readonly IBannerService _bannerService;
        private readonly IAppSetting _appSetting;

        public MediaGalleryController(
            ISessionService sessionService,
            ISystemService systemService,
            IBannerService bannerService,
            IAppSetting appSetting) : base(sessionService)
        {
            _systemService = systemService;
            _bannerService = bannerService;
            _appSetting = appSetting;
        }

        [AllowByPassFunctionAuthorize]
        public ActionResult Index(BannerParamFromHome param)
        {
            var sizeParam = new SizeParam
            {
                Languages = BuildQueryLanguages()
            };

            var model = new BannerListViewModel
            {
                TargetProductList = _bannerService.GetTargetProductList(),
                BannerRootUrl = _appSetting.BannerRootUrl,
                LanguageList = _systemService.GetAllTargetLanguageListByUser(),
                SizeList = _bannerService.GetSizeList(sizeParam),
                AffTypeList = _systemService.GetAffTypeList(),
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
                TargetProductId = model.TargetProductId,
                Languages = BuildQueryLanguages(model.Language)
            };

            return new JsonNetResult
            {
                Data = _bannerService.GetSizeList(param)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetBannerList(GetBannerViewModel model)
        {
            var param = new BannerParam
            {
                AffType = model.AffType,
                TargetProductId = model.TargetProductId,
                Languages = BuildQueryLanguages(model.Language),
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                Size = model.Size,
                FromDate = model.FromDate,
                ToDate = model.ToDate
            };

            return new JsonNetResult
            {
                Data = _bannerService.GetBannerList(param)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetBannerDetail(int selectedBannerId)
        {
            return new JsonNetResult
            {
                Data = _bannerService.GetBannerDetail(selectedBannerId)
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
                    Data = _bannerService.InsertBanner(files, encodeParam)
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
                    Data = _bannerService.UpdateBanner(files, encodeParam)
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
                    Data = _bannerService.DeleteBanners(param)
                };
            }

            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr("Please select Banner(s) to delete.")
            };
        }

        private string BuildQueryLanguages(string lang = null)
        {
            var langs = _systemService.GetAllTargetLanguageListByUser();
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
                TargetProductIds = Server.HtmlEncode(param.TargetProductIds),
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
