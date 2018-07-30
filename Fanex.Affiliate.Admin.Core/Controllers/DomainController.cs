using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Services.Common;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = "[Domain]")]
    public class DomainController : BaseController
    {
        public DomainController(ISessionService sessionService) : base(sessionService)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
