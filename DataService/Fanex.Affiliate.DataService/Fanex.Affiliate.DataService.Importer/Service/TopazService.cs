using Fanex.Affiliate.DataService.Importer.Client;
using Fanex.Affiliate.DataService.Importer.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Exceptions;
using Fanex.Affiliate.DataService.Importer.MetaModels.FlowGaming;
using Fanex.Affiliate.DataService.Importer.Repository;
using Fanex.Affiliate.DataService.Importer.ResponseChecker;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer
{
    public class TopazService : XmlImpoterlServiceBase
    {
        private const string CALL_LIMIT_EXCEEDED = "CALL_LIMIT_EXCEEDED";
        //60s to request next data for same brand_id request
        private const int TIME_BETWEEN_TWO_REQUEST = 60000;
        private const int MAX_TIME_RANGE_TRANSACTION = 15;

        private readonly IApiClient _client;
        private bool disposed = false;

        public TopazService(IApiClient client = null,IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyRepository = null) : base(appSetting, thirdPartyRepository)
        {
            _client = client ?? new TopazClient();
        }

        public override void Import(string data)
        {
            _thirdPartyProductRepository.ImportData(Products.TopazId, data, 0);
        }

        public override IEnumerable<string> GetRawDataList(DateTime startTime, DateTime endTime)
        {
            if (DateTime.Compare(startTime, endTime) >= 0)
                throw new ThirdPartyProductException($"Flow Gaming - startDate {startTime} later or equal than endDate {endTime}");

            var startRequestTime = startTime;
            var compareEndDate = startRequestTime.AddMinutes(MAX_TIME_RANGE_TRANSACTION);
            var endRequestTime = GetEndRequestTime(compareEndDate, endTime);
            var isFirstRequest = true;

            while (DateTime.Compare(endRequestTime, startRequestTime) > 0)
            {
                if (!isFirstRequest)
                    Thread.Sleep(TIME_BETWEEN_TWO_REQUEST);
                isFirstRequest = false;

                var data = GetRawData(startRequestTime, endRequestTime);
                if (IsExceedCallLimit(data))
                {
                    Thread.Sleep(TIME_BETWEEN_TWO_REQUEST);
                    data = GetRawData(startRequestTime, endRequestTime);
                }
                yield return data;

                startRequestTime = endRequestTime;
                endRequestTime = GetEndRequestTime(startRequestTime.AddMinutes(MAX_TIME_RANGE_TRANSACTION), endTime);             
            }
        }

        private bool IsExceedCallLimit(string data)
        {
            return data.Contains(CALL_LIMIT_EXCEEDED);
        }

        private string GetRawData(DateTime startRequestDate, DateTime endRequestDate)
        {
            var param = GetQueryParam(startRequestDate, endRequestDate);
            var baseUrl = _appSetting.GetValue("TopazApiUrl");
            return _client.GetResponse(baseUrl, param);
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

        private DateTime GetEndRequestTime(DateTime compareTime, DateTime endTime)
        {
            return DateTime.Compare(compareTime, endTime) > 0 ? endTime : compareTime;
        }

        protected override void CheckXmlResponseData(XDocument xml)
        {
            IThirdPartyResponseChecker checker = new TopazResponseChecker();
            checker.CheckXml(xml);
        }

        public override XDocument ConvertRawDataToXml(string raw)
        {
            var xml = XmlHelper.ParseToXDocumentFromJson(raw);
            if (xml == null)
                throw new ThirdPartyProductException($"FlowGaming - Cant convert json response to xml - {raw}");
            return xml;
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
                _client?.Dispose();
            disposed = true;
        }

        ~TopazService()
        {
            Dispose(false);
        }
    }
}
