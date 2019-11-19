using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Constants;
using Fanex.Affiliate.DataService.Importer.MetaModels.Opus;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Fanex.Affiliate.ThirdPartyProduct.Attributes;
using Fanex.Affiliate.ThirdPartyProduct.Constants;
using Fanex.Affiliate.ThirdPartyProduct.MetaModels;
using Fanex.Affiliate.ThirdPartyProduct.ViewModels;
using Newtonsoft.Json;

namespace Fanex.Affiliate.ThirdPartyProduct.Controllers
{
    [Authorize]
    [FunctionAuthorize]
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
        public ActionResult Index(OpusRequestParam param)
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

                var opusParam = new OpusParam
                {
                    StartTime = startTime,
                    EndTime = endTime
                };
                var rawDataList = _opusService.GetRawDataList(opusParam);
                param.UserName = param.UserName?.Trim();
                if (!string.IsNullOrEmpty(param.UserName))
                {
                    rawDataList = GetDataByUserName(rawDataList, param.UserName);
                }

                foreach (var item in rawDataList)
                {
                    rawDataBuilder.AppendLine(item);
                    rawDataBuilder.AppendLine("-----------------------------------------");
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

        private static IEnumerable<string> GetDataByUserName(IEnumerable<string> rawDataList, string userName)
        {
            var dataList = new List<string>();

            foreach (var rawData in rawDataList)
            {
                var xdoc = XDocument.Parse(rawData);
                var betHistoryList = xdoc.Root.Element("bets").Elements("row");

                betHistoryList.Where(x => !string.Equals(x.Element("member_code").Value, userName, StringComparison.InvariantCultureIgnoreCase))
                    .Remove();

                if (betHistoryList.Any())
                {
                    dataList.Add(xdoc.ToString());
                }
            }

            return dataList;
        }
    }
}
