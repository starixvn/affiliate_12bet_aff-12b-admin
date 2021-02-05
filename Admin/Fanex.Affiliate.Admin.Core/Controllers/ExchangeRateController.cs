using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.ExchangeRate;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.ExchangeRate;
using System;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.ExchangeRateMenu)]
    public class ExchangeRateController : ReadOnlySessionBaseController
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetExchangeRateList(DateTime date)
        {
            return new JsonNetResult
            {
                Data = _exchangeRateService.GetExchangeRateList(date)
            };
        }

        [FunctionAuthorize(FunctionName = Function.ExchangeRateMenu, IsHost = true)]
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
