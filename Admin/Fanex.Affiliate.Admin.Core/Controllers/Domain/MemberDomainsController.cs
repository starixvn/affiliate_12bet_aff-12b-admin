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
            ISystemService systemService) : base(sessionService, domainService, systemService)
        {
        }

        public override ActionResult Index()
        {
            var model = new DomainViewModel
            {
                TargetCountryList = _systemService.GetTargetCountryListByUser()
            };

            return View("~/Areas/Portal/Views/Domain/Index.cshtml", model);
        }
    }
}
