using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Utilities;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    public class SubAffLinkController : BaseMarketingController
    {
        public SubAffLinkController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ISystemService systemsService,
            IMarketingService marketingService,
            IAccountService accountService)
            : base(sessionService, appSetting, systemsService, marketingService, accountService)
        { }

        public ActionResult Index()
        {
            return RedirectToRoute("UserPanel", new { controller = "AffiliateLink", isTargetToMemberSite = false });
        }
    }
}