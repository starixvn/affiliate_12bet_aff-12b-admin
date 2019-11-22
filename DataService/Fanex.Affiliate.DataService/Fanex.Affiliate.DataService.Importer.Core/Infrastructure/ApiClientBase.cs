using HttpClientFactory;
using HttpClientFactory.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.Core.Infrastructure
{
    public abstract class ApiClientBase : IApiClient
    {
        public string GetResponse(string url, NameValueCollection query)
        {
            var result = Task.Run(() => GetResponseAsync(url, query)).GetAwaiter().GetResult();
            return result;
        }

        public abstract Task<string> GetResponseAsync(string url, NameValueCollection query);
   
    }
}
