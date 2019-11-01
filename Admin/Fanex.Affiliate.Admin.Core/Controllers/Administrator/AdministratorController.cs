using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Administrator;
using Fanex.Affiliate.Admin.Core.Extensions;

namespace Fanex.Affiliate.Admin.Core.Controllers.Administrator
{
    [FunctionAuthorize(FunctionName = Function.UserControl)]
    public class AdministratorController : BaseController
    {
        private readonly IAdministratorService _administratorService;

        public AdministratorController(
            ISessionService sessionService,
            IAdministratorService administratorService) : base(sessionService)
        {
            _administratorService = administratorService;
        }

        public ActionResult Index()
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.AdminList, Url = Url.Content("~/Portal/Administrator"), IsActive = true },
                new SubMenu { Name = Lang.DeptList, Url = Url.Content("~/Portal/Department") }
            };

            return View(subMenu);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetPolicyList(int groupId)
        {
            return new JsonNetResult
            {
                Data = _administratorService.GetPolicyList(groupId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetAdminList(string userName, string firstLetter, int groupId = 0)
        {
            return new JsonNetResult
            {
                Data = _administratorService.GetAdminList(groupId, userName, firstLetter)
            };
        }
    }
}
