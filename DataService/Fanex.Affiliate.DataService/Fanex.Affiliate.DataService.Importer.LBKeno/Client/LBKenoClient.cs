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

namespace Fanex.Affiliate.DataService.Importer.LBKeno.Client
{
    public class LBKenoClient : ApiClientBase
    {
        public LBKenoClient(HttpClient client = null) : base(client) { }

        public override async Task<string> GetResponseAsync(string url, NameValueCollection query)
        {
            //var dic = query.ToDictionary();
            //var json = JsonConvert.SerializeObject(dic);
            //var content = new StringContent(json,Encoding.UTF8, "application/json");
            var uri = new Uri($"{url}?{query.ToQuery()}");
            //var content = new FormUrlEncodedContent(query.ToKeyValuePairCollection());
            var response = await Client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        ~LBKenoClient()
        {
            Dispose(false);
        }

    }
}
