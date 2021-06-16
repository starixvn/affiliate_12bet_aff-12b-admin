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
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using System.Globalization;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Fanex.Affiliate.DataService.Importer
{
    public class IMEsportService : XmlImporterlServiceBase, IDataRequestByCurrency
    {
        protected readonly IApiClient _client;
        protected override int ProductId => Products.IMEsport;

        public override bool IsNeedConvertTime => false;



        public IMEsportService(IApiClientFactory apiClientFactory = null, IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
            : base(appSetting, thirdPartyProductRepository)
        {
            var factory = apiClientFactory ?? new ApiClientFactory();
            _client = factory.CreateApiClient(_appSetting.GetValue("IMEsportApiUrl"), HttpMethod.Post);
        }

        public override async Task<IEnumerable<string>> GetRawDataListAsync(DateTime startTime, DateTime endTime)
        {
            var supportedCurrencies = _appSetting.GetSupportedIMEsportCurrency();
            return await GetRawDataListByCurrenciesAsync(startTime, endTime, supportedCurrencies);
        }

        public async Task<IEnumerable<string>> GetRawDataListByCurrenciesAsync(DateTime startTime, DateTime endTime, IEnumerable<string> currencies)
        {
            var datas = new List<string>();
            foreach (var currency in currencies)
            {
                datas.AddRange(await GetRawDataListByCurrencyAsync(startTime, endTime, currency));
            }
            return datas;
        }

        public async Task<IEnumerable<string>> GetRawDataListByCurrencyAsync(DateTime startTime, DateTime endTime, string currency)
        {
            EnsureEndTimeLaterThanStartTime(startTime, endTime);

            var datas = new List<string>();
            var timeSpanRequest = new TimeSpan(1 ,0, 0, 0);
            var timeRanges = DateTimeHelper.GetProportionTimeRange(startTime, endTime, timeSpanRequest);
            foreach (var timeRange in timeRanges)
            {
                var rawData = await GetRawDataAsync(timeRange, currency);
                var injectedData = InjectTimeRangeData(rawData, timeRange);
                datas.Add(injectedData);
            }
            return datas;
        }

        private string InjectTimeRangeData(string rawData, Tuple<DateTime, DateTime> timeRange)
        {
            var startTimeRequest = timeRange.Item1;
            var endTimeRequest = timeRange.Item2;
            var json = JObject.Parse(rawData);
            json.Add("startDate", startTimeRequest.ToString(IMOne.Constants.IMEsportDateFormat));
            json.Add("endDate", endTimeRequest.ToString(IMOne.Constants.IMEsportDateFormat));
            return json.ToString();
        }

        private async Task<string> GetRawDataAsync(Tuple<DateTime, DateTime> timeRange, string currency)
        {
            var queryValue = BuildQuery(timeRange, currency);
            return await _client.GetResponseAsync(queryValue);
        }

        private NameValueCollection BuildQuery(Tuple<DateTime, DateTime> timeRange, string currency)
        {
            var startTimeRequest = timeRange.Item1;
            var endTimeRequest = timeRange.Item2;
            var queryValue = new NameValueCollection();

            queryValue.Add("MerchantCode", _appSetting.GetValue("IMEsportMerchantCode"));
            queryValue.Add("StartDate", startTimeRequest.ToString(IMOne.Constants.IMEsportDateFormat));
            queryValue.Add("EndDate", endTimeRequest.ToString(IMOne.Constants.IMEsportDateFormat));
            queryValue.Add("ProductWallet", _appSetting.GetValue("IMEsportProductWallet"));
            queryValue.Add("Currency", currency);
            queryValue.Add("HousePlayer", "0");

            return queryValue;
        }

        protected override void CheckXmlResponseData(XDocument xml)
        {
            var checker = new IMEsportXmlChecker();
            checker.CheckXml(xml);
        }

        public override Task<XDocument> ConvertRawDataToXmlAsync(string raw)
        {
            return Task.FromResult(XmlHelper.ParseToXDocumentFromJson(raw));
        }

        protected override bool CheckIsSuitableToImport(XDocument xml)
        {
            var codeElem = xml.Root.Element("Code");
            if (codeElem == null)
                return false;

            IMoneResponseStatus status;
            if (!Enum.TryParse<IMoneResponseStatus>(codeElem.Value, out status))
            {
                throw new ThirdPartyProductException($"IMEsport - Can't convert {codeElem.Value} response code");
            }

            if (status == IMoneResponseStatus.SUCCESS)
                return true;
            else
                return false;

        }
    }
}
