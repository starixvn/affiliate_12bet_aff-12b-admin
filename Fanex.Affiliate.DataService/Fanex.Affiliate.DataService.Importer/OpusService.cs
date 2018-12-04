using System;
using System.Collections.Generic;
using System.Net;
using Fanex.Affiliate.DataService.Importer.Constants;
using Fanex.Affiliate.DataService.Importer.MetaModels.Opus;
using Fanex.Affiliate.DataService.Importer.Utilities;

namespace Fanex.Affiliate.DataService.Importer
{
    public class OpusService : IOpusService
    {
        private const int TimeRangeByMinute = 15;

        private readonly IAppSettings _appSetting;

        public OpusService(IAppSettings appSetting = null)
        {
            _appSetting = appSetting ?? new AppSettings();
        }

        public IEnumerable<string> GetRawDataList(OpusParam param)
        {
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
                var rawData = GetRawData(paramByBatch);

                result.Add(rawData);
            }

            return result;
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

            using (var client = new WebClient())
            {
                return client.UploadString(apiUrl, queryString);
            }
        }

        private string BuildQueryString(OpusParam param)
        {
            var dateTimeFormat = _appSetting.GetValue("OpusFormatDateTime");
            var startTimeString = param.StartTime.ToString(dateTimeFormat);
            var endTimeString = param.EndTime.ToString(dateTimeFormat);

            return $"secret_key={param.SecretKey}&operator_id={param.OperatorId}&site_code={param.SiteCode}&product_code={param.ProductCode}&start_time={startTimeString}&end_time={endTimeString}";
        }
    }
}
