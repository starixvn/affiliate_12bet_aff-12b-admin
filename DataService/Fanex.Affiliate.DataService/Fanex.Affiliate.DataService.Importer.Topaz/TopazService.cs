using Fanex.Affiliate.DataService.Importer.FlowGaming.Client;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.ResponseChecker;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Fanex.Affiliate.DataService.Importer.FlowGaming.ResponseChecker;

namespace Fanex.Affiliate.DataService.Importer
{
    public class TopazService : XmlImpoterlServiceBase
    {
        private const string CALL_LIMIT_EXCEEDED = "CALL_LIMIT_EXCEEDED";
        //60s to request next data for same brand_id request
        private const int TIME_BETWEEN_TWO_REQUEST = 60000;
        private const int MAX_TIME_RANGE_TRANSACTION = 15;

        private readonly IApiClient _client;

        protected override int ProductId => Products.TopazId;

        public TopazService(IApiClient client = null,IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyRepository = null) : base(appSetting, thirdPartyRepository)
        {
            _client = client ?? new TopazClient();
        }

        public override async Task<IEnumerable<string>> GetRawDataListAsync(DateTime startTime, DateTime endTime)
        {
            if (DateTime.Compare(startTime, endTime) >= 0)
                throw new ThirdPartyProductException($"Flow Gaming - startDate {startTime} later or equal than endDate {endTime}");

            var timeRanges = GetTimeRangeRequest(startTime, endTime);
            var datas = new List<string>(timeRanges.Count());

            foreach (var time in timeRanges)
            {
                var data = await GetRawDataAsync(time.Item1, time.Item2);
                if (IsExceedCallLimit(data))
                {
                    await Task.Delay(TIME_BETWEEN_TWO_REQUEST);
                    data = await GetRawDataAsync(time.Item1, time.Item2);
                }
                datas.Add(data);
            }
            return datas;
        }

        private IEnumerable<Tuple<DateTime,DateTime>> GetTimeRangeRequest(DateTime startTime, DateTime endTime)
        {
            var startRequestTime = startTime;
            var endRequestTime = GetEndRequestTime(startRequestTime.AddMinutes(MAX_TIME_RANGE_TRANSACTION), endTime);
            while(startRequestTime < endRequestTime)
            {
                yield return new Tuple<DateTime, DateTime>(startRequestTime, endRequestTime);
                startRequestTime = endRequestTime;
                endRequestTime = GetEndRequestTime(endRequestTime.AddMinutes(MAX_TIME_RANGE_TRANSACTION), endTime);
            }
        }

        private DateTime GetEndRequestTime(DateTime compareTime, DateTime endTime)
        {
            return DateTime.Compare(compareTime, endTime) > 0 ? endTime : compareTime;
        }

        private bool IsExceedCallLimit(string data)
        {
            return data.Contains(CALL_LIMIT_EXCEEDED);
        }

        private async Task<string> GetRawDataAsync(DateTime startRequestDate, DateTime endRequestDate)
        {
            var param = GetQueryParam(startRequestDate, endRequestDate);
            var baseUrl = _appSetting.GetValue("TopazApiUrl");
            return await _client.GetResponseAsync(baseUrl, param).ConfigureAwait(false);
        }

        private NameValueCollection GetQueryParam(DateTime startTime, DateTime endTime)
        {
            var dateTimeFormat = _appSetting.GetValue("TopazFormatDateTime");
            var result = new NameValueCollection(5);
            result.Add("brandId", _appSetting.GetValue("TopazBrandId"));
            result.Add("brandPassword", _appSetting.GetValue("TopazBrandPassword"));
            result.Add("startTime", startTime.ToString(dateTimeFormat));
            result.Add("endTime", endTime.ToString(dateTimeFormat));
            return result;
        }

        protected override void CheckXmlResponseData(XDocument xml)
        {
            IThirdPartyResponseChecker checker = new TopazResponseChecker();
            checker.CheckXml(xml);
        }

        public override Task<XDocument> ConvertRawDataToXmlAsync(string raw)
        {
            var xml = XmlHelper.ParseToXDocumentFromJson(raw);
            if (xml == null)
                throw new ThirdPartyProductException($"FlowGaming - Cant convert json response to xml - {raw}");
            return Task.FromResult(xml);
        }
    }
}
