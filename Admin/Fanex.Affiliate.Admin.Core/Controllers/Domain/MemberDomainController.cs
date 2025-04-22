using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using Fanex.Affiliate.Admin.Core.ViewModels.Domain;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Domain
{
    public class MemberDomainController : DomainController
    {
        public MemberDomainController(
            ISessionService sessionService,
            IDomainService domainService) : base(sessionService, domainService)
        {
        }

        public override ActionResult Index()
        {
            return View("~/Areas/Portal/Views/Domain/Index.cshtml", model: new DomainViewModel());
        }
    }
}
