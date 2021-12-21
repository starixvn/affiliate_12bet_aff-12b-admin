using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using System.Web;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.MediaGallery
{
    public class OfferPageController : ReadOnlySessionBaseController
    {
        private readonly ILanguageService _languageService;
        private readonly IOfferPageService _offerPageService;
        private readonly IAppSetting _appSetting;

        public OfferPageController(
            IOfferPageService offerPageService,
            ISessionService sessionService,
            ILanguageService languageService,
            IAppSetting appSetting) : base(sessionService)
        {
            _languageService = languageService;
            _offerPageService = offerPageService;
            _appSetting = appSetting;
        }

        public ActionResult Index()
        {
            ViewBag.PreviewUrl = $"{_appSetting.AsianAffSiteUrl}/UserPanel/OffersForMe/Preview";
            ViewBag.Languages = _languageService.GetInChangeMarkets();
            return View("~/Areas/Portal/Views/MediaGallery/Common.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu, IsHost = true)]
        public ActionResult InsertOfferPage(HttpPostedFileBase file, InsertHtmlFileParam param)
        {
            if (ModelState.IsValid && file != null)
            {
                EncodeParam(param);
                param.FileSize = file.ContentLength;

                return new JsonNetResult
                {
                    Data = _offerPageService.InsertOfferPage(file, param)
                };
            }

            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr(Lang.InvalidFileBrowser)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu, IsHost = true)]
        public ActionResult UpdateOfferPage(HttpPostedFileBase file, UpdateOfferPageParam param)
        {
            if (ModelState.IsValid)
            {
                EncodeParam(param);
                param.FileSize = file?.ContentLength ?? param.FileSize;

                return new JsonNetResult
                {
                    Data = _offerPageService.UpdateOfferPage(file, param)
                };
            }

            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr(Lang.InvalidFileBrowser)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetOfferPageList(PagingParam param)
        {
            var data = _offerPageService.GetOfferPageList(param);

            return new JsonNetResult
            {
                Data = new EntityResult<OfferPage>
                {
                    MainData = data.MainData,
                    TotalItem = data.TotalItem
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu, IsHost = true)]
        public ActionResult DeleteOfferPages(string pageIds)
        {
            var param = new DeleteOfferPageParam
            {
                PageIds = pageIds,
                UserId = SessionService.UserId,
                Ip = SessionService.RequestIp,

            };

            return new JsonNetResult
            {
                Data = _offerPageService.DeleteOfferPages(param)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetOfferPageDetailById(int pageId)
        {
            var item = _offerPageService.GetOfferPageById(pageId);

            return new JsonNetResult
            {
                Data = item
            };
        }

        private void EncodeParam(InsertHtmlFileParam param)
        {
            param.UserId = SessionService.UserId;
            param.Ip = SessionService.RequestIp;
            param.FileName = Server.HtmlEncode(param.FileName);
            param.Language = Server.HtmlEncode(param.Language);
        }
    }
}
