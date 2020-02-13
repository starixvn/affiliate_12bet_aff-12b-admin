using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using System;
using System.Threading.Tasks;
using System.Collections.Specialized;
using Fanex.Affiliate.DataService.Importer.Core.Extension;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;

namespace Fanex.Affiliate.DataService.Importer.FlowGaming.Client
{
    public class TopazClient : ApiClientBase
    {
        public override async Task<string> GetResponseAsync(string url, NameValueCollection query)
        {
            var client = HttpClientPooling.GetHttpClient(url);
            var uri = new Uri($"{url}?{query.ToQuery()}");

            using (var response = await client.GetAsync(uri).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                return data;
            }
        }

    }
}
