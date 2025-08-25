using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.Operation;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services.Operation;
using Fanex.Affiliate.Admin.Core.Utilities;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.MediaGallery
{
    [FunctionAuthorize(FunctionName = Function.Media)]
    public class LandingPageListController : ReadOnlySessionBaseController
    {
        private readonly ILandingPageListService _landingPageService;
        private readonly ISystemService _systemService;
        private readonly ICommonDealService _commGroupCommonService;

        public LandingPageListController(
            ISessionService sessionService,
            ILandingPageListService landingPageService,
            ISystemService systemService,
            ICommonDealService commGroupCommonService) : base(sessionService)
        {
            _landingPageService = landingPageService;
            _systemService = systemService;
            _commGroupCommonService = commGroupCommonService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.DealList = _commGroupCommonService.GetNormalDealList(new GetNormalDealParam());
            return View("~/Areas/Portal/Views/MediaGallery/LandingPageList.cshtml");
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
            if (!TargetCountryHelper.IsValidTargetCountryCodes(param.BlockTargetCountry, _systemService.GetTargetCountryList()))
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
                PageCode = Server.HtmlEncode(param.PageCode),
                DealIds = Server.HtmlEncode(param.DealIds),
                IsExcluded = param.IsExcluded,
                IsMobile = param.IsMobile
            };
        }
    }
}