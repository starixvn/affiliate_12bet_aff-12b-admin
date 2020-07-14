using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.ResponseChecker;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.KYGaming.ResponseChecker
{
    public class KyGamingResponseChecker : IThirdPartyResponseChecker
    {
        public void CheckXml(XDocument xml)
        {
            var root = xml.Root;
            var data = root.Element("d");
            if (data == null)
            {
                throw new ThirdPartyProductException($"KyGaming - Xml response is not correct format {xml}");
            }
            var code = data.Element("code");
            if (code == null)
            {
                throw new ThirdPartyProductException($"KyGaming - Xml response is not correct format {xml}");
            }

            const string successCode = "0";
            const string dataNonExist = "16";
            var value = code.Value;
            if (value != successCode && value != dataNonExist)
            {
                throw new ThirdPartyProductException($"KyGaming - Xml response code indicate request not success {xml}");
            }

        }
    }
}
