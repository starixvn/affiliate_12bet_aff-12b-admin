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
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Specialized;

namespace Fanex.Affiliate.DataService.Importer.KYGaming
{
    public class KYGamingService : XmlImpoterlServiceBase, IDataRequestByCurrency
    {
        private const int DelayTimeMillisecond = 30000;

        protected readonly IApiClient _client;

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

        private async Task<string> GetRawDataAsync(KYGamingApiParam param)
        {
            var url = _appSetting.GetValue("KYGamingApiUrl");
            var queryValue = BuildQuery(param);

            return await _client.GetResponseAsync(url, queryValue);
        }

        private NameValueCollection BuildQuery(KYGamingApiParam param)
        {
            var queryValue = new NameValueCollection();

            queryValue.Add("agent", param.Agent.ToString());
            queryValue.Add("timestamp", param.TimeStamp);
            queryValue.Add("param", param.EncryptedParam);
            queryValue.Add("key", param.Key);

            return queryValue;
        }

        private KYGamingApiParam BuildParam(DateTime startTime, DateTime endTime, string currency)
        {
            var apiParam = new KYGamingApiParam
            {
                StartTime = startTime,
                EndTime = endTime,
                Agent = KYGamingHelper.GetAgent(currency),
                Deskey = KYGamingHelper.GetDESKey(currency),
                Md5Key = KYGamingHelper.GetMd5Key(currency),
                OperateSubType = Convert.ToInt32(_appSetting.GetValue("KYGamingOperateSubType"))
            };

            apiParam.EncryptedParam = AESEncrypt(apiParam);
            apiParam.Key = MD5Encrypt(apiParam);

            return apiParam;
        }

        private string AESEncrypt(KYGamingApiParam param)
        {
            byte[] encrypted;
            var deskey = Encoding.ASCII.GetBytes(param.Deskey);
            var originalText = $"s={param.OperateSubType}&startTime={param.StartTimeString}&endTime={param.EndTimeString}";

            using (var provider = new AesCryptoServiceProvider() { Key = deskey })
            {
                using (var encryptor = provider.CreateEncryptor())
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (var swEncrypt = new StreamWriter(csEncrypt))
                            {
                                //Write all data to the stream.
                                swEncrypt.Write(originalText);
                            }
                            encrypted = msEncrypt.ToArray();
                        }
                    }
                }
            }

            return BitConverter.ToString(encrypted).Replace("-", string.Empty).ToLowerInvariant();
        }

        private string MD5Encrypt(KYGamingApiParam param)
        {
            byte[] encrypted;
            var originalText = param.Agent + param.TimeStamp + param.Md5Key;

            using (var md5provider = new MD5CryptoServiceProvider())
            {
                encrypted = md5provider.ComputeHash(Encoding.UTF8.GetBytes(originalText));
            }

            return BitConverter.ToString(encrypted).Replace("-", string.Empty).ToLowerInvariant();
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
