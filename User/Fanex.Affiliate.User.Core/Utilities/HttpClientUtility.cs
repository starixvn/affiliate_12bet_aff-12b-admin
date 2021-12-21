using Fanex.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public class HttpClientUtility : IHttpClientUtility
    {
        private static readonly Lazy<HttpClient> LazyHttpClient = new Lazy<HttpClient>();
        private static readonly HttpClient httpClient = LazyHttpClient.Value;

        private readonly IAppSetting _appSetting;

        public HttpClientUtility(IAppSetting appSetting)
        {
            _appSetting = appSetting;
        }

        public async Task<string> GetContent(string path, string fileName)
        {
            var url = new Uri($"{_appSetting.ResourceDomain}{path}?fileName={fileName}");
            var content = await httpClient.GetAsync(url);
            if (content.IsSuccessStatusCode)
            {
                return await content.Content.ReadAsStringAsync();
            }
            else
            {
                await Logger.Log.ErrorAsync($"Request to take {path}?fileName={fileName} content is failed!");
            }
            return string.Empty;
        }
    }
}
