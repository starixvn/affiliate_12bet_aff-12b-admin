using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Services.Core;

namespace Fanex.Affiliate.Admin.Core.Controllers
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
