using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.ViewModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Configurations;

namespace Fanex.Affiliate.Admin.Core.Controllers.MediaGallery
{
    [FunctionAuthorize(FunctionName = Function.Media)]
    public class BannerListController : BaseController
    {
        private readonly ILanguageService _languageService;
        private readonly ICommonMediaGalleryService _commonMediaGalleryService;
        private readonly IBannerListService _bannerListService;
        private readonly IAffTypeService _affTypeService;
        private readonly IAppSetting _appSetting;

        public BannerListController(
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

        [HttpGet]
        public ActionResult Index()
        {
            var model = new BannerListViewModel
            {
                CategoryList = _commonMediaGalleryService.GetMediaGalleryFilterItemList("CategoryMedia"),
                LanguageList = _languageService.GetLanguageList(),
                CurrencyList = _commonMediaGalleryService.GetMediaGalleryFilterItemList(Lang.Currency),
                ImageTypeList = _commonMediaGalleryService.GetMediaGalleryFilterItemList("ImageType"),
                SizeList = _commonMediaGalleryService.GetMediaGalleryFilterItemList("Size"),
                AffTypeList = _affTypeService.GetAffTypeList()
            };

            return View("~/Areas/Portal/Views/MediaGallery/BannerList.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetSubCategoryCasinoList()
        {
            return new JsonNetResult
            {
                Data = _commonMediaGalleryService.GetMediaGalleryFilterItemList("CategoryMediaCasino")
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetSubCategorySportsbookList()
        {
            return new JsonNetResult
            {
                Data = _commonMediaGalleryService.GetMediaGalleryFilterItemList("CategoryMediaSportsbook")
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
        public ActionResult GetBannerList(BannerParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _bannerListService.GetBannerList(param)
                };
            }

            return new JsonNetResult
            {
                Data = new EntityResult<BannerInfo>
                {
                    MainData = new List<BannerInfo>(),
                    TotalItem = 0
                }
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
        public ActionResult InsertBanner(InsertUpdateBannerParam param)
        {
            if (ModelState.IsValid && param.File != null)
            {
                var encodeParam = EncodeParam(param);
                GetUserInfoParam(encodeParam);

                return new JsonNetResult
                {
                    Data = _bannerListService.InsertBanner(encodeParam)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "Please input Banner to upload."
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Media, IsHost = true)]
        public ActionResult UpdateBanner(InsertUpdateBannerParam param)
        {
            var isValidBannerId = param.BannerId >= Validation.MinId;
            if (ModelState.IsValid && isValidBannerId && !string.IsNullOrEmpty(param.InputFileName))
            {
                var encodeParam = EncodeParam(param);
                GetUserInfoParam(encodeParam);

                return new JsonNetResult
                {
                    Data = _bannerListService.UpdateBanner(encodeParam)
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
                File = param.File,
                FileName = param.File != null ? Server.HtmlEncode(param.File.FileName) : null,
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
