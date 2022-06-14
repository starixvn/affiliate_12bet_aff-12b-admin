using Fanex.Affiliate.DataService.Importer.BTi.ResponseChecker;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.ResponseChecker;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.BTi
{
    public class BTiService : XmlImporterlServiceBase
    {
        protected readonly IApiClientFactory _apiClientFactory;

        public BTiService(IApiClientFactory apiClientFactory = null, IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
            : base(appSetting, thirdPartyProductRepository)
        {
            _apiClientFactory = apiClientFactory ?? new ApiClientFactory();
        }

        protected override int ProductId => Products.BTiId;

        public override async Task<IEnumerable<string>> GetRawDataListAsync(DateTime startTime, DateTime endTime)
        {
            var datas = new List<string>
            {
                await GetRawDataAsync(startTime, endTime).ConfigureAwait(false)
            };

            return datas;
        }

        private async Task<string> GetRawDataAsync(DateTime startTime, DateTime endTime)
        {
            var dateTimeFormat = _appSetting.GetValue("BTiFormatDateTime");
            var queryValues = new NameValueCollection
            {
                { "From", startTime.ToString(dateTimeFormat) },
                { "To", endTime.ToString(dateTimeFormat) }
            };

            var token = await GetTokenAsync().ConfigureAwait(false);

            var url = $"{_appSetting.GetValue("BTiApiUrl")}?token={token}";
            var client = _apiClientFactory.CreateApiClient(url, HttpMethod.Post);

            return await client.GetResponseAsync(queryValues).ConfigureAwait(false);
        }

        private async Task<string> GetTokenAsync()
        {
            var client = _apiClientFactory.CreateApiClient(_appSetting.GetValue("BTiTokenUrl"), HttpMethod.Post);

            var queryValues = new NameValueCollection
            {
                { "agentUserName", _appSetting.GetValue("BTiAgentUserName") },
                { "agentPassword", _appSetting.GetValue("BTiAgentPassword") }
            };

            var result = await client.GetResponseAsync(queryValues).ConfigureAwait(false);

            return GetTokenFromRawData(result);
        }

        private static string GetTokenFromRawData(string raw)
        {
            var rawJsonOject = JObject.Parse(raw);

            return rawJsonOject.SelectToken("$.token").ToString();
        }

        protected override void CheckXmlResponseData(XDocument xml)
        {
            IThirdPartyResponseChecker checker = new BTiResponseChecker();
            checker.CheckXml(xml);
        }

        public override Task<XDocument> ConvertRawDataToXmlAsync(string raw)
        {
            var formatedJson = ReFormatJson(raw);
            var xml = XmlHelper.ParseToXDocumentFromJson(formatedJson);
            if (xml == null)
            {
                throw new ThirdPartyProductException($"BTi Product - Can't convert json response to xml - {raw}");
            }
            return Task.FromResult(xml);
        }

        private static string ReFormatJson(string raw)
        {
            const string successCode = "0";
            var rawJsonOject = JObject.Parse(raw);
            var code = rawJsonOject.SelectToken("$.errorCode");

            if ((string)code == successCode)
            {
                var dataList = rawJsonOject.SelectToken("$.Bets");
                var items = ReArrangeDataToListItems(dataList);
                dataList.Replace(items);
            }

            return rawJsonOject.ToString();
        }

        private static JToken ReArrangeDataToListItems(JToken dataList)
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
                "PurchaseID", "UserName", "TotalStake", "ValidStake",
                "PL", "BetStatus", "BetSettledDate",
            };
    }
}
