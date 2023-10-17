using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using Fanex.Affiliate.Admin.Core.ViewModels.Domain;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Domain
{
    public class MemberDomainsController : DomainController
    {
        public MemberDomainsController(
            ISessionService sessionService,
            IDomainService domainService,
            ITargetCountryService targetCountryService) : base(sessionService, domainService, targetCountryService)
        {
        }

        public override ActionResult Index()
        {
            var model = new DomainViewModel
            {
                TargetCountryList = _domainService.GetTargetCountryList(new TargetCountryParam { UserId = SessionService.UserId })
            };

            return View("~/Areas/Portal/Views/Domain/Index.cshtml", model);
        }
    }
}
