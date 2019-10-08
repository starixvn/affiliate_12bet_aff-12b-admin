using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.Controllers.Member
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class ActiveMemberController: BaseController
    {
        public ActiveMemberController(ISessionService sessionService) : base(sessionService)
        {
        }

        public ActionResult Index()
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.Member, Url = Url.Content("~/Portal/Member") },
                new SubMenu { Name = Lang.ActiveMember, Url = Url.Content("~/Portal/ActiveMember"), IsActive = true }
            };

            return View("~/Areas/Portal/Views/Member/ActiveMember.cshtml", subMenu);
        }
    }
}
