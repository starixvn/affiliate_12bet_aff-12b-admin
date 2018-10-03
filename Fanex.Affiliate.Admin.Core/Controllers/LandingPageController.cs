using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.Media)]
    public class LandingPageController : BaseController
    {
        private readonly ILandingPageService _landingPageService;

        public LandingPageController(ISessionService sessionService, ILandingPageService landingPageService) : base(sessionService)
        {
            _landingPageService = landingPageService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/LandingPage/Index.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetLandingPageList()
        {
            return Json(_landingPageService.GetLandingPageList());
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetLandingPageItem(int pageId)
        {
            return Json(_landingPageService.GetLandingPageById(pageId));
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Media, IsHost = true)]
        public ActionResult InsertLandingPage(InsertUpdateLandingPageParam param)
        {
            return Json(param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Media, IsHost = true)]
        public ActionResult UpdateLandingPage(InsertUpdateLandingPageParam param)
        {
            return Json(param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Media, IsHost = true)]
        public ActionResult DeleteLandingPage(DeleteLandingPageParam param)
        {
            return Json(param);
        }
    }
}
