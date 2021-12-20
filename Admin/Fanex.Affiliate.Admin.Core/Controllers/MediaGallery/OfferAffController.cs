using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using System.Linq;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.MediaGallery
{
    [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu)]
    public class OfferAffController : ReadOnlySessionBaseController
    {
        private readonly IOfferPageService _offerPageService;
        private readonly IOfferAffService _offerAffService;
        private readonly IAppSetting _appSetting;

        public OfferAffController(
            IOfferAffService offerAffService,
            ISessionService sessionService,
            IOfferPageService offerPageService,
            IAppSetting appSetting) : base(sessionService)
        {
            _offerPageService = offerPageService;
            _offerAffService = offerAffService;
            _appSetting = appSetting;
        }

        public ActionResult Index(string offerFile)
        {
            ViewBag.PreviewUrl = $"{_appSetting.AsianAffSiteUrl}/UserPanel/OffersForMe/Preview";
            ViewBag.OfferPages = _offerPageService.GetValidEndDateOfferPages();
            return View("~/Areas/Portal/Views/MediaGallery/OfferAff.cshtml", model: offerFile);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu, IsHost = true)]
        public ActionResult InsertOfferAff(InsertOfferAffParam param)
        {
            if (ModelState.IsValid)
            {
                EncodeParam(param);

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
                    Message = string.Join("<br />", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetOfferAffList(SelectOfferAffParam param)
        {
            param.UserId = SessionService.UserId;
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

        private void EncodeParam(InsertOfferAffParam param)
        {
            param.UserId = SessionService.UserId;
            param.Ip = SessionService.RequestIp;
            param.AffNameList = Server.HtmlEncode(param.AffNameList);
        }
    }
}
