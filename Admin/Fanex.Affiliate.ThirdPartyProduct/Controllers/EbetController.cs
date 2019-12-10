using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.ThirdPartyProduct.Attributes;
using Fanex.Affiliate.ThirdPartyProduct.Constants;
using Fanex.Affiliate.ThirdPartyProduct.MetaModels;
using Fanex.Affiliate.ThirdPartyProduct.ViewModels;
using Newtonsoft.Json;
using Fanex.Affiliate.DataService.Importer.Ebet.Utilities;
using Fanex.Affiliate.DataService.Importer.Ebet.DTO;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Ebet.MetaModels;

namespace Fanex.Affiliate.ThirdPartyProduct.Controllers
{
    [Authorize]
    [FunctionAuthorize]
    public class EbetController : Controller
    {
        private readonly IEbetConfigHelper _ebetConfigHelper;
        private readonly IEbetService _ebetService;

        public EbetController()
        {
            _ebetConfigHelper = new EbetConfigHelper();
            _ebetService = new EbetService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/ThirdPartyProduct/Ebet/Index.cshtml");
        }

        [HttpPost]
        public ActionResult Index(EbetRequestParam param)
        {
            var logBuilder = new StringBuilder();
            var rawDataBuilder = new StringBuilder();
            var convertedDataBuilder = new StringBuilder();
            try
            {
                var resultList = new List<EbetApiResult>();
                var configList = _ebetConfigHelper.GetConfigList()
                                    .Where(p => string.IsNullOrEmpty(param.CurrencyId) || string.Equals(p.CurrencyID, param.CurrencyId));

                var startTime = DateTime.ParseExact(param.StartTime, Format.RequestDateTime, CultureInfo.InvariantCulture);
                var endTime = DateTime.ParseExact(param.EndTime, Format.RequestDateTime, CultureInfo.InvariantCulture);

                if (param.IsGetByApiTimeZone)
                {
                    startTime = DateTimeConverter.ConvertTime(startTime, TimeZoneId.GmtPlus8, TimeZoneInfo.Local.Id);
                    endTime = DateTimeConverter.ConvertTime(endTime, TimeZoneId.GmtPlus8, TimeZoneInfo.Local.Id);
                }

                foreach (var config in configList)
                {
                    var ebetParam = new EbetParam
                    {
                        UserName = param.UserName ?? string.Empty,
                        StartTime = startTime,
                        EndTime = endTime,
                        ChannelId = config.ChannelId,
                        ApiUrl = config.ApiServerHost,
                        PrivateKey = config.PrivateKey,
                        CurrencyId = config.CurrencyID,
                        PageNumber = param.PageNumber,
                        PageSize = param.PageSize,
                    };

                    resultList.AddRange(_ebetService.GetResultDataList(ebetParam));
                }

                foreach (var resultItem in resultList)
                {
                    var apiParam = resultItem.Param;
                    rawDataBuilder.AppendLine($"Currency: {apiParam.CurrencyId}, ChanelId: {apiParam.ChannelId}, ApiUrl: {apiParam.ApiUrl}");
                    var paramInfo = $"UserName: {apiParam.UserName}, StartTime: {apiParam.StartTime}, EndTime: {apiParam.EndTime}, PageSize: {apiParam.PageSize}, PageNumber: {apiParam.PageNumber}";
                    rawDataBuilder.AppendLine(paramInfo);
                    rawDataBuilder.AppendLine(resultItem.JsonData);
                    rawDataBuilder.AppendLine("------------------------------------------------------");
                    rawDataBuilder.AppendLine();

                    var ebetSummary = EbetParser.FromJson(resultItem.JsonData);
                    var xml = _ebetService.ConvertToXml(ebetSummary, apiParam);

                    convertedDataBuilder.AppendLine(xml);
                    convertedDataBuilder.AppendLine("------------------------------------------------------");
                    convertedDataBuilder.AppendLine();
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
