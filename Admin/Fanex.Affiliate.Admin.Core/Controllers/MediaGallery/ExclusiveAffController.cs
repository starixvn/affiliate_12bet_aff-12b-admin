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
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.MediaGallery
{
    [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu)]
    public class ExclusiveAffController : ReadOnlySessionBaseController
    {
        private readonly ILanguageService _languageService;
        private readonly IExclusiveAffService _exclusiveAffService;
        private readonly IAppSetting _appSetting;

        public ExclusiveAffController(
            IExclusiveAffService exclusiveAffService,
            ISessionService sessionService,
            ILanguageService languageService,
            IAppSetting appSetting) : base(sessionService)
        {
            _languageService = languageService;
            _exclusiveAffService = exclusiveAffService;
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
        public ActionResult InsertExclusiveAff(InsertExclusiveAffParam model)
        {
            if (ModelState.IsValid)
            {
                var param = new InsertExclusiveAffParam
                {
                    UserId = SessionService.UserId,
                    Ip = SessionService.RequestIp
                };

                return new JsonNetResult
                {
                    Data = _exclusiveAffService.InsertExclusiveAff(param)
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
        public ActionResult GetExclusiveAffList(SelectExclusiveAffParam param)
        {
            var data = _exclusiveAffService.GetExclusiveAffs(param);

            return new JsonNetResult
            {
                Data = new EntityResult<ExclusiveAff>
                {
                    MainData = data.MainData,
                    TotalItem = data.TotalItem
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu, IsHost = true)]
        public ActionResult DeleteExclusiveAffs(string exclusiveAffIds)
        {
            var param = new DeleteExclusiveAffParam
            {
                ExclusiveAffIds = exclusiveAffIds,
                UserId = SessionService.UserId,
                Ip = SessionService.RequestIp,

            };

            return new JsonNetResult
            {
                Data = _exclusiveAffService.DeleteExclusiveAff(param)
            };
        }
    }
}
