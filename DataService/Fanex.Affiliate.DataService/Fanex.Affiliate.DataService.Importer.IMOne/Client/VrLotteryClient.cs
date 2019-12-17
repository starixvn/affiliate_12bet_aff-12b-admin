using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Net.Http;
using Fanex.Affiliate.DataService.Importer.Core.Extension;
using Newtonsoft.Json;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;

namespace Fanex.Affiliate.DataService.Importer.IMOne.Client
{
    public class VrLotteryClient : ApiClientBase
    {
        public override async Task<string> GetResponseAsync(string url, NameValueCollection query)
        {
            var client = HttpClientPooling.GetHttpClient(url);
            var dictionary = query.ToDictionary();
            var json = JsonConvert.SerializeObject(dictionary);
            var sendContent = new StringContent(json, Encoding.UTF8, "application/json");

            using (var resposeMessage = await client.PostAsync(new Uri(url), sendContent).ConfigureAwait(false))
            {
                resposeMessage.EnsureSuccessStatusCode();
                return await resposeMessage.Content.ReadAsStringAsync();
            }
        }

    }

}
