using Fanex.Affiliate.DataService.Importer.Constants;
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

        public TopazService(IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyRepository = null) : base(appSetting, thirdPartyRepository)
        {
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
            var param = BuildParam(new FlowGamingParam {
                StartTime = startRequestDate,
                EndTime = endRequestDate
            });

            var url = BuildUrl(param);
            using(WebClient client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }

        private string BuildUrl(FlowGamingParam param)
        {
            var baseUrl = _appSetting.GetValue("TopazApiUrl");
            var dateTimeFormat = _appSetting.GetValue("TopazFormatDateTime");
            return $"{baseUrl}?brandId={param.BrandId}&brandPassword={param.BrandPassword}&startTime={param.StartTime.ToString(dateTimeFormat)}&endTime={param.EndTime.ToString(dateTimeFormat)}";
        }

        private FlowGamingParam BuildParam(FlowGamingParam flowGamingParam)
        {
            return new FlowGamingParam()
            {
                BrandId = _appSetting.GetValue("TopazBrandId"),
                BrandPassword = _appSetting.GetValue("TopazBrandPassword"),
                StartTime = flowGamingParam.StartTime,
                EndTime = flowGamingParam.EndTime
            };
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

        protected override XDocument ConvertToXml(string data)
        {
            var xml = XmlHelper.ParseToXDocumentFromJson(data);
            if (xml == null)
                throw new ThirdPartyProductException($"FlowGaming - Cant convert json response to xml - {data}");
            return xml;
        }
    }
}
