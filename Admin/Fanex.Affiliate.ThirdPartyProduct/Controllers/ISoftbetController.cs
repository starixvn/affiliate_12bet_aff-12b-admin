using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.DTO.ISoftbet;
using Fanex.Affiliate.DataService.Importer.MetaModels.ISoftbet;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Fanex.Affiliate.ThirdPartyProduct.Attributes;
using Fanex.Affiliate.ThirdPartyProduct.Constants;
using Fanex.Affiliate.ThirdPartyProduct.DTO;
using Fanex.Affiliate.ThirdPartyProduct.MetaModels;
using Fanex.Affiliate.ThirdPartyProduct.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fanex.Affiliate.ThirdPartyProduct.Controllers
{
    [Authorize]
    [FunctionAuthorize]
    public class ISoftbetController : Controller
    {
        private const string SuccessStatus = "success";

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

                if (param.IsGetByApiTimeZone)
                {
                    startTime = DateTimeConverter.ToGMT(startTime, TimeZoneInfo.Local.Id);
                    endTime = DateTimeConverter.ToGMT(endTime, TimeZoneInfo.Local.Id);
                }

                var isoftbetParam = new ISoftbetEntryParam
                {
                    StartTime = startTime,
                    EndTime = endTime,
                    PageNumber = param.PageNumber
                };

                var rawDataList = _isoftbetService.GetRawDataList(isoftbetParam);
                foreach (var item in rawDataList)
                {
                    rawDataBuilder.AppendLine(item.JsonData);
                    rawDataBuilder.AppendLine("-----------------------------------------");
                    rawDataBuilder.AppendLine();

                    var jObject = JObject.Parse(item.JsonData);
                    var status = Convert.ToString(jObject["status"]);
                    if (!string.Equals("success", status))
                    {
                        return GetContentResult(string.Empty, rawDataBuilder.ToString(), string.Empty);
                    }

                    var summary = ISoftbetParser.FromJsonObject(jObject);
                    param.UserName = param.UserName?.Trim();
                    if (!string.IsNullOrEmpty(param.UserName))
                    {
                        summary.BetHistoryList = summary.BetHistoryList
                            .Where(x => string.Equals(x.PlayerId, param.UserName, StringComparison.InvariantCultureIgnoreCase));
                    }

                    var xml = _isoftbetService.ConvertToXml(summary, item.Param);

                    convertedDataBuilder.AppendLine(xml);
                    convertedDataBuilder.AppendLine("-----------------------------------------");
                    convertedDataBuilder.AppendLine();
                }
            }
            catch (Exception ex)
            {
                logBuilder.AppendLine(ex.Message);
                logBuilder.AppendLine(ex.StackTrace);
                logBuilder.AppendLine(ex.Source);
            }

            return GetContentResult(logBuilder.ToString(), rawDataBuilder.ToString(), convertedDataBuilder.ToString());
        }

        [HttpPost]
        [Compress]
        public ActionResult GetReportData(ISoftbetRequestParam param)
        {
            var dataList = new List<ISoftbetBetHistory>();
            try
            {
                var isoftbetParam = CreateParam(param);
                var rawDataList = _isoftbetService.GetRawDataList(isoftbetParam);

                foreach (var item in rawDataList)
                {
                    var jObject = JObject.Parse(item.JsonData);
                    var status = Convert.ToString(jObject["status"]);
                    if (status != SuccessStatus)
                    {
                        return Content(JsonConvert.SerializeObject(new { Message = $"Status: {status}\n" + item.JsonData }), "application/json");
                    }

                    var summary = ISoftbetParser.FromJsonObject(jObject);
                    dataList.AddRange(summary.BetHistoryList);
                }
            }
            catch (Exception ex)
            {
                return Content(JsonConvert.SerializeObject(ex), "application/json");
            }

            var resultList = GetResultDataList(dataList);

            return Content(JsonConvert.SerializeObject(new
            {
                MainData = resultList,
                Total = new
                {
                    Turnover = resultList.Sum(p => p.Turnover),
                    Winloss = resultList.Sum(p => p.Winloss)
                }
            }), "application/json");
        }

        private IList<ISoftbetReport> GetResultDataList(List<ISoftbetBetHistory> betHistoryList)
        {
            var groupedData = betHistoryList.GroupBy(p => new { p.PlayerId, p.SessionId, p.GameId, p.RoundId });

            var result = new List<ISoftbetReport>();
            foreach (var groupedItem in groupedData)
            {
                var betItem = groupedItem.FirstOrDefault(p => p.Type.ToLowerInvariant() == "bet");
                var winItem = groupedItem.FirstOrDefault(p => p.Type.ToLowerInvariant() == "win");

                if (betItem != null && winItem != null)
                {
                    result.Add(new ISoftbetReport
                    {
                        PlayerId = groupedItem.Key.PlayerId,
                        SessionId = groupedItem.Key.SessionId,
                        GameId = groupedItem.Key.GameId,
                        RoundId = groupedItem.Key.RoundId,
                        Currency = winItem.Currency,
                        Time = winItem.Time,
                        Turnover = betItem.Amount,
                        Winloss = winItem.Amount - betItem.Amount
                    });
                }
            }

            return result;
        }

        private ActionResult GetContentResult(string log, string rawData, string convertedData)
        {
            return Content(JsonConvert.SerializeObject(new ThirdPartyProductViewModel
            {
                LogMessage = log,
                RawData = rawData,
                ConvertedData = convertedData
            }), "application/json");
        }

        private ISoftbetEntryParam CreateParam(ISoftbetRequestParam param)
        {
            var startTime = DateTime.ParseExact(param.StartTime, Format.RequestDateTime, CultureInfo.InvariantCulture);
            var endTime = DateTime.ParseExact(param.EndTime, Format.RequestDateTime, CultureInfo.InvariantCulture);

            if (param.IsGetByApiTimeZone)
            {
                startTime = DateTimeConverter.ToGMT(startTime, TimeZoneInfo.Local.Id);
                endTime = DateTimeConverter.ToGMT(endTime, TimeZoneInfo.Local.Id);
            }

            return new ISoftbetEntryParam
            {
                StartTime = startTime,
                EndTime = endTime,
                PageNumber = param.PageNumber
            };
        }
    }
}