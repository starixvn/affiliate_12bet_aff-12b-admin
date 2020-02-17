using System;
using System.Globalization;
using System.Web.Mvc;
using CitiXchangeLibrary;
using Fanex.Affiliate.ThirdPartyProduct.Attributes;
using Fanex.Affiliate.ThirdPartyProduct.Constants;
using Fanex.Affiliate.ThirdPartyProduct.MetaModels;
using Fanex.ThirdParty.Importing.Model;
using Fanex.ThirdParty.Importing.Service;
using Newtonsoft.Json;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.Core.Constants;

namespace Fanex.Affiliate.ThirdPartyProduct.Controllers
{
    [Authorize]
    [FunctionAuthorize]
    public class ExchangeSummaryTransctionController : Controller
    {
        private readonly IExchangeDataGettingService _service;

        public ExchangeSummaryTransctionController()
        {
            _service = new ExchangeSettledSummaryService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/ThirdPartyProduct/ExchangeSummaryTransaction/Index.cshtml");
        }

        [HttpPost]
        public ActionResult Index(ExchangeRequestParam param)
        {
            var startTime = DateTime.ParseExact(param.StartTime, Format.RequestDateTime, CultureInfo.InvariantCulture);
            var endTime = DateTime.ParseExact(param.EndTime, Format.RequestDateTime, CultureInfo.InvariantCulture);

            if (param.IsGetByApiTimeZone)
            {
                startTime = DateTimeConverter.ConvertTime(startTime, TimeZoneId.GmtPlus8, TimeZoneInfo.Local.Id);
                endTime = DateTimeConverter.ConvertTime(endTime, TimeZoneId.GmtPlus8, TimeZoneInfo.Local.Id);
            }

            var criteria = new ExChangeApiParameter
            {
                IbcCustId = param.CustId,
                LocalStartTime = startTime,
                LocalEndTime = endTime,
                PageNumber = param.PageNumber
            };

            Enviro env;
            if (!Enum.TryParse<Enviro>(param.Environment, out env))
            {
                env = Enviro.PRD;
            }

            return Content(JsonConvert.SerializeObject(new
            {
                RawData = _service.GetJsonData(criteria, env)
            }), "application/json");
        }
    }
}
