using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.Domain)]
    public class DomainController : BaseController
    {
        public DomainController(ISessionService sessionService) : base(sessionService)
        {
        }

        public ActionResult Index(int current = 0)
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.AffiliateDomain, Url = Url.Content("~/Domain/AffiliateDomain.aspx") },
                new SubMenu { Name = Lang.Text12BetDomains, Url = Url.Content("~/Domain/12BetDomain.aspx") },
                new SubMenu { Name = Lang.DomainControl, Url = Url.Content("~/Domain/DomainControl.aspx") }
            };

            var activeIndex = current > subMenu.Count - 1 ? 0 : current;
            subMenu[activeIndex].IsActive = true;

            return View(subMenu);
        }
    }
}
