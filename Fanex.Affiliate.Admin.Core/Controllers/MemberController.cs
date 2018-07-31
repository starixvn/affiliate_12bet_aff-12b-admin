using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    public class MemberController : BaseController
    {
        public MemberController(ISessionService sessionService) : base(sessionService)
        {
        }

        public ActionResult Index()
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.Member, Url = Url.Content("~/UserInfo/MemberListAll.aspx") },
                new SubMenu { Name = Lang.ActiveMember, Url = Url.Content("~/UserInfo/MemberActiveList.aspx") }
            };

            return View(subMenu);
        }
    }
}
