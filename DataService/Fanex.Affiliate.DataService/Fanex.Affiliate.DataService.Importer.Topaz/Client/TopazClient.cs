using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using System;
using System.Threading.Tasks;
using System.Collections.Specialized;
using Fanex.Affiliate.DataService.Importer.Core.Extension;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Polly;
using System.Net.Http;

namespace Fanex.Affiliate.DataService.Importer.FlowGaming.Client
{
    public class TopazClient : ApiClientBase
    {
        private static IAsyncPolicy<string> _policy;
        private const string CALL_LIMIT_EXCEEDED = "CALL_LIMIT_EXCEEDED";
        //60s to request next data for same brand_id request
        private const int TIME_BETWEEN_TWO_REQUEST = 60000;
        protected static IAsyncPolicy<string> RetryPolicy
        {
            get 
            {
                if (_policy == null)
                {
                    _policy = GetPolicy();
                }
                return _policy;
            }
        }

        public override async Task<string> GetResponseAsync(string url, NameValueCollection query)
        {
            var policy = RetryPolicy;

            var client = HttpClientPooling.GetHttpClient(url);
            var uri = new Uri($"{url}?{query.ToQuery()}");

            var result = await policy.ExecuteAsync(async () =>
            {
                using (var response = await client.GetAsync(uri))
                {
                    response.EnsureSuccessStatusCode();
                    var data = await response.Content.ReadAsStringAsync();
                    return data;
                }
            });
            return result;
        }

        private static IAsyncPolicy<String> GetPolicy()
        {
            var retryPolicy = Policy.HandleResult<String>(s => s.Contains(CALL_LIMIT_EXCEEDED))
                .WaitAndRetryForeverAsync(retryAttempt => TimeSpan.FromMilliseconds(TIME_BETWEEN_TWO_REQUEST));
            var bulkHeadPolicy = Policy.BulkheadAsync<String>(1, 10)
                .WrapAsync(retryPolicy);
            return bulkHeadPolicy.WithPolicyKey("TopazPolicy");           
        }
    }
}
