using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Xml.Linq;
using Fanex.Affiliate.DataService.Importer.Constants;
using Fanex.Affiliate.DataService.Importer.Exceptions;
using Fanex.Affiliate.DataService.Importer.MetaModels.Opus;
using Fanex.Affiliate.DataService.Importer.Repository;
using Fanex.Affiliate.DataService.Importer.Utilities;

namespace Fanex.Affiliate.DataService.Importer
{
    public class OpusService : IOpusService
    {
        private const int DelayTimeMillisecond = 16000;

        private const int TimeRangeByMinute = 15;

        private const string TimeDataFormat = "yyyy-MM-dd HH:mm:ss";

        private const string SuccessStatus = "00";

        private readonly IAppSettings _appSetting;

        private readonly IThirdPartyProductRepository _thirdPartyProductRepository;

        public OpusService(IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
        {
            _appSetting = appSetting ?? new AppSettings();
            _thirdPartyProductRepository = thirdPartyProductRepository ?? new ThirdPartyProductRepository();
        }

        public IEnumerable<string> GetRawDataList(OpusParam param)
        {
            if (_appSetting.IsLocalTest)
            {
                return GetFakeData();
            }

            var result = new List<string>();
            var entryParam = BuildEntryParam(param);

            for (var start = entryParam.StartTime; DateTime.Compare(start, entryParam.EndTime) < 0; start = start.AddMinutes(TimeRangeByMinute))
            {
                var endTimeByBatch = start.AddMinutes(TimeRangeByMinute);
                var timeRangeParam = new OpusParam
                {
                    StartTime = start,
                    EndTime = DateTime.Compare(endTimeByBatch, entryParam.EndTime) > 0 ? entryParam.EndTime : endTimeByBatch,
                };

                var paramByBatch = BuildParam(timeRangeParam);

                if (DateTime.Compare(start, entryParam.StartTime) > 0)
                {
                    Thread.Sleep(DelayTimeMillisecond);
                }

                var rawData = GetRawData(paramByBatch);

                result.Add(rawData);
            }

            return result;
        }

        public IEnumerable<string> ConvertDataTimeZone(IEnumerable<string> rawDataList)
        {
            var result = new List<string>();

            foreach (var item in rawDataList)
            {
                var convertedData = ConvertTimeZoneForRawData(item);
                result.Add(convertedData);
            }

            return result;
        }

        public void Import(string xmlData)
        {
            _thirdPartyProductRepository.ImportData(Products.OpusProductId, xmlData);
        }

        public void Import(OpusParam param)
        {
            if (_appSetting.IsLocalTest)
            {
                var fakeDataList = GetFakeData();
                foreach (var item in fakeDataList)
                {
                    var convertedData = ConvertTimeZoneForRawData(item);
                    Import(convertedData);
                }
            }
            else
            {
                var entryParam = BuildEntryParam(param);

                for (var start = entryParam.StartTime; DateTime.Compare(start, entryParam.EndTime) < 0; start = start.AddMinutes(TimeRangeByMinute))
                {
                    var endTimeByBatch = start.AddMinutes(TimeRangeByMinute);
                    var timeRangeParam = new OpusParam
                    {
                        StartTime = start,
                        EndTime = DateTime.Compare(endTimeByBatch, entryParam.EndTime) > 0 ? entryParam.EndTime : endTimeByBatch,
                    };

                    var paramByBatch = BuildParam(timeRangeParam);

                    if (DateTime.Compare(start, entryParam.StartTime) > 0)
                    {
                        Thread.Sleep(DelayTimeMillisecond);
                    }

                    var rawData = GetRawData(paramByBatch);
                    var convertedData = ConvertTimeZoneForRawData(rawData);
                    Import(convertedData);
                }
            }
        }

        private OpusParam BuildEntryParam(OpusParam param)
        {
            var copiedParam = BuildParam(param);

            var startTime = new DateTime(copiedParam.StartTime.Year, copiedParam.StartTime.Month, copiedParam.StartTime.Day, copiedParam.StartTime.Hour, copiedParam.StartTime.Minute, 0, DateTimeKind.Local);
            var endTime = new DateTime(copiedParam.EndTime.Year, copiedParam.EndTime.Month, copiedParam.EndTime.Day, copiedParam.EndTime.Hour, copiedParam.EndTime.Minute, 0, DateTimeKind.Local);

            copiedParam.StartTime = DateTimeConverter.ToGMT(startTime.ToUniversalTime(), TimeZoneId.GmtPlus8);
            copiedParam.EndTime = DateTimeConverter.ToGMT(endTime.ToUniversalTime(), TimeZoneId.GmtPlus8);

            return copiedParam;
        }

        private OpusParam BuildParam(OpusParam param)
        {
            return new OpusParam
            {
                SecretKey = _appSetting.GetValue("OpusSecretKey"),
                OperatorId = _appSetting.GetValue("OpusOperatorId"),
                SiteCode = _appSetting.GetValue("OpusSiteCode"),
                ProductCode = _appSetting.GetValue("OpusProductCode"),
                StartTime = param.StartTime,
                EndTime = param.EndTime
            };
        }

        private string GetRawData(OpusParam param)
        {
            var apiUrl = _appSetting.GetValue("OpusApiUrl");
            var queryString = BuildQueryString(param);
            var url = $"{apiUrl}?{queryString}";

            using (var client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }

        private string BuildQueryString(OpusParam param)
        {
            var dateTimeFormat = _appSetting.GetValue("OpusFormatDateTime");
            var startTimeString = param.StartTime.ToString(dateTimeFormat);
            var endTimeString = param.EndTime.ToString(dateTimeFormat);

            return $"secret_key={param.SecretKey}&operator_id={param.OperatorId}&site_code={param.SiteCode}&product_code={param.ProductCode}&start_time={startTimeString}&end_time={endTimeString}";
        }

        private string ConvertTimeZoneForRawData(string rawData)
        {
            var xDoc = XDocument.Parse(rawData);
            var root = xDoc.Root;
            CheckResult(root);

            var betHistoryList = root.Element("bets").Elements("row");

            if (betHistoryList != null && betHistoryList.Any())
            {
                foreach (var betHistory in betHistoryList)
                {
                    UpdateTimeZoneData("transaction_date_time", betHistory);
                    UpdateTimeZoneData("stamp_date", betHistory);
                }
            }

            return xDoc.ToString();
        }

        private static void UpdateTimeZoneData(string elementName, XContainer element)
        {
            var timeString = element.Element(elementName)?.Value;
            if (!string.IsNullOrEmpty(timeString))
            {
                var time = DateTime.ParseExact(timeString, TimeDataFormat, CultureInfo.InvariantCulture);
                var convertedTime = DateTimeConverter.ConvertTime(time, TimeZoneId.GmtPlus8, TimeZoneId.GmtMinus4);
                element.Element(elementName).Value = convertedTime.ToString(TimeDataFormat);
            }
        }

        private static void CheckResult(XContainer transactionDetail)
        {
            var statusCode = transactionDetail.Element("status_code").Value;
            if (statusCode != SuccessStatus)
            {
                throw new ThirdPartyProductException($"Error: OPUS - ProductId: {Products.OpusProductId}\nResult:{transactionDetail.ToString()}");
            }
        }

        private IEnumerable<string> GetFakeData()
        {
            var result = new List<string>();
            var simulator = new DataSimulator();
            var folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"App_Data\\OpusData");
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