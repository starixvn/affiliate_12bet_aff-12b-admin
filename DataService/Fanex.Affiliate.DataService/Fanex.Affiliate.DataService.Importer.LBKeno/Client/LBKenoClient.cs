using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Net.Http;
using Fanex.Affiliate.DataService.Importer.Core.Extension;
using Newtonsoft.Json;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;

namespace Fanex.Affiliate.DataService.Importer.LBKeno.Client
{
    public class LBKenoClient : ApiClientBase
    {
        public LBKenoClient() : base() { }

        public override async Task<string> GetResponseAsync(string url, NameValueCollection query)
        {
            var client = HttpClientPooling.GetHttpClient(url);
            var uri = new Uri($"{url}?{query.ToQuery()}");
            using (var response = await client.GetAsync(uri).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

    }
}
