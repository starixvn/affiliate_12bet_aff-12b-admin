using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.ResponseChecker;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.LBKeno
{
    public class LBKenoXmlChecker : IThirdPartyResponseChecker
    {
        public void CheckXml(XDocument xml)
        {
            var root = xml.Root;
            var statusField = root.Element("status_code");
            if (statusField == null)
                throw new ThirdPartyProductException("LBKeno - Wrong xml format");
            var code = statusField.Value.Trim();
            if (code == "0")
                return;
            if (code == "40.01")
                throw new ThirdPartyProductException($"LBKeno - {code} Invalid secrtkey and/or IP address not white listed");

            var message = "";
            message = root.Element("status_text")?.Value;
            throw new ThirdPartyProductException($"LBKeno - {code} {message}");
        }
    }
}
