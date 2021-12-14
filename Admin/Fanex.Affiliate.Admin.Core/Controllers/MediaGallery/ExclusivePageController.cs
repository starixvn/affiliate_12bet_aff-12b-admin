using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using System.Web;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.MediaGallery
{
    public class ExclusivePageController : ReadOnlySessionBaseController
    {
        private readonly ILanguageService _languageService;
        private readonly IExclusivePageService _exclusivePageService;
        private readonly IAppSetting _appSetting;

        public ExclusivePageController(
            IExclusivePageService exclusivePageService,
            ISessionService sessionService,
            ILanguageService languageService,
            IAppSetting appSetting) : base(sessionService)
        {
            _languageService = languageService;
            _exclusivePageService = exclusivePageService;
            _appSetting = appSetting;
        }

        public ActionResult Index()
        {
            ViewBag.PreviewUrl = $"{_appSetting.AsianAffSiteUrl}/UserPanel/Exclusive/Preview";
            ViewBag.Languages = _languageService.GetInChangeMarkets();
            return View("~/Areas/Portal/Views/MediaGallery/Common.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu, IsHost = true)]
        public ActionResult InsertExclusivePage(HttpPostedFileBase file, InsertExclusivePageParam model)
        {
            if (ModelState.IsValid && file != null)
            {
                var param = new InsertExclusivePageParam
                {
                    UserId = SessionService.UserId,
                    Ip = SessionService.RequestIp
                };

                return new JsonNetResult
                {
                    Data = _exclusivePageService.InsertExclusivePage(file, param)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = Lang.InvalidFileBrowser
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu, IsHost = true)]
        public ActionResult UpdateNewsLetter(HttpPostedFileBase file, UpdateExclusivePageParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _exclusivePageService.UpdateExclusivePage(file, param)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = Lang.InvalidFileBrowser
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetExclusivePageList(SelectExclusivePageParam param)
        {
            var data = _exclusivePageService.GetExclusivePageList(param);

            return new JsonNetResult
            {
                Data = new EntityResult<ExclusivePage>
                {
                    MainData = data.MainData,
                    TotalItem = data.TotalItem
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu, IsHost = true)]
        public ActionResult DeleteNewsLetters(string ids)
        {
            var param = new DeleteExclusivePageParam
            {
                ExclusivePageIds = ids,
                UserId = SessionService.UserId,
                Ip = SessionService.RequestIp,

            };

            return new JsonNetResult
            {
                Data = _exclusivePageService.DeleteExclusivePage(param)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetExclusivePageDetailById(int ExclusivePageId)
        {
            var item = _exclusivePageService.GetExclusivePageById(ExclusivePageId);

            return new JsonNetResult
            {
                Data = item
            };
        }
    }
}
