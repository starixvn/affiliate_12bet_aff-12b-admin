using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.KY.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using Fanex.Affiliate.DataService.Importer.IMOne.Extension;
using Fanex.Affiliate.DataService.Importer.KYGaming.MetaModels;
using Fanex.Affiliate.DataService.Importer.KYGaming.Utilities;
using System.Collections.Specialized;

namespace Fanex.Affiliate.DataService.Importer.KYGaming
{
    public class KYGamingService : XmlImpoterlServiceBase, IDataRequestByCurrency
    {
        protected readonly IApiClient _client;
        private const string _operateSubType = "6";

        protected override int ProductId => throw new NotImplementedException();

        public KYGamingService(IApiClient client = null, IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
            : base(appSetting, thirdPartyProductRepository)
        {
            _client = client ?? new KYGamingClient();
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
                datas.AddRange(await GetRawDataListByCurrencyAsync(startTime, endTime, currency).ConfigureAwait(false));
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
            var url = _appSetting.GetValue("KYGamingApiUrl");
            var queryValue = BuildQuery(param);

            return await _client.GetResponseAsync(url, queryValue);
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
            var timeStamp = DateTimeConverter.GetMilliseconds(DateTime.Now);
            var operateSubType = _appSetting.GetValue("KYGamingOperateSubType") ?? _operateSubType;
            var startTimeMilliseconds = DateTimeConverter.GetMilliseconds(startTime);
            var endTimeMilliseconds = DateTimeConverter.GetMilliseconds(endTime);

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
            throw new NotImplementedException();
        }

        public override Task<XDocument> ConvertRawDataToXmlAsync(string raw)
        {
            throw new NotImplementedException();
        }
    }
}
