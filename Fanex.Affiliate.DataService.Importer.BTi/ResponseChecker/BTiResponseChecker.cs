using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.ResponseChecker;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.BTi.ResponseChecker
{
    public class BTiResponseChecker : IThirdPartyResponseChecker
    {
        public void CheckXml(XDocument xml)
        {
            var root = xml.Root;
            var code = root.Element("errorCode");
            if (code == null)
            {
                throw new ThirdPartyProductException($"BTi - Xml response is not correct format {xml}");
            }

            const string successCode = "0";
            var value = code.Value;

            if (value != successCode)
            {
                throw new ThirdPartyProductException($"BTi - Xml response code indicate request not success {xml}");
            }
        }
    }
}
