using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Services.Common;

namespace Fanex.Affiliate.Admin.Core.Controllers.MediaGallery
{
    [FunctionAuthorize(FunctionName = Function.Media)]
    public class TextList : BaseController
    {
        public TextList(ISessionService sessionService) : base(sessionService)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/MediaGallery/TextList.cshtml");
        }
    }
}
