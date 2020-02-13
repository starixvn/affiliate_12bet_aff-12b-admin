using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Gpi.MetaModels;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Newtonsoft.Json;
using Polly;

namespace Fanex.Affiliate.DataService.Importer
{
    public class CrystalGpiService : ICrystalGpiService
    {
        private const int DelayTimeMillisecond = 16000;

        private readonly IAppSettings _appSetting;
        private readonly IThirdPartyProductRepository _thirdPartyProductRepository;

        private static ISyncPolicy<string> _requestPolicy;
        public static ISyncPolicy<string> CurrentPolicy
        {
            get
            {
                if (_requestPolicy == null)
                {
                    _requestPolicy = GetDefaultPolicy();
                }
                return _requestPolicy;
            }
            set { _requestPolicy = value; }
        }

        public CrystalGpiService(IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
        {
            _appSetting = appSetting ?? new AppSettings();
            _thirdPartyProductRepository = thirdPartyProductRepository ?? new ThirdPartyProductRepository();
        }

        public IEnumerable<string> GetRawDataList(CrystalGpiParam param)
        {
            var paramByTimeZone = BuildParamByTimeZone(param);

            return GetRawDataListByHour(paramByTimeZone);
        }

        public void Import(CrystalGpiParam param)
        {
            var paramByTimeZone = BuildParamByTimeZone(param);
            ImportByHour(paramByTimeZone);
        }

        public void Import(IEnumerable<string> dataList)
        {
            foreach (var item in dataList)
            {
                _thirdPartyProductRepository.ImportData(Products.CrystalProductId, item);
            }
        }

        private void ImportByHour(CrystalGpiParam param)
        {
            var startDate = new DateTime(param.StartTime.Year, param.StartTime.Month, param.StartTime.Day, param.StartTime.Hour, 0, 0);
            var endDate = new DateTime(param.EndTime.Year, param.EndTime.Month, param.EndTime.Day, param.EndTime.Hour, 0, 0);

            for (var start = startDate; DateTime.Compare(start, endDate) < 0; start = start.AddHours(1))
            {
                var endDateByBatch = start.AddHours(1);
                var paramByBatch = BuildParam(
                    new CrystalGpiParam
                    {
                        StartTime = start,
                        EndTime = DateTime.Compare(endDateByBatch, endDate) > 0 ? endDate : endDateByBatch,
                        PageNumber = param.PageNumber,
                        PageSize = param.PageSize,
                        JobType = param.JobType
                    },
                    param.PageNumber);

                ImportByBatch(paramByBatch);
            }
        }

        private void ImportByBatch(CrystalGpiParam paramByBatch)
        {
            var totalPages = ImportByPaging(paramByBatch);

            for (var pageNumber = 2; pageNumber <= totalPages; pageNumber++)
            {
                var paramByPaging = BuildParam(paramByBatch, pageNumber);
                ImportByPaging(paramByPaging);
            }
        }

        private int ImportByPaging(CrystalGpiParam paramByPaging)
        {
            var rawData = GetRawData(paramByPaging);
            var xDocument = XmlHelper.ParseToXDocument(rawData);
            if (xDocument == null)
            {
                throw new ThirdPartyProductException($"Error: Crystal(Gpi)\nParam:{JsonConvert.SerializeObject(paramByPaging)}\nResult:{rawData}");
            }

            var hasError = string.Equals("error", xDocument.Root.Name.LocalName, StringComparison.InvariantCultureIgnoreCase);
            if (hasError)
            {
                throw new ThirdPartyProductException($"Error: Crystal(Gpi)\nParam:{JsonConvert.SerializeObject(paramByPaging)}\nResult:{rawData}");
            }

            _thirdPartyProductRepository.ImportData(Products.CrystalProductId, rawData, paramByPaging.JobType);

            return Convert.ToInt32(xDocument.Root.Attribute("totalPage")?.Value);
        }

        private IEnumerable<string> GetRawDataListByHour(CrystalGpiParam paramByHour)
        {
            var result = new List<string>();
            var startDate = new DateTime(paramByHour.StartTime.Year, paramByHour.StartTime.Month, paramByHour.StartTime.Day, paramByHour.StartTime.Hour, 0, 0);
            var endDate = new DateTime(paramByHour.EndTime.Year, paramByHour.EndTime.Month, paramByHour.EndTime.Day, paramByHour.EndTime.Hour, 0, 0);

            for (var start = startDate; DateTime.Compare(start, endDate) < 0; start = start.AddHours(1))
            {
                var endDateByBatch = start.AddHours(1);
                var paramByBatch = BuildParam(
                    new CrystalGpiParam
                    {
                        StartTime = start,
                        EndTime = DateTime.Compare(endDateByBatch, endDate) > 0 ? endDate : endDateByBatch,
                        PageNumber = paramByHour.PageNumber,
                        PageSize = paramByHour.PageSize
                    },
                    paramByHour.PageNumber);

                var dataListByBatch = GetRawDataListByBatch(paramByBatch);
                result.AddRange(dataListByBatch);
            }

            return result;
        }

        private IEnumerable<string> GetRawDataListByBatch(CrystalGpiParam paramByBatch)
        {
            var result = new List<string>();
            var rawData = GetRawDataByPaging(paramByBatch);
            var xDocument = XmlHelper.ParseToXDocument(rawData);
            result.Add(rawData);

            var totalPages = Convert.ToInt32(xDocument.Root.Attribute("totalPage")?.Value);

            if (totalPages > 1 && paramByBatch.PageNumber < totalPages)
            {
                for (var pageNumber = 2; pageNumber <= totalPages; pageNumber++)
                {
                    var paramByPaging = BuildParam(paramByBatch, pageNumber);
                    result.Add(GetRawDataByPaging(paramByPaging));
                }
            }

            return result;
        }

        private string GetRawDataByPaging(CrystalGpiParam paramByPaging)
        {
            var rawData = GetRawData(paramByPaging);
            var xDocument = XmlHelper.ParseToXDocument(rawData);
            if (xDocument == null)
            {
                throw new ThirdPartyProductException($"Error: Crystal(Gpi)\nParam:{JsonConvert.SerializeObject(paramByPaging)}\nResult:{rawData}");
            }
            var hasError = string.Equals("error", xDocument.Root.Name.LocalName, StringComparison.InvariantCultureIgnoreCase);
            if (hasError)
            {
                throw new ThirdPartyProductException($"Error: Crystal(Gpi)\nParam:{JsonConvert.SerializeObject(paramByPaging)}\nResult:{rawData}");
            }

            return rawData;
        }

        private string GetRawData(CrystalGpiParam param)
        {
            var apiUrl = BuildApiUrl(param);
            Thread.Sleep(DelayTimeMillisecond);

            return GetDataString(apiUrl);
        }

        private CrystalGpiParam BuildParamByTimeZone(CrystalGpiParam entryParam)
        {
            var copiedParam = BuildParam(entryParam, entryParam.PageNumber);
            copiedParam.StartTime = DateTimeConverter.ToGMT(entryParam.StartTime.ToUniversalTime(), TimeZoneId.GmtPlus8);
            copiedParam.EndTime = DateTimeConverter.ToGMT(entryParam.EndTime.ToUniversalTime(), TimeZoneId.GmtPlus8);

            return copiedParam;
        }

        private CrystalGpiParam BuildParam(CrystalGpiParam param, int pageNum)
        {
            return new CrystalGpiParam
            {
                StartTime = param.StartTime,
                EndTime = param.EndTime,
                PageNumber = pageNum,
                PageSize = param.PageSize,
                JobType = param.JobType,
                Code = _appSetting.GetValue("CrystalGpiCode"),
                Password = _appSetting.GetValue("CrystalGpiPassword"),
                Type = Convert.ToByte(_appSetting.GetValue("CrystalGpiType"))
            };
        }

        private string BuildApiUrl(CrystalGpiParam param)
        {
            var formatDateTime = _appSetting.GetValue("CrystalGpiFormatDateTime");
            var startTime = param.StartTime.ToString(formatDateTime);
            var endTime = param.EndTime.ToString(formatDateTime);

            return _appSetting.GetValue("CrystalGpiApiUrl") + $"?opCode={param.Code}&s={startTime}&e={endTime}&pageNum={param.PageNumber}&pageSize={param.PageSize}&pwd={param.Password}&type={param.Type}";
        }

        private static string GetDataString(string url)
        {
            var result = CurrentPolicy.Execute(() =>
            {
                using (var client = new WebClient())
                {
                    return client.DownloadString(url);
                }
            });
            return result;

        }

        private static ISyncPolicy<string> GetDefaultPolicy()
        {
            var retryPolicy = Policy.HandleResult<string>(response => response.Contains("Request too often"))
                .WaitAndRetry(2, retryCount => TimeSpan.FromMilliseconds(DelayTimeMillisecond));

            var policy = Policy.Bulkhead<string>(1, 5)
                .Wrap(retryPolicy)
                .WithPolicyKey("CrystalGpi");

            return policy;
        }
    }
}