using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.MetaModels.ISoftbet;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Fanex.Affiliate.ThirdPartyProduct.Attributes;
using Fanex.Affiliate.ThirdPartyProduct.Constants;
using Fanex.Affiliate.ThirdPartyProduct.MetaModels;
using Fanex.Affiliate.ThirdPartyProduct.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fanex.Affiliate.ThirdPartyProduct.Controllers
{
    public class ISoftbetController : Controller
    {
        private readonly IISoftbetService _isoftbetService;

        public ISoftbetController()
        {
            _isoftbetService = new ISoftbetService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/ThirdPartyProduct/ISoftbet/Index.cshtml");
        }

        [HttpPost]
        [Compress]
        public ActionResult Index(ISoftbetRequestParam param)
        {
            var logBuilder = new StringBuilder();
            var rawDataBuilder = new StringBuilder();
            var convertedDataBuilder = new StringBuilder();

            try
            {
                var startTime = DateTime.ParseExact(param.StartTime, Format.RequestDateTime, CultureInfo.InvariantCulture);
                var endTime = DateTime.ParseExact(param.EndTime, Format.RequestDateTime, CultureInfo.InvariantCulture);
                var isoftbetParam = new ISoftbetEntryParam
                {
                    StartTime = startTime,
                    EndTime = endTime,
                    PageNumber = param.PageNumber
                };

                var rawDataList = _isoftbetService.GetRawDataList(isoftbetParam);
                if (rawDataList.Any())
                {
                    foreach (var item in rawDataList)
                    {
                        rawDataBuilder.AppendLine(item.JsonData);
                        rawDataBuilder.AppendLine("-----------------------------------------");
                        rawDataBuilder.AppendLine();

                        var jObject = JObject.Parse(item.JsonData);
                        var status = Convert.ToString(jObject["status"]);
                        if (string.Equals("success", status))
                        {
                            var summary = ISoftbetParser.FromJsonObject(jObject);
                            var xml = _isoftbetService.ConvertToXml(summary, item.Param);

                            convertedDataBuilder.AppendLine(xml);
                            convertedDataBuilder.AppendLine("-----------------------------------------");
                            convertedDataBuilder.AppendLine();
                        }
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
