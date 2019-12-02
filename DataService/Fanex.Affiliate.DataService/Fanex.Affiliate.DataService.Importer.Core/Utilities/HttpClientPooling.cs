using HttpClientFactory;
using HttpClientFactory.Impl;
using System.Net.Http;

namespace Fanex.Affiliate.DataService.Importer.Core.Utilities
{
    public static class HttpClientPooling
    {
        private static IHttpClientFactory _httpClientFactory;

        private static IHttpClientFactory HttpClientFactory => _httpClientFactory ?? new PerHostHttpClientFactory();

        public static HttpClient GetHttpClient(string url)
        {
            return HttpClientFactory.GetHttpClient(url);
        }

        public static void ClearAllHttpClient()
        {
            _httpClientFactory.Dispose();
            _httpClientFactory = null;
        }
    }
}
