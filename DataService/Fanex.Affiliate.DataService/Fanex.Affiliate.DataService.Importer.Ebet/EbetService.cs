using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.Ebet.Utilities;
using Fanex.Affiliate.DataService.Importer.Ebet.MetaModels;
using Fanex.Affiliate.DataService.Importer.Ebet.DTO;
using Newtonsoft.Json;


namespace Fanex.Affiliate.DataService.Importer
{
    public class EbetService : IEbetService
    {
        private const int WaitingTimeInMillisecond = 90000;
        public readonly IAppSettings _appSetting;

        private readonly IThirdPartyProductRepository _thirdPartyProductRepository;

        public EbetService(IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
        {
            _appSetting = appSetting ?? new AppSettings();
            _thirdPartyProductRepository = thirdPartyProductRepository ?? new ThirdPartyProductRepository();
        }

        public string GetRawData(EbetParam param)
        {
            if (_appSetting.IsLocalTest)
            {
                var dataSimulator = new DataSimulator();
                var fileName = $"ebet_{param.CurrencyId}{(param.PageNumber > 1 ? "_" + param.PageNumber.ToString() : string.Empty)}.json";
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"App_Data\\EbetData\\{fileName}");

                return dataSimulator.GetLocalData(filePath);
            }

            var url = $"{Uri.UriSchemeHttp}{Uri.SchemeDelimiter}{param.ApiUrl}/api/userbethistory";
            var formBody = BuildFormBody(param);

            using (var webClient = new WebClient())
            {
                return Encoding.UTF8.GetString(webClient.UploadValues(url, "POST", formBody));
            }
        }

        public IEnumerable<EbetApiResult> GetResultDataList(EbetParam param)
        {
            var result = new List<EbetApiResult>();
            if (DateTime.Compare(param.StartTime, param.EndTime) < 0)
            {
                var convertedParam = BuildEntryParam(param);
                var firstJsonData = GetRawData(convertedParam);
                var resultInfo = EbetParser.GetEbetResultInfo(firstJsonData);

                if (resultInfo.RemainingVisits <= 1)
                {
                    Thread.Sleep(WaitingTimeInMillisecond);
                    var repulledJsonData = GetRawData(convertedParam);
                    result.Add(new EbetApiResult { Param = convertedParam, JsonData = repulledJsonData });
                }
                else
                {
                    result.Add(new EbetApiResult { Param = convertedParam, JsonData = firstJsonData });
                }

                if (resultInfo.Count > convertedParam.PageSize)
                {
                    var totalPage = (resultInfo.Count / convertedParam.PageSize) + (resultInfo.Count % convertedParam.PageSize == 0 ? 0 : 1);
                    for (var pageIndex = 2; pageIndex <= totalPage; pageIndex++)
                    {
                        var paramByBatch = BuildParamByPageNumber(convertedParam, pageIndex);
                        var dataByBatch = GetRawData(paramByBatch);
                        var resultInfoByBatch = EbetParser.GetEbetResultInfo(dataByBatch);

                        if (resultInfoByBatch.RemainingVisits <= 1)
                        {
                            Thread.Sleep(WaitingTimeInMillisecond);
                        }

                        result.Add(new EbetApiResult
                        {
                            Param = paramByBatch,
                            JsonData = dataByBatch
                        });
                    }
                }
            }

            return result;
        }

        public string ConvertToXml(EbetSummary summaryData, EbetParam param)
        {
            return "<ROOT>" + BuildRequestXml(param) + BuildResponseXml(summaryData) + "</ROOT>";
        }

        public void Import(string xmlData)
        {
            _thirdPartyProductRepository.ImportData(Products.EbetProductId, xmlData);
        }

        public void Import(EbetParam param)
        {
            try
            {
                var resultDataList = GetResultDataList(param);
                foreach (EbetApiResult dataItem in resultDataList)
                {
                    var ebetSummary = EbetParser.FromJson(dataItem.JsonData);
                    if (ebetSummary.ResultInfo.Status != (int)HttpStatusCode.OK)
                    {
                        var message = BuildErrorMessage(param, ebetSummary, dataItem);
                        throw new ThirdPartyProductException(message);
                    }

                    var xml = ConvertToXml(ebetSummary, dataItem.Param);
                    Import(xml);
                }
            }
            catch (Exception ex)
            {
                throw new ThirdPartyProductException(ex.Message, ex);
            }
        }

