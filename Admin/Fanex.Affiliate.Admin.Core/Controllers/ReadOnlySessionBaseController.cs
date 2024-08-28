using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Services.Common;
using ONEbook.UM.AspNetMVC.Filters;
using System.Linq;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [CustomAuthorize]
    [CheckUMFilter]
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class ReadOnlySessionBaseController : Controller
    {
        protected ISessionService SessionService { get; }

        protected ReadOnlySessionBaseController(ISessionService sessionService)
        {
            SessionService = sessionService;
        }
    }
}
