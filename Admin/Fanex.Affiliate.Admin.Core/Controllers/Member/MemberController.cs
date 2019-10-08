using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.ViewModels.Member;

namespace Fanex.Affiliate.Admin.Core.Controllers.Member
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class MemberController : BaseController
    {
        public MemberController(ISessionService sessionService) : base(sessionService)
        {
        }

        public ActionResult Index(string affName)
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.Member, Url = Url.Content("~/Portal/Member"), IsActive = true },
                new SubMenu { Name = Lang.ActiveMember, Url = Url.Content("~/Portal/ActiveMember") }
            };

            var model = new MemberListViewModel
            {
                SubMenuList = subMenu,
                AffName = affName
            };

            return View(model);
        }
    }
}
