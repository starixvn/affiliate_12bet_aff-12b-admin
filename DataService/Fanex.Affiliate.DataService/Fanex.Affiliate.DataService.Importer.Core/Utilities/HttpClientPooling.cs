using HttpClientFactory;
using HttpClientFactory.Impl;
using System;
using System.Net.Http;

namespace Fanex.Affiliate.DataService.Importer.Core.Utilities
{
    public static class HttpClientPooling
    {
        private static Lazy<IHttpClientFactory> _httpClientFactory = new Lazy<IHttpClientFactory>(() => new PerHostHttpClientFactory(), true);

        public static IHttpClientFactory CurrentFactory => _httpClientFactory.Value;

        public static HttpClient GetHttpClient(string url)
        {
            return CurrentFactory.GetHttpClient(url);
        }

        public static void SetCurrentHttpClienFactory(IHttpClientFactory factory)
        {
            if (_httpClientFactory.IsValueCreated)
            {
                _httpClientFactory.Value.Dispose();
            }
            _httpClientFactory = new Lazy<IHttpClientFactory>(() => factory, true);
        }

        public static void ClearAllHttpClient()
        {
            if (_httpClientFactory.IsValueCreated)
            {
                CurrentFactory.Dispose();
                _httpClientFactory = new Lazy<IHttpClientFactory>(() => new PerHostHttpClientFactory(), true);
            }
        }
    }
}
