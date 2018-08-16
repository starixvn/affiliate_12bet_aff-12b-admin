using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Services.Common;
using ONEbook.UM.Filters;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [CustomAuthorize]
    [CheckUMFilter]
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
