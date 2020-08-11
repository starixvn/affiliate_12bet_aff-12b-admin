using Fanex.Affiliate.DataService.Importer.Core.Extension;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;

namespace Fanex.Affiliate.DataService.Importer.Core.Infrastructure
{
    public sealed class JsonContentProvider : IHttpContentProvider
    {
        public HttpContent GetHttpContent(NameValueCollection param)
        {
            var dic = param.ToDictionary();
            var json = JsonConvert.SerializeObject(dic);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return content;
        }
    }
}
