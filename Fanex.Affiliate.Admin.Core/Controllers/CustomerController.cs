using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Services.Common;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class CustomerController : BaseController
    {
        public CustomerController(ISessionService sessionService) : base(sessionService)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}