using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.DTO;

namespace Fanex.Affiliate.Admin.Core.Controllers.Administrator
{
    [FunctionAuthorize(FunctionName = Function.UserControl)]
    public class DepartmentController : BaseController
    {
        public DepartmentController(ISessionService sessionService) : base(sessionService)
        {
        }

        public ActionResult Index()
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.AdminList, Url = Url.Content("~/Portal/Administrator") },
                new SubMenu { Name = Lang.DeptList, Url = Url.Content("~/Portal/Department"), IsActive = true }
            };

            return View("~/Areas/Portal/Views/Administrator/Department.cshtml", subMenu);
        }
    }
}
