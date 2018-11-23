using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.Importer.Constants;
using Fanex.Affiliate.DataService.Importer.DTO.Quartz;
using Fanex.Affiliate.DataService.Importer.Exceptions;
using Fanex.Affiliate.DataService.Importer.MetaModels.Quartz;
using Fanex.Affiliate.DataService.Importer.Repository;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Newtonsoft.Json.Linq;

namespace Fanex.Affiliate.DataService.Importer
{
    public class QuartzService : IQuartzService
    {
        private const string SuccessStatus = "success";

        private readonly IAppSettings _appSetting;

        private readonly IThirdPartyProductRepository _thirdPartyProductRepository;

        public QuartzService(IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
        {
            _appSetting = appSetting ?? new AppSettings();
            _thirdPartyProductRepository = thirdPartyProductRepository ?? new ThirdPartyProductRepository();
        }

        public async Task<IEnumerable<QuartzApiParamResultPair>> GetRawDataList(QuartzEntryParam param)
        {
            var startTime = new DateTime(param.StartTime.Year, param.StartTime.Month, param.StartTime.Day, param.StartTime.Hour, param.StartTime.Minute, 0);
            var endTime = new DateTime(param.EndTime.Year, param.EndTime.Month, param.EndTime.Day, param.EndTime.Hour, param.EndTime.Minute, 0);

            var paramByTimeZone = new QuartzEntryParam
            {
                StartTime = startTime.ToUniversalTime(),
                EndTime = endTime.ToUniversalTime(),
                PageNumber = param.PageNumber
            };

            return await GetRawDataByBatch(paramByTimeZone);
        }

        public string ConvertToXml(QuartzSummary quartzSummary, QuartzApiParam param)
        {
            return "<ROOT>" + BuildRequestXml(param) + BuildResponseXml(quartzSummary) + "</ROOT>";
        }

        public void Import(string xmlData)
        {
            _thirdPartyProductRepository.ImportData(Products.EbetProductId, xmlData);
        }

        public async Task Import(QuartzEntryParam param)
        {
            var resultDataList = await GetRawDataList(param);

            foreach (var dataItem in resultDataList)
            {
                var jObject = JObject.Parse(dataItem.JsonData);
                var status = Convert.ToString(jObject["status"]);
                if (status != SuccessStatus)
                {
                    throw new ThirdPartyProductException($"Error: Quartz - ProductId: {Products.QuartzProductId}\n{dataItem.JsonData}");
                }

                var summary = QuartzParser.FromJsonObject(jObject);

                var xml = ConvertToXml(summary, dataItem.Param);
                Import(xml);
            }
        }

        private async Task<IEnumerable<QuartzApiParamResultPair>> GetRawDataByBatch(QuartzEntryParam param)
        {
            var result = new List<QuartzApiParamResultPair>();
            var taskList = new List<Task<IEnumerable<QuartzApiParamResultPair>>>();

            for (var requestTime = param.StartTime; DateTime.Compare(requestTime, param.EndTime) <= 0; requestTime = requestTime.AddMinutes(1))
            {
                var apiParam = new QuartzApiParam
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

        private async Task<IEnumerable<QuartzApiParamResultPair>> GetRawDataByPaging(QuartzApiParam apiParam)
        {
            var result = new List<QuartzApiParamResultPair>();
            var taskList = new List<Task<QuartzApiParamResultPair>>();
            var rawData = _appSetting.IsLocalTest ? await GetFakeData() : await GetRawDataAsync(apiParam);
            result.Add(new QuartzApiParamResultPair { Param = apiParam, JsonData = rawData });

            var jObject = JObject.Parse(rawData);
            var status = Convert.ToString(jObject["status"]);

            if (status == SuccessStatus)
            {
                var resultInfo = QuartzParser.GetResultInfo(jObject);
                for (var pageNumber = 2; pageNumber <= resultInfo.Pages; pageNumber++)
                {
                    var paramByPaging = new QuartzApiParam { RequestTime = apiParam.RequestTime, PageNumber = pageNumber };
                    taskList.Add(Task.Run(async () => new QuartzApiParamResultPair
                    {
                        Param = paramByPaging,
                        JsonData = await GetRawDataAsync(paramByPaging)
                    }));
                }

                result.AddRange(await Task.WhenAll(taskList));
            }

            return result;
        }

        private async Task<string> GetRawDataAsync(QuartzApiParam param)
        {
            var jsonBody = BuildBodyParam(param);
            var url = $"{_appSetting.GetValue("QuartzApiUrl")}?hash={HashMessage(jsonBody)}";

            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/json; charset=utf-8");

                return await client.UploadStringTaskAsync(url, jsonBody);
            }
        }

        private string BuildBodyParam(QuartzApiParam param)
        {
            var jAction = new JObject(
                new JProperty("command", "wagersFeed"),
                new JProperty("parameters", new JObject(
                        new JProperty("datetime", param.RequestTime.ToString(_appSetting.GetValue("QuartzFormatDateTime"))),
                        new JProperty("page", param.PageNumber)
                    ))
                );

            return new JObject(
                    new JProperty("operator", "12Bet"),
                    new JProperty("action", jAction)
                ).ToString();
        }

        private static string BuildRequestXml(QuartzApiParam param)
        {
            return "<REQUEST>" + $"<PARAM DATETIME=\"" + param.RequestTime + "\" PAGE =\"" + param.PageNumber + "\" />" + "</REQUEST>";
        }

        private static string BuildResponseXml(QuartzSummary quartzSummary)
        {
            var builder = new StringBuilder();
            var resultInfo = quartzSummary.ResultInfo;
            builder.AppendLine("<RESPONSE>");
            builder.AppendLine($"<RESULT DATETIME=\"{resultInfo.DateTime}\" PAGES=\"{resultInfo.Pages}\" CURRENTPAGE=\"{resultInfo.CurrentPage}\" TOTAL=\"{resultInfo.Total}\" />");

            builder.AppendLine("<BETHISTORYLIST>");
            foreach (var item in quartzSummary.BetHistoryList)
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
            var secretKey = _appSetting.GetValue("QuartzSecretKey");

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

        private async Task<string> GetFakeData()
        {
            var simulator = new DataSimulator();

            return await simulator.GetLocalData(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"App_Data\\QuartzData\\QuartzData.json"));
        }
    }
}