using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Constants;
using Fanex.Affiliate.DataService.Importer.MetaModels.GPI;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Fanex.Affiliate.ThirdPartyProduct.Attributes;
using Fanex.Affiliate.ThirdPartyProduct.Constants;
using Fanex.Affiliate.ThirdPartyProduct.MetaModels;
using Fanex.Affiliate.ThirdPartyProduct.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Fanex.Affiliate.ThirdPartyProduct.Controllers
{
    public class GpiLotteryController : Controller
    {
        private readonly IGpiLotteryService GpiLotteryService;

        public GpiLotteryController()
        {
            GpiLotteryService = new GpiLotteryService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/ThirdPartyProduct/GpiLottery/Index.cshtml");
        }

        [HttpPost]
        [Compress]
        public ActionResult Index(GplLotteryRequestParam param)
        {
            var logBuilder = new StringBuilder();
            var rawDataBuilder = new StringBuilder();

            try
            {
                var startTime = DateTime.ParseExact(param.StartTime, Format.RequestDateTime, CultureInfo.InvariantCulture);
                var endTime = DateTime.ParseExact(param.EndTime, Format.RequestDateTime, CultureInfo.InvariantCulture);

                if (param.IsGetByApiTimeZone)
                {
                    startTime = DateTimeConverter.ConvertTime(startTime, TimeZoneId.GmtPlus8, TimeZoneInfo.Local.Id);
                    endTime = DateTimeConverter.ConvertTime(endTime, TimeZoneId.GmtPlus8, TimeZoneInfo.Local.Id);
                }

                var rawDataList = GpiLotteryService.GetRawDataList(startTime, endTime);

                if (rawDataList.Any())
                {
                    foreach (var item in rawDataList)
                    {
                        rawDataBuilder.AppendLine(item);
                        rawDataBuilder.AppendLine("-----------------------------------------");
                        rawDataBuilder.AppendLine();
                    }
                }
            }
            catch (Exception ex)
            {
                logBuilder.AppendLine(ex.Message);
                logBuilder.AppendLine(ex.StackTrace);
                logBuilder.AppendLine(ex.Source);
            }

            return Content(JsonConvert.SerializeObject(new ThirdPartyProductViewModel
            {
                LogMessage = logBuilder.ToString(),
                RawData = rawDataBuilder.ToString()
            }), "application/json");
        }
    }
}
