using Fanex.Affiliate.DataService.Importer.BigGaming.MetaModels;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.BigGaming
{
    public class BgLiveCasinoService : XmlImporterlServiceBase
    {
        protected override int ProductId => Products.BgLiveCasino;

        protected readonly IApiClientFactory _apiClientFactory;

        public BgLiveCasinoService(IApiClientFactory apiClientFactory = null, IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
            : base(appSetting, thirdPartyProductRepository)
        {
            _apiClientFactory = apiClientFactory ?? new ApiClientFactory();
        }

        public override async Task<IEnumerable<string>> GetRawDataListAsync(DateTime startTime, DateTime endTime)
        {
            var client = GetClient();
            var datas = new List<string>();
            var pageSize = int.Parse(_appSetting.GetValue("BigGamingPageSize"));
            var param = BuildBigGamingParam(startTime, endTime, pageSize);

            var rawData = await GetRawDataAsync(client, param);
            datas.Add(rawData);

            var totalPages = GetTotalPages(rawData, pageSize);

            for (var pageIndex = 2; pageIndex <= totalPages; pageIndex++)
            {
                param.PageIndex = pageIndex;
                datas.Add(await GetRawDataAsync(client, param));
            }

            return datas;
        }

        //private async Task<string> GetDataAsync()
        //{
        //    var path = Environment.CurrentDirectory;
        //    using (var stream = new StreamReader("../../App_Data/BgLiveCasino.json"))
        //    {
        //        return await stream.ReadToEndAsync();
        //    }
        //}

        private async Task<string> GetRawDataAsync(IApiClient client, BigGamingParam param)
        {
            var queryValues = BuildApiQuery(param);
            return await client.GetResponseAsync(queryValues).ConfigureAwait(false);
        }

        private int GetTotalPages(string json, int pageSize)
        {
            var dataObject = JObject.Parse(json);
            var totalItems = dataObject.SelectToken($"$.result.total")?.ToObject<int>() ?? 0;

            return (totalItems/pageSize) + 1;
        }

        private BigGamingParam BuildBigGamingParam(DateTimeOffset startTime, DateTimeOffset endTime, int pageSize)
        {
            var random = GetRandom();
            var sn = _appSetting.GetValue("BigGamingSn");
            var dateTimeFormat = _appSetting.GetValue("BigGamingFormatDate");

            return new BigGamingParam
            {
                Sn = sn,
                Sign = GetSign(random, sn),
                Random = random,
                FromDate = startTime.ToString(dateTimeFormat),
                ToDate = endTime.ToString(dateTimeFormat),
                PageIndex = 1,
                PageSize = pageSize.ToString(),
                GameType = _appSetting.GetValue("BigGamingGameType"),
                Method = _appSetting.GetValue("BgLiveCasinoMethod"),
            };
        }

        private NameValueCollection BuildApiQuery(BigGamingParam param)
        {
            return new NameValueCollection(9)
            {
                { "sn", param.Sn },
                { "sign", param.Sign },
                { "random", param.Random },
                { "startdate", param.FromDate },
                { "enddate", param.ToDate },
                { "pageIndex", param.PageIndex.ToString() },
                { "pageSize", param.PageSize },
                { "method", param.Method },
                { "gameType", param.GameType }
            };
        }

        private IApiClient GetClient()
        {
            var url = $"{_appSetting.GetValue("BgLiveCasinoApiURL")}";
            var client = _apiClientFactory.CreateApiClient(url, HttpMethod.Post);
            client.ContentProvider = new FormUrlEncodedContentProvider();

            return client;
        }

        private string GetSign(string random, string sn)
        {
            var secretKey = _appSetting.GetValue("BigGamingSecretKey");
            var keyText = $"{random}{sn}{secretKey}";

            return Encrypt.MD5Encrypt(keyText);
        }

        private string GetRandom()
        {
            return Guid.NewGuid().ToString();
        }

        public override Task<XDocument> ConvertRawDataToXmlAsync(string raw)
        {
            throw new NotImplementedException();
        }

        protected override void CheckXmlResponseData(XDocument xml)
        {
            throw new NotImplementedException();
        }
    }
}
