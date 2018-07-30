using System.Web.Mvc;
using System.Web.Routing;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Utilities;
using ONEbook.UM.Filters;

namespace Fanex.Affiliate.User.Core.Controllers
{
    [CheckUMFilter]
    public class BaseController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IAppSetting _appSetting;

        protected BaseController(ISessionService sessionService, IAppSetting appSetting)
        {
            _sessionService = sessionService;
            _appSetting = appSetting;
        }

        protected ISessionService SessionService => _sessionService;

        protected IAppSetting AppSetting => _appSetting;

       
    }
}
