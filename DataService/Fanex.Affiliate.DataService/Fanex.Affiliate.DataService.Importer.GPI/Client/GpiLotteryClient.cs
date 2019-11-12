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

namespace Fanex.Affiliate.DataService.Importer.Client
{
    public class GpiLotteryClient : ApiClientBase
    {
        public GpiLotteryClient() : base()
        {
        }
        public override async Task<string> GetResponseAsync(string url, NameValueCollection query)
        {
            var dic = query.ToDictionary();
            var json = JsonConvert.SerializeObject(dic);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            response.Dispose();
            return data;
        }

        ~GpiLotteryClient()
        {
            Dispose(false);
        }
    }
}
