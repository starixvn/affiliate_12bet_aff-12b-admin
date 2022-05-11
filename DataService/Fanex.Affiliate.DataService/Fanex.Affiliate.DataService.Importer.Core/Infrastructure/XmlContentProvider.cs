using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Web;

namespace Fanex.Affiliate.DataService.Importer.Core.Infrastructure
{
    public class XmlContentProvider : IHttpContentProvider
    {
        private readonly string Xml;

        public XmlContentProvider(string xml)
        {
            Xml = xml;
        }
        public HttpContent GetHttpContent(NameValueCollection param)
        {
            var encoded = HttpUtility.HtmlEncode(Xml);
            var content = new StringContent(encoded, Encoding.Unicode, "application/xml");
            return content;
        }
    }
}
