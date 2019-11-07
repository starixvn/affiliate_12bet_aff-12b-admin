using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.Core.Infrastructure
{
    public interface IApiClient
    {
        string GetResponse(string url, NameValueCollection query);
        Task<string> GetResponseAsync(string url, NameValueCollection query);
    }
}
