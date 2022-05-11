using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.ResponseChecker;
using Fanex.Affiliate.DataService.Importer.Jasper.Helpers;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.Sunstone.ResponseChecker
{
    public class SunstoneResponseChecker : IThirdPartyResponseChecker
    {
        public void CheckXml(XDocument xml)
        {
            var element = xml.Root.Element("element");
            var status = element.Element("status");
            if (status == null)
            {
                throw new ThirdPartyProductException($"Sunstone - Xml response is not correct format {xml}");
            }

            if (!Validations.IsRequestSuccess(status))
            {
                throw new ThirdPartyProductException($"Sunstone - Xml response code indicate request not success {xml}");
            }
        }
    }
}
