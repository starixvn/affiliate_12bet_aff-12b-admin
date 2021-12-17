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
    public class OfferAffController : ReadOnlySessionBaseController
    {
        private readonly ILanguageService _languageService;
        private readonly IOfferAffService _offerAffService;
        private readonly IAppSetting _appSetting;

        public OfferAffController(
            IOfferAffService offerAffService,
            ISessionService sessionService,
            ILanguageService languageService,
            IAppSetting appSetting) : base(sessionService)
        {
            _languageService = languageService;
            _offerAffService = offerAffService;
            _appSetting = appSetting;
        }

        public ActionResult Index(string offerFile)
        {
            ViewBag.PreviewUrl = $"{_appSetting.AsianAffSiteUrl}/UserPanel/OffersForMe/Preview";
            ViewBag.Languages = _languageService.GetInChangeMarkets();
            return View("~/Areas/Portal/Views/MediaGallery/Common.cshtml", model: offerFile);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu, IsHost = true)]
        public ActionResult InsertOfferAff(InsertOfferAffParam model)
        {
            if (ModelState.IsValid)
            {
                var param = new InsertOfferAffParam
                {
                    UserId = SessionService.UserId,
                    Ip = SessionService.RequestIp
                };

                return new JsonNetResult
                {
                    Data = _offerAffService.InsertOfferAff(param)
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
        public ActionResult GetOfferAffList(SelectOfferAffParam param)
        {
            var data = _offerAffService.GetOfferAffs(param);

            return new JsonNetResult
            {
                Data = new EntityResult<OfferAff>
                {
                    MainData = data.MainData,
                    TotalItem = data.TotalItem
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu, IsHost = true)]
        public ActionResult DeleteOfferAffs(string affIds)
        {
            var param = new DeleteOfferAffParam
            {
                AffIds = affIds,
                UserId = SessionService.UserId,
                Ip = SessionService.RequestIp,

            };

            return new JsonNetResult
            {
                Data = _offerAffService.DeleteOfferAff(param)
            };
        }
    }
}
