using System.Net.Http;

namespace Fanex.Affiliate.DataService.Importer.Core.Infrastructure
{
    public interface IApiClientFactory
    {
        IApiClient CreateApiClient(string url, HttpMethod method = null, HttpClientHeaderOption headers = null);

    }
}
