using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Fanex.Affiliate.Admin.Core.Utilities;
using System.Linq;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.MediaGallery
{
    [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu)]
    public class LandingPageListController : ReadOnlySessionBaseController
    {
        private readonly ILandingPageService _landingPageService;
        private readonly ISystemService _systemService;

        public LandingPageListController(
            ISessionService sessionService,
            ILandingPageService landingPageService,
            ISystemService systemService) : base(sessionService)
        {
            _landingPageService = landingPageService;
            _systemService = systemService;
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
                Data = _landingPageService.GetLandingPageList(SessionService.UserId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetLandingPageItem(int pageId)
        {
            return new JsonNetResult
            {
                Data = _landingPageService.GetLandingPageById(SessionService.UserId, pageId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetTargetCountryListByUser()
        {
            return new JsonNetResult
            {
                Data = _systemService.GetTargetCountryListByUser()
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetTargetCountryListByLandingPage(ExcludedTargetCountryParam param)
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
            return HandleSaveLandingPage(param, true);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu, IsHost = true)]
        public ActionResult UpdateLandingPage(InsertUpdateLandingPageParam param)
        {
            return HandleSaveLandingPage(param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu, IsHost = true)]
        public ActionResult DeleteLandingPage(DeleteLandingPageParam param)
        {
            if (ModelState.IsValid)
            {
                AssignUserInfoParam(param);

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

        private JsonNetResult HandleSaveLandingPage(InsertUpdateLandingPageParam param, bool isInsert = false)
        {
            if (!SystemHelper.IsValidTargetCountryCodes(param.ExcludedTargetCountries, _systemService.GetTargetCountryListByUser()))
            {
                return new JsonNetResult
                {
                    Data = ResultInfo.GenerateErr("The target country's info is invalid!")
                };
            }

            if (ModelState.IsValid)
            {
                var encodeParam = EncodeParam(param);
                AssignUserInfoParam(encodeParam);

                return new JsonNetResult
                {
                    Data = _landingPageService.HandleSaveLandingPage(encodeParam, isInsert)
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
                ExcludedTargetCountries = Server.HtmlEncode(param.ExcludedTargetCountries),
                PageName = Server.HtmlEncode(param.PageName),
                PageCode = Server.HtmlEncode(param.PageCode)
            };
        }
    }
}