using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using Fanex.Affiliate.DataService.Importer.Core.Extension;
using Newtonsoft.Json;
using System.Net.Http;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;

namespace Fanex.Affiliate.DataService.Importer.Client
{
    public class GpiLotteryClient : ApiClientBase
    {
        public GpiLotteryClient() : base()
        {
        }
        public override async Task<string> GetResponseAsync(string url, NameValueCollection query)
        {
            var client = HttpClientPooling.GetHttpClient(url);
            var dic = query.ToDictionary();
            var json = JsonConvert.SerializeObject(dic);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(url, content).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                return data;
            }
        }
    }
}
