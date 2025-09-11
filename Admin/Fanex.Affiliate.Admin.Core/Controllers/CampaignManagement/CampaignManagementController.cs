using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.CampaignManagement
{
    public class CampaignManagementController : ReadOnlySessionBaseController
    {
        public CampaignManagementController(
            ISessionService sessionService) : base(sessionService)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
