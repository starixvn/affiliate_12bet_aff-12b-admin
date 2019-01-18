using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.MetaModels.Opus;
using Fanex.Affiliate.ThirdPartyProduct.Attributes;
using Fanex.Affiliate.ThirdPartyProduct.Constants;
using Fanex.Affiliate.ThirdPartyProduct.MetaModels;
using Fanex.Affiliate.ThirdPartyProduct.ViewModels;
using Newtonsoft.Json;

namespace Fanex.Affiliate.ThirdPartyProduct.Controllers
{
    public class OpusController : Controller
    {
        private readonly IOpusService _opusService;

        public OpusController()
        {
            _opusService = new OpusService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/ThirdPartyProduct/Opus/Index.cshtml");
        }

        [HttpPost]
        [Compress]
        public ActionResult Index(OpusRequestParam param)
        {
            var logBuilder = new StringBuilder();
            var rawDataBuilder = new StringBuilder();
            var convertedDataBuilder = new StringBuilder();

            try
            {
                var startTime = DateTime.ParseExact(param.StartTime, Format.RequestDateTime, CultureInfo.InvariantCulture);
                var endTime = DateTime.ParseExact(param.EndTime, Format.RequestDateTime, CultureInfo.InvariantCulture);
                var opusParam = new OpusParam
                {
                    StartTime = startTime,
                    EndTime = endTime
                };
                var rawDataList = _opusService.GetRawDataList(opusParam);
                if (rawDataList.Any())
                {
                    foreach (var item in rawDataList)
                    {
                        rawDataBuilder.AppendLine(item);
                        rawDataBuilder.AppendLine("-----------------------------------------");
                        convertedDataBuilder.AppendLine();
                    }

                    var convertedData = _opusService.ConvertDataTimeZone(rawDataList);
                    foreach (var item in convertedData)
                    {
                        convertedDataBuilder.AppendLine(item);
                        convertedDataBuilder.AppendLine("-----------------------------------------");
                        convertedDataBuilder.AppendLine();
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
                RawData = rawDataBuilder.ToString(),
                ConvertedData = convertedDataBuilder.ToString()
            }), "application/json");
        }
    }
}
