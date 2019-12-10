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
        private const int MINUTE_TIME_RANGE_LIMIT = 10;
        public const int MAX_PAGE_SIZE = 50000;

        protected readonly IApiClient _client;
        protected override int ProductId => Products.VRLotteryId;

        public int TimeRangeLimitInMinForEachRequest { get; set; } = MINUTE_TIME_RANGE_LIMIT;

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
            EnsureEndTimeLaterThanStartTime(startTime, endTime);

            var datas = new List<string>();
            var timeSpanRequest = new TimeSpan(0, TimeRangeLimitInMinForEachRequest, 0);
            var timeRanges = DateTimeHelper.GetProportionTimeRange(startTime, endTime, timeSpanRequest);
            foreach (var timeRange in timeRanges)
            {
                var startRequestTime = timeRange.Item1;
                var endRequestTime = timeRange.Item2;
                datas.AddRange(await GetRawDataByPageAsync(startRequestTime, endRequestTime, currency).ConfigureAwait(false));

            }
            return datas;
        }

        private async Task<IEnumerable<string>> GetRawDataByPageAsync(DateTime startTimeRequest, DateTime endTimeRequest, string currency)
        {
            var datas = new List<string>();
            var timeRange = new Tuple<DateTime, DateTime>(startTimeRequest, endTimeRequest);
            int page = 1;
            var rawData = await GetRawDataAsync(timeRange, currency, page);
            datas.Add(rawData);
            while (HasAnotherPage(rawData))
            {
                page++;
                rawData = await GetRawDataAsync(timeRange, currency, page);
                datas.Add(rawData);
            }
            return datas;

        }

        private bool HasAnotherPage(string rawData)
        {
            var xml = XmlHelper.ParseToXDocumentFromJson(rawData);
            CheckXmlResponseData(xml);
            var pageProportion = xml.Root.Element("Pagination");
            var currentPage = int.Parse(pageProportion.Element("CurrentPage").Value);
            var totalPage = int.Parse(pageProportion.Element("TotalPage").Value);
            return currentPage < totalPage;
        }

        private async Task<string> GetRawDataAsync(Tuple<DateTime, DateTime> timeRange, string currency, int page)
        {
            var url = _appSetting.GetValue("VrLotteryApiUrl");
            var queryValue = BuildQuery(timeRange, currency, page);

            return await _client.GetResponseAsync(url, queryValue);
        }

        private  NameValueCollection BuildQuery(Tuple<DateTime, DateTime> timeRange, string currency, int page)
        {
            var startTimeRequest = timeRange.Item1;
            var endTimeRequest = timeRange.Item2;
            var queryValue = new NameValueCollection();

            queryValue.Add("MerchantCode",  _appSetting.GetValue("VrLotteryMerchantCode"));
            queryValue.Add("StartDate", startTimeRequest.ToString(IMOne.Constants.IMOneDateTimeFormat));
            queryValue.Add("EndDate", endTimeRequest.ToString(IMOne.Constants.IMOneDateTimeFormat));
            queryValue.Add("ProductWallet", _appSetting.GetValue("VrLotteryProductWallet"));
            queryValue.Add("Currency", currency);
            queryValue.Add("PageSize", MAX_PAGE_SIZE.ToString());
            queryValue.Add("Page", page.ToString());

            return queryValue;
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
