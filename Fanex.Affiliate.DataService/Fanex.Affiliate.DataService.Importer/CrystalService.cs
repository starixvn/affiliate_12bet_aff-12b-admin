using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using Fanex.Affiliate.DataService.Importer.Constants;
using Fanex.Affiliate.DataService.Importer.Exceptions;
using Fanex.Affiliate.DataService.Importer.MetaModels.Crystal;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Newtonsoft.Json;

namespace Fanex.Affiliate.DataService.Importer
{
    public class CrystalService : ICrystalService
    {
        private readonly IAppSettings _appSetting;

        public CrystalService(IAppSettings appSetting = null)
        {
            _appSetting = appSetting ?? new AppSettings();
        }

        public async Task<IEnumerable<string>> GetRawDataList(CrystalParam param)
        {
            var result = new List<string>();
            var paramByTimeZone = BuildParamByTimeZone(param);
            var firstRawData = await GetRawData(paramByTimeZone);
            var xDocument = XDocument.Parse(firstRawData);
            var hasError = string.Equals("error", xDocument.Root.Name.LocalName, StringComparison.InvariantCultureIgnoreCase);
            if (hasError)
            {
                throw new ThirdPartyProductException($"Crystal Product Error\nParam:{JsonConvert.SerializeObject(paramByTimeZone)}\nResult:{firstRawData}");
            }

            result.Add(firstRawData);
            var totalPages = Convert.ToInt32(xDocument.Root.Attribute("totalPage")?.Value);
            if (totalPages > 1 && param.PageNumber <= totalPages)
            {
                await Task.Delay(16000);
                var paramByPage = BuildParam(param, param.PageNumber + 1);
                result.AddRange(await GetRawDataList(paramByPage));
            }

            return result;
        }

        private async Task<IEnumerable<string>> GetRawDataByBatch(CrystalParam param)
        {
            var result = new List<string>();
            var startDate = new DateTime(param.StartTime.Year, param.StartTime.Month, param.StartTime.Day, param.StartTime.Hour, 0, 0);
            var endDate = new DateTime(param.EndTime.Year, param.EndTime.Month, param.EndTime.Day, param.EndTime.Hour, 0, 0);

            for (var start = startDate; DateTime.Compare(start, endDate) < 0; start = start.AddHours(1))
            {
                var paramByBatch = BuildParam(new CrystalParam { StartTime = start, EndTime = start.AddHours(1) });
                taskList.Add(GetRawDataAsync(paramByBatch));
            }

            return result;
        }

        private async Task<string> GetRawData(CrystalParam param)
        {
            var apiUrl = BuildApiUrl(param);
            return await GetDataStringAsync(apiUrl);
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
                PageSize = Convert.ToInt32(_appSetting.GetValue("CrystalPageSize")),
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

        private async Task<string> GetDataStringAsync(string url)
        {
            using (var client = new WebClient())
            {
                return await client.DownloadStringTaskAsync(url);
            }
        }
    }
}
