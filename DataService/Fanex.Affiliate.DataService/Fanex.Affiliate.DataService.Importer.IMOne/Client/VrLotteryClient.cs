using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading;
using Fanex.Affiliate.DataService.Importer.Core.Extension;
using Newtonsoft.Json;

namespace Fanex.Affiliate.DataService.Importer.IMOne.Client
{
    public class VrLotteryClient : ApiClientBase, IConnectionCountable
    {
        private static Lazy<Dictionary<string, int>> _requestCountMapping = new Lazy<Dictionary<string, int>>(() => new Dictionary<string, int>(), true);
        private Dictionary<string, int> RequestCountMapping => _requestCountMapping.Value;

        public VrLotteryClient(HttpClient client = null) : base(client) { }

        public override async Task<string> GetResponseAsync(string url, NameValueCollection query)
        {
            IncreaseConnection(url);
            var dictionary = query.ToDictionary();
            var json = JsonConvert.SerializeObject(dictionary);
            var sendContent = new StringContent(json, Encoding.UTF8, "application/json");
            var resposeMessage = await Client.PostAsync(url, sendContent);
            DecreaseConnection(url);
            resposeMessage.EnsureSuccessStatusCode();
            var data = await resposeMessage.Content.ReadAsStringAsync();
            resposeMessage.Dispose();
            return data;
        }

        public int TotalConnection(string url)
        {
            return RequestCountMapping.ContainsKey(url) ? RequestCountMapping[url] : 0;
        }

        private void DecreaseConnection(string url)
        {
            lock (RequestCountMapping)
            {
                if (RequestCountMapping.ContainsKey(url))
                {
                    RequestCountMapping[url]--;
                    if (RequestCountMapping[url] < 0)
                        RequestCountMapping[url] = 0;
                }
            }
        }

        private void IncreaseConnection(string url)
        {
            lock (RequestCountMapping)
            {
                if (RequestCountMapping.ContainsKey(url))
                    RequestCountMapping[url]++;
                else
                    RequestCountMapping.Add(url, 1);
            }
        }

        ~VrLotteryClient()
        {
            Dispose(false);
        }

    }

}
