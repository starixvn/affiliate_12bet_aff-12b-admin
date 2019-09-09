using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Message;
using Fanex.Affiliate.Admin.Core.Services.Message;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.HotNews)]
    public class MessageController : BaseController
    {
        private readonly IMessageService _messageSevice;

        public MessageController(
            ISessionService sessionService,
            IMessageService messageSevice) : base(sessionService)
        {
            _messageSevice = messageSevice;
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

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetMessageList(MessageParam param)
        {
            return new JsonNetResult
            {
                Data = _messageSevice.GetMessageList(param)
            };
        }
    }
}
