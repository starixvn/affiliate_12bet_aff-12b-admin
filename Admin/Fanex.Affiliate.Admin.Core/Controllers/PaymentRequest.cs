using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    public class PaymentRequest : BaseController
    {
        public PaymentRequest(ISessionService sessionService) : base(sessionService)
        {
        }

        public ActionResult index()
        {
            return View();
        }
    }
}
