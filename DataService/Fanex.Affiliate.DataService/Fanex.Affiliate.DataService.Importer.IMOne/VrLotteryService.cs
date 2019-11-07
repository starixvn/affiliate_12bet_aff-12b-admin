using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Fanex.Affiliate.DataService.Importer.Repository;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Fanex.Affiliate.DataService.Importer.Constants;
using Fanex.Affiliate.DataService.Importer.IMOne;
using Fanex.Affiliate.DataService.Importer.IMOne.Extension;
using Fanex.Affiliate.DataService.Importer.Infrastructure;
using System.Collections.Specialized;
using Fanex.Affiliate.DataService.Importer.IMOne.Client;
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;

namespace Fanex.Affiliate.DataService.Importer
{
    public class VrLotteryService : XmlImpoterlServiceBase, IDataRequestByCurrency
    {
        private readonly TimeSpan MAX_REQUEST_TIMESPAN = new TimeSpan(31, 0, 0, 0);
        protected IApiClient _client;

        public VrLotteryService(IApiClient client = null, IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null) : base(appSetting, thirdPartyProductRepository)
        {
            _client = client ?? new VrLotteryClient();
        }

        public override IEnumerable<string> GetRawDataList(DateTime startTime, DateTime endTime)
        {
            var supportedCurrencies = _appSetting.GetSupportedVrLotteryCurrency();
            return GetRawDataListByCurrencies(startTime, endTime, supportedCurrencies);
        }

        public IEnumerable<string> GetRawDataListByCurrencies(DateTime startTime, DateTime endTime, IEnumerable<string> currencies)
        {
            foreach (var currency in currencies)
            {
                var datas = GetRawDataListByCurrency(startTime, endTime, currency);
                foreach (var data in datas)
                {
                    yield return data;
                }
            }
        }

        public IEnumerable<string> GetRawDataListByCurrency(DateTime startTime, DateTime endTime, string currency)
        {
            DateTime startTimeRoundingByHour = GetRoundingByHour(startTime);
            DateTime endTimeRoundingByHour = GetRoundingByHour(endTime);
            EnsureEndTimeLaterThanStartTime(startTimeRoundingByHour, endTimeRoundingByHour);

            var timeSpan = endTimeRoundingByHour - startTimeRoundingByHour;
            while(timeSpan > TimeSpan.Zero)
            {
                var startTimeRequest = startTimeRoundingByHour;
                var endTimeRequest = endTimeRoundingByHour;

                timeSpan = timeSpan - MAX_REQUEST_TIMESPAN;
                if (timeSpan > TimeSpan.Zero)
                    endTimeRequest = startTimeRoundingByHour.Add(MAX_REQUEST_TIMESPAN);

                yield return GetRawData(startTimeRequest, endTimeRequest, currency);

                startTimeRoundingByHour = endTimeRequest;
            }
        }

        private string GetRawData(DateTime startTimeRequest, DateTime endTimeRequest, string currency)
        {
            var url = _appSetting.GetValue("VrLotteryApiUrl");
            var queryValue = BuildQuery(startTimeRequest, endTimeRequest, currency);

            return _client.GetResponse(url, queryValue);
        }

        private NameValueCollection BuildQuery(DateTime startTimeRequest, DateTime endTimeRequest, string currency)
        {
            var queryValue = new NameValueCollection();
            queryValue.Add("MerchantCode", _appSetting.GetValue("VrLotteryMerchantCode"));
            queryValue.Add("StartDate", startTimeRequest.ToString(IMOne.Constants.IMOneDateTimeFormat));
            queryValue.Add("EndDate", endTimeRequest.ToString(IMOne.Constants.IMOneDateTimeFormat));
            queryValue.Add("ProductWallet", _appSetting.GetValue("VrLotteryProductWallet"));
            queryValue.Add("Currency", currency);
            queryValue.Add("ReportBy", _appSetting.GetValue("VrLotteryReportBy"));
            return queryValue;
        }

        private DateTime GetRoundingByHour(DateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0, 0);
        }

        public override void Import(string data)
        {
            _thirdPartyProductRepository.ImportData(Products.VRLotteryId, data, 0);
        }

        protected override void CheckXmlResponseData(XDocument xml)
        {
            var checker = new VrLotteryXmlChecker();
            checker.CheckXml(xml);
        }

        protected override XDocument ConvertToXml(string data)
        {
            return XmlHelper.ParseToXDocumentFromJson(data);
        }

        public async Task<IEnumerable<string>> GetRawDataListByCurrenciesAsync(DateTime startTime, DateTime endTime, IEnumerable<string> currencies)
        {
            await Task.Yield();
            return GetRawDataListByCurrencies(startTime, endTime, currencies);
        }

        public async Task<IEnumerable<string>> GetRawDataListByCurrencyAsync(DateTime startTime, DateTime endTime, string currency)
        {
            await Task.Yield();
            return GetRawDataListByCurrency(startTime, endTime, currency);
        }
    }
}
