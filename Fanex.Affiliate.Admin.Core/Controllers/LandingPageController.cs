using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services.Common;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.Media)]
    public class LandingPageController : BaseController
    {
        private readonly ILandingPageRepository _landingPageRepository;

        public LandingPageController(ISessionService sessionService, ILandingPageRepository landingPageRepository) : base(sessionService)
        {
            _landingPageRepository = landingPageRepository;
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
            return Json(new { });
        }
    }
}
