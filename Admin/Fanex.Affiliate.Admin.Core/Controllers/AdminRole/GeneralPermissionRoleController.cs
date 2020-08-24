using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Services;
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
        public GeneralPermissionRoleController(
            ISessionService sessionService,
            ILanguageService languageService) : base(sessionService)
        {
            _languageService = languageService;
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
    }
}
