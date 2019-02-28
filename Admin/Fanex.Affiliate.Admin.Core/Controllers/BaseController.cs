using System.Web.Mvc;
using System.Web.SessionState;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Services.Common;
using ONEbook.UM.Filters;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [CustomAuthorize]
    [CheckUMFilter]
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class BaseController : Controller
    {
        private readonly ISessionService _sessionService;

        protected BaseController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        protected ISessionService SessionService => _sessionService;

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = int.MaxValue
            };
        }
    }
}
