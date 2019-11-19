using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Extensions;
using System;
using Fanex.Affiliate.Admin.Core.Services.ExchangeRate;
using Fanex.Affiliate.Admin.Core.MetaModels.ExchangeRate;
using Fanex.Affiliate.Admin.Core.DTO;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.AffExRate)]
    public class ExchangeRateController : BaseController
    {
        private readonly IExchangeRateService _exchangeRateService;
        public ExchangeRateController(
            ISessionService sessionService,
            IExchangeRateService exchangeRateService) : base(sessionService)
        {
            _exchangeRateService = exchangeRateService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var permissionList = SessionService.UserPermissionList;
            var hasPermission = permissionList.Any(i => i.FunctionName.Equals(Function.AffExRate, StringComparison.OrdinalIgnoreCase) && i.IsHost);

            return View(model: hasPermission);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetExchangeRateList(DateTime date)
        {
            return new JsonNetResult {
                Data = _exchangeRateService.GetExchangeRateList(date)
            };
        }

        [FunctionAuthorize(FunctionName = Function.AffExRate, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult UpdateExchangeRate(ExchangeRateParam param)
        {
            if (ModelState.IsValid && param.IsValid())
            {
                param.UserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                return new JsonNetResult
                {
                    Data = _exchangeRateService.UpdateExchangeRate(param)
                };
            }
            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "The rate is invalid."
                }
            };
        }
    }
}
