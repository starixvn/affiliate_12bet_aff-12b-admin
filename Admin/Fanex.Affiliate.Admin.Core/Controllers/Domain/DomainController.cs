using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;

namespace Fanex.Affiliate.Admin.Core.Controllers.Domain
{
    [FunctionAuthorize(FunctionName = Function.Domain)]
    public class DomainController : BaseController
    {
        public DomainController(ISessionService sessionService) : base(sessionService)
        {
        }

        public ActionResult Index()
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.AffiliateDomain, Url = Url.Content("~/Portal/Domain"), IsActive = true },
                new SubMenu { Name = Lang.Text12BetDomains, Url = Url.Content("~/Portal/_12BetDomains") },
                new SubMenu { Name = Lang.DomainControl, Url = Url.Content("~/Portal/DomainControl") }
            };

            return View(subMenu);
        }
    }
}
