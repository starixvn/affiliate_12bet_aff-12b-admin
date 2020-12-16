using Fanex.Affiliate.DataService.Importer.BTi.MetaModels;
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
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.BTi
{
    public class BTiService : XmlImpoterlServiceBase
    {
        protected readonly IApiClient _client;
        protected readonly IApiClientFactory _apiClientFactory;

        public BTiService(IApiClientFactory apiClientFactory = null, IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null) : base(appSetting, thirdPartyProductRepository)
        {
            _apiClientFactory = apiClientFactory ?? new ApiClientFactory();
            _client = _apiClientFactory.CreateApiClient(_appSetting.GetValue("BTiApiUrl"), HttpMethod.Post);
        }

        protected override int ProductId => Products.BTiId;

        public override async Task<IEnumerable<string>> GetRawDataListAsync(DateTime startTime, DateTime endTime)
        {
            var paramToRequest = BuildParam(startTime, endTime);
            var datas = new List<string>
            {
                await GetRawDataAsync(paramToRequest).ConfigureAwait(false)
            };

            return datas;
        }

        private async Task<string> GetRawDataAsync(BTiParam paramToRequest)
        {
            var queryValues = BuildQueryParam(paramToRequest);
            return await _client.GetResponseAsync(queryValues).ConfigureAwait(false);
        }

        private NameValueCollection BuildQueryParam(BTiParam paramToRequest)
        {
            var dateTimeFormat = _appSetting.GetValue("BTiFormatDateTime");

            var queryValues = new NameValueCollection(3)
            {
                { "token", paramToRequest.Token },
                { "from", paramToRequest.From.ToString(dateTimeFormat) },
                { "to", paramToRequest.To.ToString(dateTimeFormat) }
            };

            return queryValues;
        }

        private BTiParam BuildParam(DateTime startTime, DateTime endTime)
        {
            var token = GetTokenAsync();
            return new BTiParam
            {
                Token = token.Result,
                From = startTime,
                To = endTime
            };
        }

        private async Task<string> GetTokenAsync()
        {
            var client = _apiClientFactory.CreateApiClient(_appSetting.GetValue("BTiTokenUrl"), HttpMethod.Post);
            var queryValues = BuildQueryTokenParam();
            var rawData = await client.GetResponseAsync(queryValues).ConfigureAwait(false);

            return GetTokenFromRawData(rawData);
        }

        private static string GetTokenFromRawData(string raw)
        {
            var rawJsonOject = JObject.Parse(raw);

            return (string)rawJsonOject.SelectToken("$.token");
        }

        private NameValueCollection BuildQueryTokenParam()
        {
            var agentUserName = _appSetting.GetValue("BTiAgentUserName");
            var agentPassword = _appSetting.GetValue("BTiAgentPassword");

            var queryValues = new NameValueCollection(2)
            {
                { "agentUserName", agentUserName },
                { "agentPassword", agentPassword }
            };
            return queryValues;
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
                throw new ThirdPartyProductException($"BTi Product - Cant convert json response to xml - {raw}");
            }
            return Task.FromResult(xml);
        }

        private static string ReFormatJson(string raw)
        {
            const string tokenExpiredCode = "-1000";
            const string successCode = "0";
            var rawJsonOject = JObject.Parse(raw);
            var code = rawJsonOject.SelectToken("$.errorCode");
            if ((string)code == tokenExpiredCode)
            {
                return raw;
            }

            if ((string)code == successCode)
            {
                var count = (int)rawJsonOject.SelectToken("$.count");
                var dataList = rawJsonOject.SelectToken("$.Bets");
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
                "BetID", "UserName", "TotalStake", "ValidStake",
                "PL", "BetStatus", "BetSettleDate",
            };
    }
}
