using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Extensions;
using System;
using Fanex.Affiliate.Admin.Core.Repository.ExchangeRate;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.AffExRate)]
    public class ExchangeRateController : BaseController
    {
        private readonly IExchangeRateRepository _exchangeRateRepository;
        public ExchangeRateController(
            ISessionService sessionService,
            IExchangeRateRepository exchangeRateRepository) : base(sessionService)
        {
            _exchangeRateRepository = exchangeRateRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExchangeRate()
        {
            return View("~/Areas/Portal/Views/ExchangeRate/ExchangeRate.cshtml");
        }

        public ActionResult GetExchangeRateList(DateTime date)
        {
            return new JsonNetResult {
                Data = _exchangeRateRepository.GetExchangeRateList(date)
            };
        }
    }
}
