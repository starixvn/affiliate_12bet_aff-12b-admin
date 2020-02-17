using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.ResponseChecker;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.FlowGaming.ResponseChecker
{
    public class TopazResponseChecker : IThirdPartyResponseChecker
    {
        public void CheckXml(XDocument xml)
        {
            var root = xml.Root;
            var statusEle = root.Element("status");
            if (statusEle == null)
            {
                throw new ThirdPartyProductException($"Topaz - Xml response is not correct format {xml}");
            }
            if (statusEle.Value.Contains("FAIL"))
            {
                var msgEle = root.Element("message");
                var errorEle = root.Element("error");
                throw new ThirdPartyProductException($"Topaz - Request fail {msgEle} - {errorEle}");
            }
            if (!statusEle.Value.Contains("SUCCESS"))
            {
                throw new ThirdPartyProductException($"Topaz - Xml response is not correct format {xml}");
            }
        }
    }
}
