using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.Importer.Constants;
using Fanex.Affiliate.DataService.Importer.DTO.Ebet;
using Fanex.Affiliate.DataService.Importer.MetaModels.Ebet;
using Fanex.Affiliate.DataService.Importer.Repository;
using Fanex.Affiliate.DataService.Importer.Utilities;

namespace Fanex.Affiliate.DataService.Importer
{
    public class EbetService : IEbetService
    {
        public readonly IAppSettings _appSetting;

        private readonly IThirdPartyProductRepository _thirdPartyProductRepository;

        public EbetService(IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
        {
            _appSetting = appSetting ?? new AppSettings();
            _thirdPartyProductRepository = thirdPartyProductRepository ?? new ThirdPartyProductRepository();
        }

        public async Task<string> GetRawData(EbetParam param)
        {
            if (_appSetting.IsLocalTest)
            {
                var dataSimulator = new EbetDataSimulator();
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"App_Data\\EbetData\\ebet_{param.CurrencyId}.json");

                return await dataSimulator.GetLocalData(filePath);
            }

            var url = $"{Uri.UriSchemeHttp}{Uri.SchemeDelimiter}{param.ApiUrl}/api/userbethistory";
            var formBody = BuildFormBody(param);

            using (var webClient = new WebClient())
            {
                return Encoding.UTF8.GetString(await webClient.UploadValuesTaskAsync(url, "POST", formBody));
            }
        }

        public async Task<IEnumerable<EbetApiResult>> GetResultDataList(EbetParam param)
        {
            var result = new List<EbetApiResult>();
            var taskList = new List<Task<EbetApiResult>>();
            var convertedParam = BuildEntryParam(param);
            var firstJsonData = await GetRawData(convertedParam);
            var resultInfo = EbetParser.GetEbetResultInfo(firstJsonData);

            result.Add(new EbetApiResult { Param = convertedParam, JsonData = firstJsonData });

            if (resultInfo.Count > convertedParam.PageSize)
            {
                var totalPage = (resultInfo.Count / convertedParam.PageSize) + (resultInfo.Count % convertedParam.PageSize == 0 ? 0 : 1);
                for (var pageIndex = 2; pageIndex <= totalPage; pageIndex++)
                {
                    var paramByBatch = BuildParamByPageNumber(convertedParam, pageIndex);
                    taskList.Add(Task.Run(async () => new EbetApiResult
                    {
                        Param = paramByBatch,
                        JsonData = await GetRawData(paramByBatch)
                    }));
                }

                var taskResultList = await Task.WhenAll(taskList);
                foreach (var resultByBatch in taskResultList)
                {
                    result.Add(resultByBatch);
                }
            }

            return result;
        }

        public string ConvertToXml(EbetSummary summaryData, EbetParam param)
        {
            return "<Root>" + BuildRequestXml(param) + BuildResponseXml(summaryData) + "</Root>";
        }

        public void Import(string xmlData)
        {
            _thirdPartyProductRepository.ImportData(Products.EbetProductId, xmlData);
        }

        public async Task Import(EbetParam param)
        {
            var resultDataList = await GetResultDataList(param);

            foreach (var dataItem in resultDataList)
            {
                var ebetSummary = EbetParser.FromJson(dataItem.JsonData);
                var xml = ConvertToXml(ebetSummary, dataItem.Param);
                Import(xml);
            }
        }

        private NameValueCollection BuildFormBody(EbetParam param)
        {
            return new NameValueCollection
            {
                { "Content-Type", "application/x-www-form-urlencoded" },
                { "startTimeStr", param.StartTime.ToString(_appSetting.EbetFormatDateTime) },
                { "endTimeStr", param.EndTime.ToString(_appSetting.EbetFormatDateTime) },
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
                PageNumber = pageNumber
            };
        }

        private static EbetParam BuildEntryParam(EbetParam param)
        {
            var copiedParam = BuildParamByPageNumber(param, param.PageNumber);
            copiedParam.StartTime = DateTimeConverter.ToGMT(param.StartTime, TimeZoneId.GmtPlus8);
            copiedParam.EndTime = DateTimeConverter.ToGMT(param.EndTime, TimeZoneId.GmtPlus8);

            copiedParam.TimeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            copiedParam.Signature = RsaHelper.SignAndHash($"{copiedParam.UserName}{copiedParam.TimeStamp}", copiedParam.PrivateKey);

            return copiedParam;
        }

        private static string BuildRequestXml(EbetParam param)
        {
            var attributes = "currencyid=\"" + param.CurrencyId + "\" " +
                            "username=\"" + param.UserName + "\" " +
                            "starttime=\"" + param.StartTime.ToString() + "\" " +
                            "endtime=\"" + param.EndTime.ToString() + "\" " +
                            "pagenumber=\"" + param.PageNumber + "\" " +
                            "pagesize=\"" + param.PageSize + "\" " +
                            "timestamp=\"" + param.TimeStamp + "\" " +
                            "signature=\"" + param.Signature + "\" ";

            return "<Request>" + $"<Param {attributes} />" + "</Request>";
        }

        private static string BuildResponseXml(EbetSummary ebetSummary)
        {
            var builder = new StringBuilder();
            var resultInfo = ebetSummary.ResultInfo;
            builder.AppendLine("<Response>");
            builder.AppendLine($"<Result count=\"{resultInfo.Count}\" remainingvisits=\"{resultInfo.RemainingVisits}\" status=\"{resultInfo.Status}\" />");

            builder.AppendLine("<BetHistoryList>");
            foreach (var item in ebetSummary.BetHistories)
            {
                var payoutTime = DateTimeOffset.FromUnixTimeSeconds(item.PayoutUnixTime).DateTime;
                var convertedTime = DateTimeConverter.ConvertTime(payoutTime, TimeZoneId.GmtPlus8, TimeZoneId.Gmt4);
                builder.AppendLine($"<Bethistory id=\"{item.BetHistoryId}\" username=\"{item.UserName}\" bet=\"{item.Bet}\" payout=\"{item.Payout}\" payouttime=\"{convertedTime.ToString()}\" />");
            }
            builder.AppendLine("</BetHistoryList>");

            builder.AppendLine("</Response>");

            return builder.ToString();
        }
    }
}