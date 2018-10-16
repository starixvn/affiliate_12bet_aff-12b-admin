using Fanex.Affiliate.Admin.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Constants;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class CustomerStatisticsController : BaseController
    {
        public CustomerStatisticsController(ISessionService sessionService) : base(sessionService)
        {
        }

        public ActionResult index()
        {
            return View();
        }
    }
}