        private NameValueCollection BuildFormBody(EbetParam param)
        {
            var formatDateTime = _appSetting.GetValue("EbetFormatDateTime");

            return new NameValueCollection
            {
                { "Content-Type", "application/x-www-form-urlencoded" },
                { "startTimeStr", param.StartTime.ToString(formatDateTime) },
                { "endTimeStr", param.EndTime.ToString(formatDateTime) },
                { "channelId", param.ChannelId },
                { "subChannelId", param.SubChanelId.ToString() },
                { "pageNum", param.PageNumber.ToString() },
                { "pageSize", param.PageSize.ToString() },
                { "timestamp", param.TimeStamp.ToString() },
                { "signature", param.Signature },
                { "username", param.UserName }
            };
        }

        private static EbetParam BuildParamByPageNumber(EbetParam param, int pageNumber)
        {
            var timeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            var signature = RsaHelper.SignAndHash($"{param.UserName}{timeStamp}", param.PrivateKey);

            return new EbetParam
            {
                ApiUrl = param.ApiUrl,
                ChannelId = param.ChannelId,
                SubChanelId = param.SubChanelId,
                CurrencyId = param.CurrencyId,
                PageSize = param.PageSize,
                PrivateKey = param.PrivateKey,
                UserName = param.UserName,
                StartTime = param.StartTime,
                EndTime = param.EndTime,
                PageNumber = pageNumber,
                TimeStamp = timeStamp,
                Signature = signature
            };
        }

        private static EbetParam BuildEntryParam(EbetParam param)
        {
            var copiedParam = BuildParamByPageNumber(param, param.PageNumber);
            copiedParam.StartTime = DateTimeConverter.ToGMT(param.StartTime.ToUniversalTime(), TimeZoneId.GmtPlus8);
            copiedParam.EndTime = DateTimeConverter.ToGMT(param.EndTime.ToUniversalTime(), TimeZoneId.GmtPlus8);

            return copiedParam;
        }

        private static string BuildRequestXml(EbetParam param)
        {
            var attributes = "CURRENCYID=\"" + param.CurrencyId + "\" " +
                            "USERNAME=\"" + param.UserName + "\" " +
                            "STARTTIME=\"" + param.StartTime + "\" " +
                            "ENDTIME=\"" + param.EndTime + "\" " +
                            "PAGENUMBER=\"" + param.PageNumber + "\" " +
                            "PAGESIZE=\"" + param.PageSize + "\" " +
                            "TIMESTAMP=\"" + param.TimeStamp + "\" " +
                            "SIGNATURE=\"" + param.Signature + "\" ";

            return "<REQUEST>" + $"<PARAM {attributes} />" + "</REQUEST>";
        }

        private static string BuildResponseXml(EbetSummary ebetSummary)
        {
            var builder = new StringBuilder();
            var resultInfo = ebetSummary.ResultInfo;
            builder.AppendLine("<RESPONSE>");
            builder.AppendLine($"<RESULT COUNT=\"{resultInfo.Count}\" REMAININGVISITS=\"{resultInfo.RemainingVisits}\" STATUS=\"{resultInfo.Status}\" />");

            builder.AppendLine("<BETHISTORYLIST>");
            foreach (var item in ebetSummary.BetHistories)
            {
                var payoutTime = DateTimeConverter.ToGMT(DateTimeOffset.FromUnixTimeSeconds(item.PayoutUnixTime).DateTime, TimeZoneId.GmtPlus8);
                var createTime = DateTimeConverter.ToGMT(DateTimeOffset.FromUnixTimeSeconds(item.CreateUnixTime).DateTime, TimeZoneId.GmtPlus8);
                builder.AppendLine($"<BETHISTORY ID=\"{item.BetHistoryId}\" USERNAME=\"{item.UserName}\" BET=\"{item.Bet}\" PAYOUT=\"{item.Payout}\" PAYOUTTIME=\"{payoutTime.ToString()}\" CREATETIME=\"{createTime.ToString()}\" />");
            }
            builder.AppendLine("</BETHISTORYLIST>");

            builder.AppendLine("</RESPONSE>");

            return builder.ToString();
        }

        private static string BuildErrorMessage(EbetParam param, EbetSummary ebetSummary, EbetApiResult dataItem)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"Error: Ebet - ProductId: {Products.EbetProductId}");
            builder.AppendLine($"Param:{JsonConvert.SerializeObject(param)}");
            builder.AppendLine($"Result: {JsonConvert.SerializeObject(ebetSummary.ResultInfo)}");
            builder.AppendLine($"Data: {dataItem.JsonData}");

            return builder.ToString();
        }
    }
}