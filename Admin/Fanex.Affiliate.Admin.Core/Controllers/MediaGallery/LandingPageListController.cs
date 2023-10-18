using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using System.Linq;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.MediaGallery
{
    [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu)]
    public class LandingPageListController : ReadOnlySessionBaseController
    {
        private readonly ILandingPageService _landingPageService;
        private readonly ITargetCountryService _targetCountryService;

        public LandingPageListController(
            ISessionService sessionService,
            ILandingPageService landingPageService,
            ITargetCountryService targetCountryService) : base(sessionService)
        {
            _landingPageService = landingPageService;
            _targetCountryService = targetCountryService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/MediaGallery/LandingPageList.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetLandingPageList()
        {
            return new JsonNetResult
            {
                Data = _landingPageService.GetLandingPageList()
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetLandingPageItem(int pageId)
        {
            return new JsonNetResult
            {
                Data = _landingPageService.GetLandingPageById(pageId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetVisibleTargetCountriesByUser()
        {
            return new JsonNetResult
            {
                Data = _targetCountryService.GetVisibleTargetCountriesByUser(SessionService.UserId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetExcludedTargetCountryList(ExcludedTargetCountryParam param)
        {
            param.UserId = SessionService.UserId;

            return new JsonNetResult
            {
                Data = _landingPageService.GetExcludedTargetCountryList(param)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu, IsHost = true)]
        public ActionResult InsertLandingPage(InsertUpdateLandingPageParam param)
        {
            if (ModelState.IsValid)
            {
                var encodeParam = EncodeParam(param);
                AssignCommonParam(encodeParam);

                return new JsonNetResult
                {
                    Data = _landingPageService.InsertLandingPage(encodeParam)
                };
            }

            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr(string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)))
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu, IsHost = true)]
        public ActionResult UpdateLandingPage(InsertUpdateLandingPageParam param)
        {
            if (ModelState.IsValid)
            {
                var encodeParam = EncodeParam(param);
                AssignCommonParam(encodeParam);

                return new JsonNetResult
                {
                    Data = _landingPageService.UpdateLandingPage(encodeParam)
                };
            }

            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr(string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)))
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu, IsHost = true)]
        public ActionResult DeleteLandingPage(DeleteLandingPageParam param)
        {
            if (ModelState.IsValid)
            {
                param.UserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                return new JsonNetResult
                {
                    Data = _landingPageService.DeleteLandingPage(param)
                };
            }

            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr(string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)))
            };
        }

        private InsertUpdateLandingPageParam EncodeParam(InsertUpdateLandingPageParam param)
        {
            return new InsertUpdateLandingPageParam
            {
                PageId = param.PageId,
                PageName = Server.HtmlEncode(param.PageName),
                PageCode = Server.HtmlEncode(param.PageCode)
            };
        }

        private void AssignCommonParam(InsertUpdateLandingPageParam param)
        {
            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;
        }
    }
}