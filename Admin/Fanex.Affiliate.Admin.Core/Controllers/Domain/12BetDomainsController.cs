using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Domain
{
    [FunctionAuthorize(FunctionName = Function.Domain)]
    public class _12BetDomainsController : BaseController
    {
        public _12BetDomainsController(ISessionService sessionService) : base(sessionService)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.AffiliateDomain, Url = Url.Content("~/Portal/Domain") },
                new SubMenu { Name = Lang.Text12BetDomains, Url = Url.Content("~/Portal/_12BetDomains"), IsActive = true },
                new SubMenu { Name = Lang.DomainControl, Url = Url.Content("~/Portal/DomainControl") }
            };

            return View("~/Areas/Portal/Views/Domain/_12BetDomains.cshtml", subMenu);
        }
    }
}
