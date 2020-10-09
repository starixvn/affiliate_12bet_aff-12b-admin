using System.Net.Http;

namespace Fanex.Affiliate.DataService.Importer.Core.Infrastructure
{
    public class ApiClientFactory : IApiClientFactory
    {
        public IApiClient CreateApiClient(string url, HttpMethod method = null, HttpClientHeaderOption headers = null)
            => new ApiClient(url, method, headers);
    }
}
