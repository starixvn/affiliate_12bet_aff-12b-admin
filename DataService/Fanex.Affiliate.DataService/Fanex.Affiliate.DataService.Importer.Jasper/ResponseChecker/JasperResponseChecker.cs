using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.ResponseChecker;
using Fanex.Affiliate.DataService.Importer.Jasper.Helpers;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.Jasper.ResponseChecker
{
    public class JasperResponseChecker : IThirdPartyResponseChecker
    {
        public void CheckXml(XDocument xml)
        {
            var root = xml.Root;
            var status = root.Element("status");
            if (status == null)
            {
                throw new ThirdPartyProductException($"Jasper - Xml response is not correct format {xml}");
            }

            if (!Validations.IsRequestSuccess(status))
            {
                throw new ThirdPartyProductException($"Jasper - Xml response code indicate request not success {xml}");
            }
        }
    }
}
