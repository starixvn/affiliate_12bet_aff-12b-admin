using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.IMOne;
using Fanex.Affiliate.DataService.Importer.IMOne.Extension;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using System.Collections.Specialized;
using Fanex.Affiliate.DataService.Importer.IMOne.Client;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer 
{
    public class VrLotteryService : XmlImpoterlServiceBase, IDataRequestByCurrency
    {
        private readonly TimeSpan MAX_REQUEST_TIMESPAN = new TimeSpan(31, 0, 0, 0);
        protected readonly IApiClient _client;

        protected override int ProductId => Products.VRLotteryId;

        public VrLotteryService(IApiClient client = null, IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null) 
            : base(appSetting, thirdPartyProductRepository)
        {
            _client = client ?? new VrLotteryClient();
        }

        public override async Task<IEnumerable<string>> GetRawDataListAsync(DateTime startTime, DateTime endTime)
        {
            var supportedCurrencies = _appSetting.GetSupportedVrLotteryCurrency();
            return await GetRawDataListByCurrenciesAsync(startTime, endTime, supportedCurrencies);
        }

        public async Task<IEnumerable<string>> GetRawDataListByCurrenciesAsync(DateTime startTime, DateTime endTime, IEnumerable<string> currencies)
        {
            var datas = new List<string>();
            foreach (var currency in currencies)
            {
                datas.AddRange(await GetRawDataListByCurrencyAsync(startTime, endTime, currency).ConfigureAwait(false));
            }
            return datas;
        }

        public async Task<IEnumerable<string>> GetRawDataListByCurrencyAsync(DateTime startTime, DateTime endTime, string currency)
        {
            DateTime startTimeRoundingByHour = GetRoundingByHour(startTime);
            DateTime endTimeRoundingByHour = GetRoundingByHour(endTime);
            EnsureEndTimeLaterThanStartTime(startTimeRoundingByHour, endTimeRoundingByHour);

            var datas = new List<string>();

            var timeSpan = endTimeRoundingByHour - startTimeRoundingByHour;
            while(timeSpan > TimeSpan.Zero)
            {
                var startTimeRequest = startTimeRoundingByHour;
                var endTimeRequest = endTimeRoundingByHour;

                timeSpan = timeSpan - MAX_REQUEST_TIMESPAN;
                if (timeSpan > TimeSpan.Zero)
                {
                    endTimeRequest = startTimeRoundingByHour.Add(MAX_REQUEST_TIMESPAN);
                }

                datas.Add(await GetRawDataAsync(startTimeRequest, endTimeRequest, currency).ConfigureAwait(false));

                startTimeRoundingByHour = endTimeRequest;
            }
            return datas;
        }

        private async Task<string> GetRawDataAsync(DateTime startTimeRequest, DateTime endTimeRequest, string currency)
        {
            var url = _appSetting.GetValue("VrLotteryApiUrl");
            var queryValue = BuildQuery(startTimeRequest, endTimeRequest, currency);

            return await _client.GetResponseAsync(url, queryValue);
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

        private static DateTime GetRoundingByHour(DateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0, 0);
        }

        protected override void CheckXmlResponseData(XDocument xml)
        {
            var checker = new VrLotteryXmlChecker();
            checker.CheckXml(xml);
        }

        public override Task<XDocument> ConvertRawDataToXmlAsync(string raw)
        {
            return Task.FromResult(XmlHelper.ParseToXDocumentFromJson(raw));
        }
    }
}
