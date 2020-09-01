using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace Fanex.Affiliate.DataService.Importer
{
    public sealed class TopazService : XmlImpoterlServiceBase
    {
        private const string DEFAULT_CURRENCY = "USD";
        private const string DEFAULT_TIME_ZONE = "UTC+8";
        private const string DEFAULT_LANGUAGE = "en";
        private const string AUTH_PATH = "/oauth/token";
        private const string FEED_DATA_PATH = "/v1/feed/transaction";
        private const int PAGE_SIZE = 2000;

        private string CompanyId => _appSetting.GetValue("TopazCompanyId");
        private string AuthUserName => _appSetting.GetValue("TopazAuthUserName");
        private string AuthSecret => _appSetting.GetValue("TopazAuthSecret");
        private string ApiUserName => _appSetting.GetValue("TopazApiUserName");
        private string ApiPassword => _appSetting.GetValue("TopazPassword");
        private string ApiUrl => _appSetting.GetValue("TopazApiUrl");
        private int IntervalTime => 60;
        private string DateTimeFormat => _appSetting.GetValue("TopazFormatDateTime");

        private readonly IHttpContentProvider ContentProvider = new FormUrlEncodedContentProvider();

        private readonly IApiClientFactory _apiClientFactory;

        protected override int ProductId => Products.TopazId;

        public TopazService(IApiClientFactory apiClientFactory = null, IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyRepository = null) : base(appSetting, thirdPartyRepository)
        {
            _apiClientFactory = apiClientFactory ?? new ApiClientFactory();
        }

        public override async Task<IEnumerable<string>> GetRawDataListAsync(DateTime startTime, DateTime endTime)
        {
            var token = await GetAuthTokenAsync();
            if (DateTime.Compare(startTime, endTime) >= 0)
            {
                throw new ThirdPartyProductException($"Flow Gaming - startDate {startTime} later or equal than endDate {endTime}");
            }

            var timeRanges = GetTimeRangeRequest(startTime, endTime);
            var datas = new List<string>(timeRanges.Count());

            foreach (var time in timeRanges)
            {
                var data = await GetRawDataAsync(time.Item1, time.Item2, token);
                datas.AddRange(data);
            }
            return datas;
        }

        private async Task<IEnumerable<string>> GetRawDataAsync(DateTime startTime, DateTime endTime, string token)
        {
            var datas = new List<string>();
            const string feedDataPath = FEED_DATA_PATH;
            var url = $"{ApiUrl}{feedDataPath}";
            var header = GetDataHeader(token);
            var api = _apiClientFactory.CreateApiClient(url, HttpMethod.Get, header);

            var pageCount = 1;
            var firstPageParam = GetDataParam(startTime, endTime, pageCount);
            var firstPage = await api.GetResponseAsync(firstPageParam);
            datas.Add(firstPage);

            var data = firstPage;
            while(IsHasAnotherPage(data))
            {
                var param = GetDataParam(startTime, endTime, ++pageCount);
                data = await api.GetResponseAsync(param);
                datas.Add(data);
            }    

            return datas;
        }

        private bool IsHasAnotherPage(string json)
        {
            var jsonObject = JObject.Parse(json);
            var datas = jsonObject.SelectToken("$.data");
            return datas != null && datas.Count() == PAGE_SIZE;
        }

        private NameValueCollection GetDataParam(DateTime startTime, DateTime endTime, int page)
            => new NameValueCollection
            {
                { "company_id", CompanyId },
                { "start_time", startTime.ToString(DateTimeFormat) },
                { "end_time", endTime.ToString(DateTimeFormat) },
                { "include_transfers", "true" },
                { "include_end_round", "true" },
                { "page", page.ToString() },
                { "page_size", PAGE_SIZE.ToString() }
            };

        private async Task<string> GetAuthTokenAsync()
        {
            var authResponse = await SendAuthenRequestAsync();
            var jsonObject = JObject.Parse(authResponse);
            var token = jsonObject.SelectToken("$.access_token");
            return token.ToString();
        }

        private async Task<string> SendAuthenRequestAsync()
        {
            var header = GetAuthHeader();
            var query = GetAuthParam();
            var authUrl = $"{ApiUrl}{AUTH_PATH}";
            var client = _apiClientFactory.CreateApiClient(authUrl, HttpMethod.Post, header);
            client.ContentProvider = ContentProvider;
            return await client.GetResponseAsync(query);
        }

        private HttpClientHeaderOption GetAuthHeader()
        {
            const string authScheme = "Basic";
            var authRaw = $"{AuthUserName}:{AuthSecret}";
            var plainAuthByte = Encoding.UTF8.GetBytes(authRaw);
            var base64AuthIdentity = Convert.ToBase64String(plainAuthByte);
            var authHeader = new AuthenticationHeaderValue(authScheme, base64AuthIdentity);

            var header = GetBasicHeader();
            header.AuthenHeader = authHeader;
            return header;
        }

        private HttpClientHeaderOption GetDataHeader(string token)
        {
            const string scheme = "Bearer";
            var authHeader = new AuthenticationHeaderValue(scheme, token);
            var header = GetBasicHeader();
            header.AuthenHeader = authHeader;
            return header;
        }

        private HttpClientHeaderOption GetBasicHeader()
            => new HttpClientHeaderOption
            {
                AcceptContent = "application/json;charset=UTF-8",
                Headers = new List<NameValueHeaderValue>
                {
                    new NameValueHeaderValue("X-DAS-TX-ID", Guid.NewGuid().ToString()),
                    new NameValueHeaderValue("X-DAS-TZ", DEFAULT_TIME_ZONE),
                    new NameValueHeaderValue("X-DAS-CURRENCY", DEFAULT_CURRENCY),
                    new NameValueHeaderValue("X-DAS-LANG", DEFAULT_LANGUAGE)
                }
            };

        private NameValueCollection GetAuthParam()
            => new NameValueCollection
            {
                { "grant_type", "password" },
                { "username", ApiUserName },
                { "password", ApiPassword }
            };

        private IEnumerable<Tuple<DateTime, DateTime>> GetTimeRangeRequest(DateTime startTime, DateTime endTime)
        {
            var span = new TimeSpan(0, IntervalTime, 0);
            return DateTimeHelper.GetProportionTimeRange(startTime, endTime, span);
        }

        protected override void CheckXmlResponseData(XDocument xml)
        {
        }

        public override Task<XDocument> ConvertRawDataToXmlAsync(string raw)
        {
            var xml = XmlHelper.ParseToXDocumentFromJson(raw);
            if (xml == null)
            {
                throw new ThirdPartyProductException($"FlowGaming - Cant convert json response to xml - {raw}");
            }
            return Task.FromResult(xml);
        }
    }
}
