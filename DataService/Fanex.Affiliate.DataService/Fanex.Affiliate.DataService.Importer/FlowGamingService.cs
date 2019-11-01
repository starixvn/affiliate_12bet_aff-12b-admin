using Fanex.Affiliate.DataService.Importer.Exceptions;
using Fanex.Affiliate.DataService.Importer.MetaModels.FlowGaming;
using Fanex.Affiliate.DataService.Importer.Repository;
using Fanex.Affiliate.DataService.Importer.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer
{
    public class FlowGamingService : ImporterXmlServiceBase
    {
        private const string CALL_LIMIT_EXCEEDED = "CALL_LIMIT_EXCEEDED";
        //60s to request next data for same brand_id request
        private const int TIME_BETWEEN_TWO_REQUEST = 60000;
        private const int MAX_TIME_RANGE_TRANSACTION = 15;

        public FlowGamingService(IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyRepository = null) : base(appSetting, thirdPartyRepository)
        {
        }

        public override IEnumerable<string> GetRawDataList(DateTime startTime, DateTime endTime)
        {
            if (DateTime.Compare(startTime, endTime) >= 0)
                throw new ThirdPartyProductException($"Flow Gaming - startDate {startTime} later or equal than endDate {endTime}");

            var startRequestTime = startTime;
            var compareEndDate = startRequestTime.AddMinutes(MAX_TIME_RANGE_TRANSACTION);
            var endRequestTime = GetEndRequestTime(compareEndDate, endTime);
            while (DateTime.Compare(endRequestTime, startRequestTime) > 0)
            {
                var data = GetRawData(startRequestTime, endRequestTime);
                yield return data;

                startRequestTime = endRequestTime;
                endRequestTime = GetEndRequestTime(startRequestTime.AddMinutes(MAX_TIME_RANGE_TRANSACTION), endTime);                
            }
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
                BrandPassword = _appSetting.GetValue("TopazPassword"),
                StartTime = flowGamingParam.StartTime,
                EndTime = flowGamingParam.EndTime
            };
        }

        private DateTime GetEndRequestTime(DateTime compareTime, DateTime endTime)
        {
            return DateTime.Compare(compareTime, endTime) > 0 ? endTime : compareTime;
        }

        protected override string ConvertToXmlString(string data)
        {
            var xml = XmlHelper.ParseToXDocumentFromJson(data);
            return xml.ToString();
        }
    }
}
