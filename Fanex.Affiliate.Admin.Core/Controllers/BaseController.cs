using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Services.Core;

namespace AffiliateAdmin.Areas.Portal.Controllers
{
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
