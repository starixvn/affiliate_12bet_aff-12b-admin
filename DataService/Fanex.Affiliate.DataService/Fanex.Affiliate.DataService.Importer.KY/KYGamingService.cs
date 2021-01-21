using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using Fanex.Affiliate.DataService.Importer.KYGaming.MetaModels;
using Fanex.Affiliate.DataService.Importer.KYGaming.Utilities;
using System.Collections.Specialized;
using Polly;
using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.ResponseChecker;
using Fanex.Affiliate.DataService.Importer.KYGaming.ResponseChecker;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace Fanex.Affiliate.DataService.Importer.KYGaming
{
    public class KYGamingService : XmlImporterlServiceBase, IDataRequestByCurrency
    {
        protected readonly IApiClient _client;
        private const string _operateSubType = "6";
        private const int TIME_BETWEEN_TWO_REQUEST = 10000;
        private const int MAX_TIME_RANGE_TRANSACTION = 10;

        protected override int ProductId => Products.KyGamingId;

        private static Func<IAsyncPolicy<string>> GetDefaultRetryPolicy = () =>
        {
            const string tooFrequestPullRequest = "\"code\":43";
            const int numberOfRetries = 4;
            return Policy.HandleResult<String>(s => s.Contains(tooFrequestPullRequest))
                .WaitAndRetryAsync(numberOfRetries, retryAttempt => TimeSpan.FromMilliseconds(TIME_BETWEEN_TWO_REQUEST));
        };

        public KYGamingService(IApiClientFactory apiClientFactory = null, IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
            : base(appSetting, thirdPartyProductRepository)
        {
            var factory = apiClientFactory ?? new ApiClientFactory();
            _client = factory.CreateApiClient(_appSetting.GetValue("KYGamingApiUrl"), HttpMethod.Get);
        }

        public override async Task<IEnumerable<string>> GetRawDataListAsync(DateTime startTime, DateTime endTime)
        {
            var supportedCurrencies = _appSetting.GetSupportedKYGamingCurrency();
            return await GetRawDataListByCurrenciesAsync(startTime, endTime, supportedCurrencies);
        }

        public async Task<IEnumerable<string>> GetRawDataListByCurrenciesAsync(DateTime startTime, DateTime endTime, IEnumerable<string> currencies)
        {
            var datas = new List<string>();
            foreach (var currency in currencies)
            {
                var timeRanges = DateTimeHelper.GetProportionTimeRange(startTime, endTime, TimeSpan.FromMinutes(MAX_TIME_RANGE_TRANSACTION));
                foreach (var range in timeRanges)
                {
                    datas.AddRange(await GetRawDataListByCurrencyAsync(range.Item1, range.Item2, currency).ConfigureAwait(false));
                }

            }
            return datas;
        }

        public async Task<IEnumerable<string>> GetRawDataListByCurrencyAsync(DateTime startTime, DateTime endTime, string currency)
        {
            EnsureEndTimeLaterThanStartTime(startTime, endTime);

            var datas = new List<string>();
            var param = BuildParam(startTime, endTime, currency);

            datas.Add(await GetRawDataAsync(param));

            return datas;
        }

        public async Task<string> GetRawDataAsync(KYGamingApiParam param)
        {
            var queryValue = BuildQuery(param);

            return await GetDefaultRetryPolicy().ExecuteAsync(async () => await _client.GetResponseAsync(queryValue));          
        }

        public NameValueCollection BuildQuery(KYGamingApiParam param)
        {
            var queryValue = new NameValueCollection();

            queryValue.Add("agent", param.Agent.ToString());
            queryValue.Add("timestamp", param.TimeStamp.ToString());
            queryValue.Add("param", param.EncryptedParam);
            queryValue.Add("key", param.EncryptedKey);

            return queryValue;
        }

        public KYGamingApiParam BuildParam(DateTime startTime, DateTime endTime, string currency)
        {
            var agent = KYGamingHelper.GetAgent(currency);
            var deskey = KYGamingHelper.GetDESKey(currency);
            var md5Key = KYGamingHelper.GetMd5Key(currency);
            var timeStamp = DateTimeConverter.GetUnixTimeMilisecond(DateTime.Now);
            var operateSubType = _appSetting.GetValue("KYGamingOperateSubType") ?? _operateSubType;
            var startTimeMilliseconds = DateTimeConverter.GetUnixTimeMilisecond(startTime);
            var endTimeMilliseconds = DateTimeConverter.GetUnixTimeMilisecond(endTime);

            var paramText = $"s={operateSubType}&startTime={startTimeMilliseconds}&endTime={endTimeMilliseconds}";
            var keyText = $"{agent}{timeStamp}{md5Key}";

            var apiParam = new KYGamingApiParam
            {
                Agent = agent,
                TimeStamp = timeStamp,
                EncryptedKey = Encrypt.MD5Encrypt(keyText),
                EncryptedParam = Encrypt.AESEncrypt(paramText, deskey)
            };

            return apiParam;
        }

        protected override void CheckXmlResponseData(XDocument xml)
        {
            IThirdPartyResponseChecker checker = new KyGamingResponseChecker();
            checker.CheckXml(xml);
        }

        public override Task<XDocument> ConvertRawDataToXmlAsync(string raw)
        {
            var formatedJson = ReFormatJson(raw);
            var xml = XmlHelper.ParseToXDocumentFromJson(formatedJson);
            if (xml == null)
            {
                throw new ThirdPartyProductException($"FlowGaming - Cant convert json response to xml - {raw}");
            }
            return Task.FromResult(xml);
        }

        private static string ReFormatJson(string raw)
        {
            const string noDataCode = "16";
            const string successCode = "0";
            var rawJsonOject = JObject.Parse(raw);
            var code = rawJsonOject.SelectToken("$.d.code");
            if((string)code == noDataCode)
            {
                return raw;
            } 

            if((string)code == successCode)
            {
                var count = (int)rawJsonOject.SelectToken("$.d.count");
                var dataList = rawJsonOject.SelectToken("$.d.list");
                var items = ReArrangeDataToListItems(dataList, count);
                dataList.Replace(items);
            }
            
            return rawJsonOject.ToString();
        }

        private static JToken ReArrangeDataToListItems(JToken dataList, int count)
        {
            var items = new JArray();
            for (int i = 0; i < count; i++)
            {
                var item = new JObject();
                FillDataToItem(item, dataList, i);
                items.Add(item);
            }
            return items;
        }

        private static void FillDataToItem(JObject item, JToken dataSource, int rowNumber)
        {
            foreach (var pro in GetDataFieldNames())
            {
                item.Add(pro, dataSource.SelectToken($"$.{pro}[{rowNumber}]"));
            }
        }

        private static string[] GetDataFieldNames()
            => new[]
            {
                "GameID", "Accounts", "ServerID", "KindID",
                "TableID", "ChairID", "UserCount", "CardValue",
                "CellScore", "AllBet", "Profit", "Revenue",
                "GameStartTime", "GameEndTime", "ChannelID", "LineCode",
            };
    }
}
