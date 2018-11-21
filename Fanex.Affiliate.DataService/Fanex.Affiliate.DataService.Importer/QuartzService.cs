using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.Importer.Constants;
using Fanex.Affiliate.DataService.Importer.DTO.Quartz;
using Fanex.Affiliate.DataService.Importer.MetaModels.Quartz;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Newtonsoft.Json.Linq;

namespace Fanex.Affiliate.DataService.Importer
{
    public class QuartzService : IQuartzService
    {
        private readonly IAppSettings _appSetting;

        public QuartzService(IAppSettings appSetting = null)
        {
            _appSetting = appSetting ?? new AppSettings();
        }

        public async Task<IEnumerable<QuartzApiParamResultPair>> GetRawDataList(QuartzParam param)
        {
            var result = new List<QuartzApiParamResultPair>();
            var taskList = new List<Task<QuartzApiParamResultPair>>();
            var rawData = await GetRawDataAsync(param);
            result.Add(new QuartzApiParamResultPair { Param = param, JsonData = rawData });

            var jObject = JObject.Parse(rawData);
            var status = Convert.ToString(jObject["status"]);

            if (status == "success")
            {
                var resultInfo = QuartzParser.GetResultInfo(jObject);
                for (var pageNumber = 2; pageNumber <= resultInfo.Pages; pageNumber++)
                {
                    var paramByPaging = new QuartzParam { RequestTime = param.RequestTime, PageNumber = pageNumber };
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

        public async Task<string> GetRawDataAsync(QuartzParam param)
        {
            var jsonBody = BuildBodyParam(param);
            var url = $"{_appSetting.GetValue("QuartzApiUrl")}?hash={HashMessage(jsonBody)}";

            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/json; charset=utf-8");

                return await client.UploadStringTaskAsync(url, jsonBody);
            }
        }

        public string ConvertToXml(QuartzSummary quartzSummary, QuartzParam param)
        {
            return "<ROOT>" + BuildRequestXml(param) + BuildResponseXml(quartzSummary) + "</ROOT>";
        }

        private string BuildBodyParam(QuartzParam param)
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

        private static string BuildRequestXml(QuartzParam param)
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
    }
}