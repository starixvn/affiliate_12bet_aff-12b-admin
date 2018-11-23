using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.Importer.Constants;
using Fanex.Affiliate.DataService.Importer.DTO.ISoftbet;
using Fanex.Affiliate.DataService.Importer.Exceptions;
using Fanex.Affiliate.DataService.Importer.MetaModels.ISoftbet;
using Fanex.Affiliate.DataService.Importer.Repository;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Newtonsoft.Json.Linq;

namespace Fanex.Affiliate.DataService.Importer
{
    public class ISoftbetService : IISoftbetService
    {
        private const string SuccessStatus = "success";

        private readonly IAppSettings _appSetting;

        private readonly IThirdPartyProductRepository _thirdPartyProductRepository;

        public ISoftbetService(IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
        {
            _appSetting = appSetting ?? new AppSettings();
            _thirdPartyProductRepository = thirdPartyProductRepository ?? new ThirdPartyProductRepository();
        }

        public async Task<IEnumerable<ISoftbetApiParamResultPair>> GetRawDataList(ISoftbetEntryParam param)
        {
            var startTime = new DateTime(param.StartTime.Year, param.StartTime.Month, param.StartTime.Day, param.StartTime.Hour, param.StartTime.Minute, 0);
            var endTime = new DateTime(param.EndTime.Year, param.EndTime.Month, param.EndTime.Day, param.EndTime.Hour, param.EndTime.Minute, 0);

            var paramByTimeZone = new ISoftbetEntryParam
            {
                StartTime = startTime.ToUniversalTime(),
                EndTime = endTime.ToUniversalTime(),
                PageNumber = param.PageNumber
            };

            if (_appSetting.IsLocalTest)
            {
                return (await GetFakeData()).Select(x => new ISoftbetApiParamResultPair
                {
                    JsonData = x,
                    Param = new ISoftbetApiParam { PageNumber = paramByTimeZone.PageNumber, RequestTime = paramByTimeZone.StartTime }
                });
            }

            return await GetRawDataByBatch(paramByTimeZone);
        }

        public string ConvertToXml(ISoftbetSummary ISoftbetSummary, ISoftbetApiParam param)
        {
            return "<ROOT>" + BuildRequestXml(param) + BuildResponseXml(ISoftbetSummary) + "</ROOT>";
        }

        public void Import(string xmlData)
        {
            _thirdPartyProductRepository.ImportData(Products.EbetProductId, xmlData);
        }

        public async Task Import(ISoftbetEntryParam param)
        {
            var resultDataList = await GetRawDataList(param);

            foreach (var dataItem in resultDataList)
            {
                var jObject = JObject.Parse(dataItem.JsonData);
                var status = Convert.ToString(jObject["status"]);
                if (status != SuccessStatus)
                {
                    throw new ThirdPartyProductException($"Error: ISoftbet - ProductId: {Products.ISoftbetProductId}\n{dataItem.JsonData}");
                }

                var summary = ISoftbetParser.FromJsonObject(jObject);

                var xml = ConvertToXml(summary, dataItem.Param);
                Import(xml);
            }
        }

        private async Task<IEnumerable<ISoftbetApiParamResultPair>> GetRawDataByBatch(ISoftbetEntryParam param)
        {
            var result = new List<ISoftbetApiParamResultPair>();
            var taskList = new List<Task<IEnumerable<ISoftbetApiParamResultPair>>>();

            for (var requestTime = param.StartTime; DateTime.Compare(requestTime, param.EndTime) < 0; requestTime = requestTime.AddMinutes(1))
            {
                var apiParam = new ISoftbetApiParam
                {
                    RequestTime = requestTime,
                    PageNumber = param.PageNumber
                };

                taskList.Add(GetRawDataByPaging(apiParam));
            }

            var resultByBatchList = await Task.WhenAll(taskList);
            foreach (var resultByBatch in resultByBatchList)
            {
                result.AddRange(resultByBatch);
            }

            return result;
        }

        private async Task<IEnumerable<ISoftbetApiParamResultPair>> GetRawDataByPaging(ISoftbetApiParam apiParam)
        {
            var result = new List<ISoftbetApiParamResultPair>();
            var taskList = new List<Task<ISoftbetApiParamResultPair>>();

            var rawData = await GetRawDataAsync(apiParam);
            result.Add(new ISoftbetApiParamResultPair { Param = apiParam, JsonData = rawData });

            var jObject = JObject.Parse(rawData);
            var status = Convert.ToString(jObject["status"]);

            if (status == SuccessStatus)
            {
                var resultInfo = ISoftbetParser.GetResultInfo(jObject);
                if (resultInfo.Pages > 1)
                {
                    for (var pageNumber = 2; pageNumber <= resultInfo.Pages; pageNumber++)
                    {
                        var paramByPaging = new ISoftbetApiParam { RequestTime = apiParam.RequestTime, PageNumber = pageNumber };
                        taskList.Add(Task.Run(async () => new ISoftbetApiParamResultPair
                        {
                            Param = paramByPaging,
                            JsonData = await GetRawDataAsync(paramByPaging)
                        }));
                    }

                    result.AddRange(await Task.WhenAll(taskList));
                }
            }

            return result;
        }

        private async Task<string> GetRawDataAsync(ISoftbetApiParam param)
        {
            var jsonBody = BuildBodyParam(param);
            var url = $"{_appSetting.GetValue("ISoftbetApiUrl")}?hash={HashMessage(jsonBody)}";

            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/json; charset=utf-8");

                return await client.UploadStringTaskAsync(url, jsonBody);
            }
        }

