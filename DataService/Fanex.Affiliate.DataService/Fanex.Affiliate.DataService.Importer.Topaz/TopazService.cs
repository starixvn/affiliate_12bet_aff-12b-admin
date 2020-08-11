using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.ResponseChecker;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Fanex.Affiliate.DataService.Importer.FlowGaming.ResponseChecker;
using Polly;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;

namespace Fanex.Affiliate.DataService.Importer
{
    public sealed class TopazService : XmlImpoterlServiceBase
    {
        private const string CALL_LIMIT_EXCEEDED = "CALL_LIMIT_EXCEEDED";
        //60s to request next data for same brand_id request
        private const int TIME_BETWEEN_TWO_REQUEST = 60000;
        private const int MAX_TIME_RANGE_TRANSACTION = 15;
        private const string DEFAULT_CURRENCY = "USD";
        private const string DEFAULT_TIME_ZONE = "UTC";
        private const string DEFAULT_LANGUAGE = "en";
        private const string AUTH_PATH = "/oauth/token";

        private string CompanyId => _appSetting.GetValue("TopazCompanyId");
        private string AuthUserName => _appSetting.GetValue("TopazAuthUserName");
        private string AuthSecret => _appSetting.GetValue("TopazAuthSecret");
        private string ApiUserName => _appSetting.GetValue("TopazApiUserName");
        private string ApiPassword => _appSetting.GetValue("TopazPassword");
        private string ApiUrl => _appSetting.GetValue("TopazApiUrl");

        private static Dictionary<string, IAsyncPolicy<string>> _policies = new Dictionary<string, IAsyncPolicy<string>>();

        private readonly IHttpContentProvider ContentProvider = new FormUrlEncodedContentProvider();

        public bool IsTestMode { get; set; } = false;
        public int NextRequestWaitTime => IsTestMode ? 1 : TIME_BETWEEN_TWO_REQUEST;

        private readonly IApiClientFactory _apiClientFactory;

        protected override int ProductId => Products.TopazId;

        public TopazService(IApiClientFactory apiClientFactory = null, IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyRepository = null) : base(appSetting, thirdPartyRepository)
        {
            _apiClientFactory = apiClientFactory ?? new ApiClientFactory();
        }

        public override async Task<IEnumerable<string>> GetRawDataListAsync(DateTime startTime, DateTime endTime)
        {
            var authenResponse = await SendAuthenRequestAsync();
            //if (DateTime.Compare(startTime, endTime) >= 0)
            //{
            //    throw new ThirdPartyProductException($"Flow Gaming - startDate {startTime} later or equal than endDate {endTime}");
            //}

            //var timeRanges = GetTimeRangeRequest(startTime, endTime);
            //var datas = new List<string>(timeRanges.Count());

            //foreach (var time in timeRanges)
            //{
            //    var data = await GetRawDataAsync(time.Item1, time.Item2);
            //    if (IsExceedCallLimit(data))
            //    {
            //        await Task.Delay(NextRequestWaitTime);
            //        data = await GetRawDataAsync(time.Item1, time.Item2);
            //    }
            //    datas.Add(data);
            //}
            return new List<string> { authenResponse };
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

        private static IEnumerable<Tuple<DateTime, DateTime>> GetTimeRangeRequest(DateTime startTime, DateTime endTime)
        {
            var span = new TimeSpan(0, MAX_TIME_RANGE_TRANSACTION, 0);
            return DateTimeHelper.GetProportionTimeRange(startTime, endTime, span);
        }

        private NameValueCollection GetQueryParam(DateTime startTime, DateTime endTime)
        {
            var dateTimeFormat = _appSetting.GetValue("TopazFormatDateTime");
            var result = new NameValueCollection(5);
            result.Add("brandId", _appSetting.GetValue("TopazBrandId"));
            result.Add("brandPassword", _appSetting.GetValue("TopazBrandPassword"));
            result.Add("startTime", startTime.ToString(dateTimeFormat));
            result.Add("endTime", endTime.ToString(dateTimeFormat));
            return result;
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
