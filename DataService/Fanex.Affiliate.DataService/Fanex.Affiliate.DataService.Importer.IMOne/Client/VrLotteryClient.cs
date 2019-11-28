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

namespace Fanex.Affiliate.DataService.Importer.IMOne.Client
{
    public class VrLotteryClient : ApiClientBase, IConnectionCountable
    {
        private static Lazy<Dictionary<string, int>> _requestCountMapping = new Lazy<Dictionary<string, int>>(() => new Dictionary<string, int>(), true);
        private static Dictionary<string, int> RequestCountMapping => _requestCountMapping.Value;

        public override async Task<string> GetResponseAsync(string url, NameValueCollection query)
        {
            var client = HttpClientPooling.GetHttpClient(url);
            var dictionary = query.ToDictionary();
            var json = JsonConvert.SerializeObject(dictionary);
            var sendContent = new StringContent(json, Encoding.UTF8, "application/json");

            IncreaseConnection(url);
            using (var resposeMessage = await client.PostAsync(new Uri(url), sendContent).ConfigureAwait(false))
            {

                DecreaseConnection(url);
                resposeMessage.EnsureSuccessStatusCode();
                return await resposeMessage.Content.ReadAsStringAsync();
            }
        }

        public int TotalConnection(string url)
        {
            return RequestCountMapping.ContainsKey(url) ? RequestCountMapping[url] : 0;
        }

        private static void DecreaseConnection(string url)
        {
            lock (RequestCountMapping)
            {
                if (RequestCountMapping.ContainsKey(url))
                {
                    RequestCountMapping[url]--;
                    if (RequestCountMapping[url] < 0)
                    {
                        RequestCountMapping[url] = 0;
                    }
                }
            }
        }

        private static void IncreaseConnection(string url)
        {
            lock (RequestCountMapping)
            {
                if (RequestCountMapping.ContainsKey(url))
                {
                    RequestCountMapping[url]++;
                }
                else
                {
                    RequestCountMapping.Add(url, 1);
                }
            }
        }


    }

}
