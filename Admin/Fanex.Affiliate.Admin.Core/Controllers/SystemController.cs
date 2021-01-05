using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Extensions;
using System;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [CustomAuthorize]
    class SystemController : Controller
    {
        [HttpPost]
        public ActionResult GetServerNowDate()
        {
            return new JsonNetResult(DateTime.Now.ToString("MM/dd/yyyy"));
        }
    }
}
