using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Domain
{
    public class MemberDomainController : DomainController
    {
        public MemberDomainController(
            ISessionService sessionService,
            IDomainService domainService,
            ISystemService systemService) : base(sessionService, domainService, systemService)
        {
        }

        public override ActionResult Index()
        {
            return RedirectToCommonIndex(false);
        }
    }
}
