using System.Linq;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
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
            if (ModelState.IsValid)
            {
                AssignCommonParam(param);

                return Json(_landingPageService.InsertLandingPage(param));
            }

            return Json(new ResultInfo
            {
                Code = Code.GeneralErrorCode,
                Message = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
            });
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Media, IsHost = true)]
        public ActionResult UpdateLandingPage(InsertUpdateLandingPageParam param)
        {
            if (ModelState.IsValid)
            {
                AssignCommonParam(param);

                return Json(_landingPageService.UpdateLandingPage(param));
            }

            return Json(new ResultInfo
            {
                Code = Code.GeneralErrorCode,
                Message = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
            });
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Media, IsHost = true)]
        public ActionResult DeleteLandingPage(DeleteLandingPageParam param)
        {
            if (ModelState.IsValid)
            {
                param.UserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                return Json(_landingPageService.DeleteLandingPage(param));
            }

            return Json(new ResultInfo
            {
                Code = Code.GeneralErrorCode,
                Message = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
            });
        }

        private void AssignCommonParam(InsertUpdateLandingPageParam param)
        {
            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;
        }
    }
}