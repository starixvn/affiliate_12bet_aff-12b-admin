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
    public class DomainControlController : BaseController
    {
        public DomainControlController(ISessionService sessionService) : base(sessionService)
        {
        }

        public ActionResult Index()
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.AffiliateDomain, Url = Url.Content("~/Portal/Domain") },
                new SubMenu { Name = Lang.Text12BetDomains, Url = Url.Content("~/Portal/_12BetDomains") },
                new SubMenu { Name = Lang.DomainControl, Url = Url.Content("~/Portal/DomainControl"), IsActive = true }
            };

            return View("~/Areas/Portal/Views/Domain/DomainControl.cshtml", subMenu);
        }
    }
}
