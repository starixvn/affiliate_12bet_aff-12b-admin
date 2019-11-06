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

namespace Fanex.Affiliate.DataService.Importer.IMOne.Client
{
    public class VrLotteryClient : IApiClient, IConnectionCountable
    {
        private static Lazy<Dictionary<string, int>> _requestCountMapping = new Lazy<Dictionary<string, int>>(true);
        private Dictionary<string, int> RequestCountMapping => _requestCountMapping.Value;

        public string GetResponse(string url, NameValueCollection query)
        {
            return GetResponseAsync(url, query).ToString();
        }

        public async Task<string> GetResponseAsync(string url, NameValueCollection query)
        {
            using (var client = new HttpClient())
            {
                IncreaseConnection(url);
                var sendContent = new FormUrlEncodedContent(query.ToKeyValuePairCollection());
                var resposeMessage =  await client.PostAsync(url, sendContent);
                DecreaseConnection(url);
                resposeMessage.EnsureSuccessStatusCode();

                return await resposeMessage.Content.ReadAsStringAsync();
            }
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
            lock(RequestCountMapping)
            {
                if (RequestCountMapping.ContainsKey(url))
                    RequestCountMapping[url]++;
                else
                    RequestCountMapping.Add(url, 1);
            }
        }
    }

}
