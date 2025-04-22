using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Fanex.Affiliate.Admin.Core.Utilities;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.MediaGallery
{
    [FunctionAuthorize(FunctionName = Function.Media)]
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
            return View("~/Areas/Portal/Views/MediaGallery/LandingPageList.cshtml", model: _systemService.GetTargetCountries());
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetLandingPageList()
        {
            return GenerateJsonNetResult(_landingPageService.GetLandingPageList());
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetLandingPageItem(int pageId)
        {
            return GenerateJsonNetResult(_landingPageService.GetLandingPageById(pageId));
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetTargetCountryList()
        {
            return GenerateJsonNetResult(_systemService.GetTargetCountries());
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetTargetCountryListByLandingPage(int pageId)
        {
            return GenerateJsonNetResult(_landingPageService.GetExcludedTargetCountryList(pageId));
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Media, IsHost = true)]
        public ActionResult InsertLandingPage(InsertUpdateLandingPageParam param)
        {
            return HandleSaveLandingPage(param, true);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Media, IsHost = true)]
        public ActionResult UpdateLandingPage(InsertUpdateLandingPageParam param)
        {
            return HandleSaveLandingPage(param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Media, IsHost = true)]
        public ActionResult DeleteLandingPage(DeleteLandingPageParam param)
        {
            if (ModelState.IsValid)
            {
                AssignUserInfoParam(param);

                return GenerateJsonNetResult(_landingPageService.DeleteLandingPage(param));
            }

            return GenerateModelStateErrResult();
        }

        private ActionResult HandleSaveLandingPage(InsertUpdateLandingPageParam param, bool isInsert = false)
        {
            if (!TargetCountryHelper.IsValidTargetCountryCodes(param.BlockTargetCountry, _systemService.GetTargetCountries()))
            {
                return GenerateErrResult("The target country's info is invalid!");
            }

            if (ModelState.IsValid)
            {
                var encodeParam = EncodeParam(param);
                AssignUserInfoParam(encodeParam);

                return GenerateJsonNetResult(_landingPageService.HandleSaveLandingPage(encodeParam, isInsert));
            }

            return GenerateModelStateErrResult();
        }

        private InsertUpdateLandingPageParam EncodeParam(InsertUpdateLandingPageParam param)
        {
            return new InsertUpdateLandingPageParam
            {
                PageId = param.PageId,
                BlockTargetCountry = Server.HtmlEncode(param.BlockTargetCountry),
                PageName = Server.HtmlEncode(param.PageName),
                PageCode = Server.HtmlEncode(param.PageCode)
            };
        }
    }
}