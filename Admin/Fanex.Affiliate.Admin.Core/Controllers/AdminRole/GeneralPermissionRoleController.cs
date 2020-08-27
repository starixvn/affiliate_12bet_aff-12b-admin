using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.AdminRole;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.AdminRole
{
    [FunctionAuthorize(FunctionName = Function.UserControl)]
    public class GeneralPermissionRoleController : BaseController
    {
        private readonly ILanguageService _languageService;
        private readonly IAdminRoleCommonService _adminRoleCommonService;
        public GeneralPermissionRoleController(
            ISessionService sessionService,
            ILanguageService languageService,
            IAdminRoleCommonService adminRoleCommonService) : base(sessionService)
        {
            _languageService = languageService;
            _adminRoleCommonService = adminRoleCommonService;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetFunctionListOfAllRoles()
        {
            return new JsonNetResult
            {
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetLanguageList()
        {
            return new JsonNetResult
            {
                Data = _languageService.GetLanguageList()
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetMarketList()
        {
            return new JsonNetResult
            {
                Data = _adminRoleCommonService.GetMarketList()
            };
        }
    }
}
