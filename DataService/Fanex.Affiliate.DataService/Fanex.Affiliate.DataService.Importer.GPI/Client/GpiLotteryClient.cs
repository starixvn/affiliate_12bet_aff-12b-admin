using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using Fanex.Affiliate.DataService.Importer.Core.Extension;
using Newtonsoft.Json;
using System.Net.Http;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using System;

namespace Fanex.Affiliate.DataService.Importer.Gpi.Client
{
    public class GpiLotteryClient : ApiClientBase
    {
        public override async Task<string> GetResponseAsync(string url, NameValueCollection query)
        {
            var client = HttpClientPooling.GetHttpClient(url);
            var dic = query.ToDictionary();
            var json = JsonConvert.SerializeObject(dic);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            client.Timeout = TimeSpan.FromMinutes(10);

            using (var response = await client.PostAsync(new Uri(url), content).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                return data;
            }
        }
    }
}
