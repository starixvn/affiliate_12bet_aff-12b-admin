using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Services.Common;
using ONEbook.UM.AspNetMVC.Filters;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [CustomAuthorize]
    [CheckUMFilter]
    [FunctionAuthorize(FunctionName = Function.CustomerMenu)]
    public class CommonSettingController : Controller
    {
        private readonly ISystemService _systemService;

        public CommonSettingController(
            ISystemService systemService
            )
        {
            _systemService = systemService;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetProductList()
        {
            return new JsonNetResult
            {
                Data = _systemService.GetProductList()
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetCountryList()
        {
            return new JsonNetResult
            {
                Data = _systemService.GetCountryList()
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetAllTargetLanguageList()
        {
            return new JsonNetResult
            {
                Data = _systemService.GetAllTargetLanguageListByUser()
            };
        }
    }
}