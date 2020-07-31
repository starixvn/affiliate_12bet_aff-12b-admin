using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Services.Common;
using ONEbook.UM.Filters;
using System.Web.Mvc;
using System.Web.SessionState;

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
    }
}
