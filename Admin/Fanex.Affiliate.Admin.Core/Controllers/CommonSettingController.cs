using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Services.Common;
using ONEbook.UM.AspNetMVC.Filters;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [CustomAuthorize]
    [CheckUMFilter]
    public class CommonSettingController : Controller
    {
        private readonly ISystemService _commonSettingService;

        public CommonSettingController(
            ISystemService commonSettingService
            )
        {
            _commonSettingService = commonSettingService;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.CustomerMenu)]
        public ActionResult GetProductList()
        {
            return new JsonNetResult
            {
                Data = _commonSettingService.GetProductList()
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.CustomerMenu)]
        public ActionResult GetCountryList()
        {
            return new JsonNetResult
            {
                Data = _commonSettingService.GetCountryList()
            };
        }
    }
}