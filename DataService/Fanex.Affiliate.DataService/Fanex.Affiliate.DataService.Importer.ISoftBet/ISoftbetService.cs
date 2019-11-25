using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.ISoftbet.DTO;
using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.ISoftbet.MetaModels;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.ISoftbet.Utilities;
using Newtonsoft.Json.Linq;

namespace Fanex.Affiliate.DataService.Importer
{
    public class ISoftbetService : IISoftbetService
    {
        private const string SuccessStatus = "success";
        private const int DelayTimeInMinute = 5;

        private readonly IAppSettings _appSetting;

        private readonly IThirdPartyProductRepository _thirdPartyProductRepository;

        public ISoftbetService(IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
        {
            _appSetting = appSetting ?? new AppSettings();
            _thirdPartyProductRepository = thirdPartyProductRepository ?? new ThirdPartyProductRepository();
        }

        public IEnumerable<ISoftbetApiParamResultPair> GetRawDataList(ISoftbetEntryParam param)
        {
            var startTime = CreateTimeByMinute(param.StartTime);
            var endTime = CreateTimeByMinute(param.EndTime);

            var paramByTimeZone = new ISoftbetEntryParam
            {
                StartTime = startTime.ToUniversalTime(),
                EndTime = endTime.ToUniversalTime(),
                PageNumber = param.PageNumber,
                JobType = param.JobType
            };

            if (_appSetting.IsLocalTest)
            {
                return GetFakeData().Select(x => new ISoftbetApiParamResultPair
                {
                    JsonData = x,
                    Param = new ISoftbetApiParam { PageNumber = paramByTimeZone.PageNumber, RequestTime = paramByTimeZone.StartTime }
                });
            }

            return GetRawDataByBatch(paramByTimeZone);
        }

        public string ConvertToXml(ISoftbetSummary isoftbetSummary, ISoftbetApiParam param)
        {
            return "<ROOT>" + BuildRequestXml(param) + BuildResponseXml(isoftbetSummary) + "</ROOT>";
        }

        public void Import(string xmlData, byte jobType)
        {
            _thirdPartyProductRepository.ImportData(Products.ISoftbetProductId, xmlData, jobType);
        }

        public void Import(ISoftbetEntryParam param)
        {
            var startTime = CreateTimeByMinute(param.StartTime);
            var endTime = CreateTimeByMinute(param.EndTime);

            var paramByTimeZone = new ISoftbetEntryParam
            {
                StartTime = startTime.ToUniversalTime(),
                EndTime = endTime.ToUniversalTime(),
                PageNumber = param.PageNumber,
                JobType = param.JobType
            };

            if (_appSetting.IsLocalTest)
            {
                var resultList = GetFakeData().Select(x => new ISoftbetApiParamResultPair
                {
                    JsonData = x,
                    Param = new ISoftbetApiParam { PageNumber = paramByTimeZone.PageNumber, RequestTime = paramByTimeZone.StartTime }
                });

                foreach (var item in resultList)
                {
                    ImportDataByResult(item, param.JobType);
                }
            }
            else
            {
                ImportDataByBatch(paramByTimeZone);
            }
        }

        private void ImportDataByBatch(ISoftbetEntryParam param)
        {
            for (var requestTime = param.StartTime; DateTime.Compare(requestTime, param.EndTime) < 0; requestTime = requestTime.AddMinutes(1))
            {
                var apiParam = new ISoftbetApiParam
                {
                    RequestTime = requestTime,
                    PageNumber = param.PageNumber
                };

                var rawDataListByPaging = GetRawDataByPaging(apiParam);

                foreach (var item in rawDataListByPaging)
                {
                    ImportDataByResult(item, param.JobType);
                }
            }
        }

        private void ImportDataByResult(ISoftbetApiParamResultPair result, byte jobType)
        {
            var jObject = JObject.Parse(result.JsonData);
            var status = Convert.ToString(jObject["status"]);
            if (status != SuccessStatus)
            {
                throw new ThirdPartyProductException($"Error: ISoftbet - ProductId: {Products.ISoftbetProductId}\n{result.JsonData}");
            }

            var summary = ISoftbetParser.FromJsonObject(jObject);

            var xml = ConvertToXml(summary, result.Param);
            Import(xml, jobType);
        }

        private IEnumerable<ISoftbetApiParamResultPair> GetRawDataByBatch(ISoftbetEntryParam param)
        {
            var result = new List<ISoftbetApiParamResultPair>();

            for (var requestTime = param.StartTime; DateTime.Compare(requestTime, param.EndTime) < 0; requestTime = requestTime.AddMinutes(1))
            {
                var apiParam = new ISoftbetApiParam
                {
                    RequestTime = requestTime,
                    PageNumber = param.PageNumber
                };

                var rawDataListByPaging = GetRawDataByPaging(apiParam);
                result.AddRange(rawDataListByPaging);
            }

            return result;
        }

        private IEnumerable<ISoftbetApiParamResultPair> GetRawDataByPaging(ISoftbetApiParam apiParam)
        {
            var result = new List<ISoftbetApiParamResultPair>();

            var rawData = GetRawData(apiParam);
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
                        result.Add(new ISoftbetApiParamResultPair
                        {
                            Param = paramByPaging,
                            JsonData = GetRawData(paramByPaging)
                        });
                    }
                }
            }

            return result;
        }

        private string GetRawData(ISoftbetApiParam param)
        {
            var jsonBody = BuildBodyParam(param);
            var url = $"{_appSetting.GetValue("ISoftbetApiUrl")}?hash={HashMessage(jsonBody)}";

            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/json; charset=utf-8");

                return client.UploadString(url, jsonBody);
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

        private static string BuildResponseXml(ISoftbetSummary isoftbetSummary)
        {
            var builder = new StringBuilder();
            var resultInfo = isoftbetSummary.ResultInfo;
            builder.AppendLine("<RESPONSE>");
            builder.AppendLine($"<RESULT DATETIME=\"{resultInfo.DateTime}\" PAGES=\"{resultInfo.Pages}\" CURRENTPAGE=\"{resultInfo.CurrentPage}\" TOTAL=\"{resultInfo.Total}\" />");

            builder.AppendLine("<BETHISTORYLIST>");
            foreach (var item in isoftbetSummary.BetHistoryList)
            {
                var attributes = "PLAYERID=\"" + item.PlayerId + "\" " +
                            "CURRENCY=\"" + item.Currency + "\" " +
                            "SESSIONID=\"" + item.SessionId + "\" " +
                            "GAMEID=\"" + item.GameId + "\" " +
                            "ROUNDID=\"" + item.RoundId + "\" " +
                            "TRANSACTIONID=\"" + item.TransactionId + "\" " +
                            "TYPE=\"" + item.Type + "\" " +
                            "TIME=\"" + item.Time.ToString() + "\" " +
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

            using (var hmacmd5 = new HMACSHA256(keyByte))
            {
                var messageBytes = encoding.GetBytes(message);
                var hashmessage = hmacmd5.ComputeHash(messageBytes);

                return ByteToString(hashmessage).ToLower();
            }
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

        private DateTime CreateTimeByMinute(DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0).AddMinutes(-DelayTimeInMinute);
        }

        private IEnumerable<string> GetFakeData()
        {
            var result = new List<string>();
            var simulator = new DataSimulator();
            var folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"App_Data\\ISoftbetData");
            var directory = new DirectoryInfo(folderPath);
            var fileList = directory.GetFiles().Select(f => f.FullName);

            foreach (var file in fileList)
            {
                var data = simulator.GetLocalData(file);
                result.Add(data);
            }

            return result;
        }
    }
}