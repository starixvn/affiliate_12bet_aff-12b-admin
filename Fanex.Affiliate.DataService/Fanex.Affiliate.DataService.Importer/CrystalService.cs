using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using Fanex.Affiliate.DataService.Importer.Constants;
using Fanex.Affiliate.DataService.Importer.Exceptions;
using Fanex.Affiliate.DataService.Importer.MetaModels.Crystal;
using Fanex.Affiliate.DataService.Importer.Repository;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Newtonsoft.Json;

namespace Fanex.Affiliate.DataService.Importer
{
    public class CrystalService : ICrystalService
    {
        private const int DelayTimeMillisecond = 15000;
        private const string TimeDataFormat = "dd-MM-yyyy H:mm:ss";

        private readonly IAppSettings _appSetting;
        private readonly IThirdPartyProductRepository _thirdPartyProductRepository;

        public CrystalService(IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
        {
            _appSetting = appSetting ?? new AppSettings();
            _thirdPartyProductRepository = thirdPartyProductRepository ?? new ThirdPartyProductRepository();
        }

        public IEnumerable<string> GetRawDataList(CrystalParam param)
        {
            var paramByTimeZone = BuildParamByTimeZone(param);

            return GetRawDataListByBatch(paramByTimeZone);
        }

        public IEnumerable<string> ConvertDataTimeZone(IEnumerable<string> rawDataList)
        {
            var result = new List<string>();
            foreach (var item in rawDataList)
            {
                var xDoc = XDocument.Parse(item);
                var betHistoryList = xDoc.Root.Elements("history");

                if (betHistoryList != null && betHistoryList.Any())
                {
                    foreach (var betHistory in betHistoryList)
                    {
                        var changeTimeString = betHistory.Attribute("changeTime")?.Value;
                        if (!string.IsNullOrEmpty(changeTimeString))
                        {
                            var changeTime = DateTime.ParseExact(changeTimeString, TimeDataFormat, CultureInfo.InvariantCulture);
                            var convertedTime = DateTimeConverter.ConvertTime(changeTime, TimeZoneId.GmtPlus8, TimeZoneId.GmtMinus4);
                            betHistory.Attribute("changeTime").Value = convertedTime.ToString(TimeDataFormat);
                        }

                    }
                }

                result.Add(xDoc.ToString());
            }

            return result;
        }

        public void Import(CrystalParam param)
        {
            var rawDataList = GetRawDataList(param);
            var convertedDataTimeZone = ConvertDataTimeZone(rawDataList);
            Import(convertedDataTimeZone);
        }

        public void Import(IEnumerable<string> dataList)
        {
            foreach (var item in dataList)
            {
                _thirdPartyProductRepository.ImportData(Products.CrystalProductId, item);
            }
        }

        private IEnumerable<string> GetRawDataListByBatch(CrystalParam param)
        {
            var result = new List<string>();
            var startDate = new DateTime(param.StartTime.Year, param.StartTime.Month, param.StartTime.Day, param.StartTime.Hour, 0, 0);
            var endDate = new DateTime(param.EndTime.Year, param.EndTime.Month, param.EndTime.Day, param.EndTime.Hour, 0, 0);

            for (var start = startDate; DateTime.Compare(start, endDate) < 0; start = start.AddHours(1))
            {
                var endDateByBatch = start.AddHours(1);
                var paramByBatch = BuildParam(
                    new CrystalParam
                    {
                        StartTime = start,
                        EndTime = DateTime.Compare(endDateByBatch, endDate) > 0 ? endDate : endDateByBatch,
                        PageNumber = param.PageNumber,
                        PageSize = param.PageSize
                    },
                    param.PageNumber);

                var dataListByBatch = GetRawDataListByPaging(paramByBatch);
                result.AddRange(dataListByBatch);
            }

            return result;
        }

        private IEnumerable<string> GetRawDataListByPaging(CrystalParam paramByBatch)
        {
            var result = new List<string>();
            var firstRawData = GetRawData(paramByBatch);
            var xDocument = XDocument.Parse(firstRawData);

            var hasError = string.Equals("error", xDocument.Root.Name.LocalName, StringComparison.InvariantCultureIgnoreCase);
            if (hasError)
            {
                throw new ThirdPartyProductException($"Crystal Product Error\nParam:{JsonConvert.SerializeObject(paramByBatch)}\nResult:{firstRawData}");
            }

            result.Add(firstRawData);
            var totalPages = Convert.ToInt32(xDocument.Root.Attribute("totalPage")?.Value);
            if (totalPages > 1 && paramByBatch.PageNumber < totalPages)
            {
                var paramByPaging = BuildParam(paramByBatch, paramByBatch.PageNumber + 1);
                result.AddRange(GetRawDataListByPaging(paramByPaging));
            }

            return result;
        }

        private string GetRawData(CrystalParam param)
        {
            var apiUrl = BuildApiUrl(param);
            Task.Delay(DelayTimeMillisecond).Wait();

            return GetDataString(apiUrl);
        }

        private CrystalParam BuildParamByTimeZone(CrystalParam entryParam)
        {
            var copiedParam = BuildParam(entryParam, entryParam.PageNumber);
            copiedParam.StartTime = DateTimeConverter.ToGMT(entryParam.StartTime.ToUniversalTime(), TimeZoneId.GmtPlus8);
            copiedParam.EndTime = DateTimeConverter.ToGMT(entryParam.EndTime.ToUniversalTime(), TimeZoneId.GmtPlus8);

            return copiedParam;
        }

        private CrystalParam BuildParam(CrystalParam param, int pageNum)
        {
            return new CrystalParam
            {
                StartTime = param.StartTime,
                EndTime = param.EndTime,
                PageNumber = pageNum,
                PageSize = param.PageSize,
                Code = _appSetting.GetValue("CrystalCode"),
                Password = _appSetting.GetValue("CrystalPassword"),
                Type = Convert.ToByte(_appSetting.GetValue("CrystalType"))
            };
        }

        private string BuildApiUrl(CrystalParam param)
        {
            var startTime = param.StartTime.ToString(_appSetting.GetValue("CrystalFormatDateTime"));
            var endTime = param.EndTime.ToString(_appSetting.GetValue("CrystalFormatDateTime"));

            return _appSetting.GetValue("CrystalApiUrl") + $"?opCode={param.Code}&s={startTime}&e={endTime}&pageNum={param.PageNumber}&pageSize={param.PageSize}&pwd={param.Password}&type={param.Type}";
        }

        private string GetDataString(string url)
        {
            using (var client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }
    }
}