        private string BuildBodyParam(ISoftbetApiParam param)
        {
            var jAction = new JObject(
                new JProperty("command", "wagersFeed"),
                new JProperty("parameters", new JObject(
                        new JProperty("datetime", param.RequestTime.ToString(_appSetting.GetValue("ISoftbetFormatDateTime"))),
                        new JProperty("page", param.PageNumber)
                    ))
                );

            return new JObject(
                    new JProperty("operator", "12Bet"),
                    new JProperty("action", jAction)
                ).ToString();
        }

        private static string BuildRequestXml(ISoftbetApiParam param)
        {
            return "<REQUEST>" + $"<PARAM DATETIME=\"" + param.RequestTime + "\" PAGE =\"" + param.PageNumber + "\" />" + "</REQUEST>";
        }

        private static string BuildResponseXml(ISoftbetSummary ISoftbetSummary)
        {
            var builder = new StringBuilder();
            var resultInfo = ISoftbetSummary.ResultInfo;
            builder.AppendLine("<RESPONSE>");
            builder.AppendLine($"<RESULT DATETIME=\"{resultInfo.DateTime}\" PAGES=\"{resultInfo.Pages}\" CURRENTPAGE=\"{resultInfo.CurrentPage}\" TOTAL=\"{resultInfo.Total}\" />");

            builder.AppendLine("<BETHISTORYLIST>");
            foreach (var item in ISoftbetSummary.BetHistoryList)
            {
                var convertedTime = DateTimeConverter.ToGMT(item.Time, TimeZoneId.GmtMinus4);
                var attributes = "PLAYERID=\"" + item.PlayerId + "\" " +
                            "CURRENCY=\"" + item.Currency + "\" " +
                            "SESSIONID=\"" + item.SessionId + "\" " +
                            "GAMEID=\"" + item.GameId + "\" " +
                            "ROUNDID=\"" + item.RoundId + "\" " +
                            "TRANSACTIONID=\"" + item.TransactionId + "\" " +
                            "TYPE=\"" + item.Type + "\" " +
                            "TIME=\"" + convertedTime.ToString() + "\" " +
                            "AMOUNT=\"" + item.Amount.ToString() + "\"";

                builder.AppendLine($"<BETHISTORY {attributes} />");
            }
            builder.AppendLine("</BETHISTORYLIST>");

            builder.AppendLine("</RESPONSE>");

            return builder.ToString();
        }

        private string HashMessage(string message)
        {
            var secretKey = _appSetting.GetValue("ISoftbetSecretKey");

            var encoding = new ASCIIEncoding();
            var keyByte = encoding.GetBytes(secretKey);

            var hmacmd5 = new HMACSHA256(keyByte);
            var messageBytes = encoding.GetBytes(message);
            var hashmessage = hmacmd5.ComputeHash(messageBytes);

            return ByteToString(hashmessage).ToLower();
        }

        private static string ByteToString(byte[] buff)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < buff.Length; i++)
            {
                builder.Append(buff[i].ToString("X2")); // hex format
            }

            return builder.ToString();
        }

        private async Task<IEnumerable<string>> GetFakeData()
        {
            var taskList = new List<Task<string>>();
            var simulator = new DataSimulator();
            for (var i = 1; i <= 6; i++)
            {
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"App_Data\\ISoftbetData\\isoftbet_{i}.json");
                taskList.Add(simulator.GetLocalData(filePath));
            }

            return await Task.WhenAll(taskList);
        }
    }
}