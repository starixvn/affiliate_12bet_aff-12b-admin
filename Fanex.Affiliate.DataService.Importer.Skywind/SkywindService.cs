using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.Skywind.MetaModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.Skywind
{
    public class SkywindService : XmlImporterlServiceBase
    {
        protected readonly IApiClientFactory _apiClientFactory;
        protected override int ProductId => Products.Skywind;

        public SkywindService(IApiClientFactory apiClientFactory = null, IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
            : base(appSetting, thirdPartyProductRepository)
        {
            _apiClientFactory = apiClientFactory ?? new ApiClientFactory();
        }

        public override async Task<IEnumerable<string>> GetRawDataListAsync(DateTime startTime, DateTime endTime)
        {
            return await GetRawDataAsync(startTime, endTime).ConfigureAwait(false);
        }

        private async Task<IEnumerable<string>> GetRawDataAsync(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            var client = await GetClient();
            var limit = int.Parse(_appSetting.GetValue("SkywindLimit"));
            var skywindParam = BuildSkywindParam(startTime, endTime, limit);

            var offset = 0;
            var hasAnotherPage = true;
            var datas = new List<string>();
            while(hasAnotherPage)
            {
                var queryValues = BuildApiQuery(skywindParam, offset);

                var rawData = await client.GetResponseAsync(queryValues).ConfigureAwait(false);
                offset++;
                datas.Add(rawData);
                hasAnotherPage = CheckHasAnotherPage(rawData, limit);
            }

            return datas;
        }

        private async Task<IApiClient> GetClient()
        {
            var token = await GetTokenAsync().ConfigureAwait(false);

            var header = new HttpClientHeaderOption
            {
                AcceptContent = "application/json;charset=UTF-8",
                Headers = new List<NameValueHeaderValue> {
                        new NameValueHeaderValue("X-ACCESS-TOKEN", token)
                    }
            };
            var url = $"{_appSetting.GetValue("SkywindApiUrl")}";
            return _apiClientFactory.CreateApiClient(url, HttpMethod.Get, header);
        }

        private SkywindParam BuildSkywindParam(DateTimeOffset startTime, DateTimeOffset endTime, int limit)
        {
            var dateTimeFormat = _appSetting.GetValue("SkywindFormatDateTime");

            return new SkywindParam {
                Limit = limit.ToString(),
                SortBy = _appSetting.GetValue("SkywindSortBy"),
                FromDate = startTime.ToString(dateTimeFormat),
                ToDate = endTime.ToString(dateTimeFormat)
            };
        }

        private NameValueCollection BuildApiQuery(SkywindParam param, int offset)
        {
            return new NameValueCollection(5)
            {
                { "offset", (offset * 100).ToString() },
                { "limit", param.Limit },
                { "sortBy", param.SortBy },
                { "ts__gte", param.FromDate },
                { "ts__lt", param.ToDate }
            };
        }

        private async Task<string> GetTokenAsync()
        {
            var client = _apiClientFactory.CreateApiClient(_appSetting.GetValue("SkywindLoginUrl"), HttpMethod.Post);

            var queryValues = new NameValueCollection
            {
                { "secretKey", _appSetting.GetValue("SkywindSecretKey") },
                { "username", _appSetting.GetValue("SkywindUserName") },
                { "password", _appSetting.GetValue("SkywindPassword") }
            };

            var result = await client.GetResponseAsync(queryValues).ConfigureAwait(false);

            return GetTokenFromRawData(result);
        }

        private static string GetTokenFromRawData(string raw)
        {
            var rawJsonOject = JObject.Parse(raw);

            return rawJsonOject.SelectToken("$.accessToken").ToString();
        }

        private bool CheckHasAnotherPage(string json, int limit)
        {
            var xml = ConvertRawDataToXml(json, false);
            CheckXmlResponseData(xml);
            var count = xml.Root.Elements("Tickets").Count();
            return count == limit;
        }

        protected override void CheckXmlResponseData(XDocument xml)
        {
            if (xml.Root.IsEmpty)
                return;
            var elem = xml.Root.Element("Tickets");
            if (elem == null)
                throw new ThirdPartyProductException($"Skywind Product - Not correct format {xml}");
        }

        public override Task<XDocument> ConvertRawDataToXmlAsync(string raw)
        {
            return Task.FromResult(ConvertRawDataToXml(raw));
        }

        private XDocument ConvertRawDataToXml(string raw, bool shouldRearrangeItems = true)
        {
            var formatedJson = ReFormatJson(raw, shouldRearrangeItems);
            var xml = XmlHelper.ParseToXDocumentFromJson(formatedJson);
            if (xml == null)
            {
                throw new ThirdPartyProductException($"Skywind Product - Can't convert json response to xml - {raw}");
            }
            return xml;
        }

        private static string ReFormatJson(string raw, bool shouldRearrangeItems)
        {
            var dataList = JToken.Parse(raw);
            var items = shouldRearrangeItems ? RearrangeDataToListItems(dataList) : dataList;

            return $"{{Tickets:{items}}}";
        }

        private static JToken RearrangeDataToListItems(JToken dataList)
        {
            var items = new JArray();
            for (int i = 0; i < dataList.Count(); i++)
            {
                var item = new JObject();
                FillDataToItem(item, dataList[i]);
                items.Add(item);
            }
            return items;
        }

        private static void FillDataToItem(JObject item, JToken betItem)
        {
            foreach (var pro in GetDataFieldNames())
            {
                var data = betItem.SelectToken($"$.{pro}");

                item.Add(pro, data);
            }
        }

        private static string[] GetDataFieldNames()
            => new[]
            {
                "roundId", "playerCode", "currency", "bet", "revenue", "ts",
            };
    }
}
