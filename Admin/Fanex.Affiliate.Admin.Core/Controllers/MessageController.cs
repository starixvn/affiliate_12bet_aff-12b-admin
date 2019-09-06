using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Services.Common;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.HotNews)]
    public class MessageController : BaseController
    {
        public MessageController(ISessionService sessionService) : base(sessionService)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Message()
        {
            return View("~/Areas/Portal/Views/Message/Message.cshtml");
        }
    }
}
