using System.Collections.Specialized;
using System.Net.Http;
using System.Text;

namespace Fanex.Affiliate.DataService.Importer.Core.Infrastructure
{
    public class XmlContentProvider : IHttpContentProvider
    {
        private readonly string EncodedXml;

        public XmlContentProvider(string encodedXml)
        {
            EncodedXml = encodedXml;
        }
        public HttpContent GetHttpContent(NameValueCollection param)
        {
            var content = new StringContent(EncodedXml, Encoding.Unicode, "application/xml");
            return content;
        }
    }
}
