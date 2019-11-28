using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.ResponseChecker;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.IMOne
{
    public class VrLotteryXmlChecker : IThirdPartyResponseChecker
    {
        public void CheckXml(XDocument xml)
        {
            var root = xml.Root;
            var code = root.Element("Code");
            if (code == null)
                throw new ThirdPartyProductException($"VRLottery - xml not correct format {xml}");
            int value;
            if (!int.TryParse(code.Value, out value))
                throw new ThirdPartyProductException($"VRLottery - Can't convert {code.Value} response code to int");
            switch(value)
            {
                case 507:
                    throw new ThirdPartyProductException($"VRLottery - Invalid Currency.");
                case 508:
                    throw new ThirdPartyProductException($"VRLottery - Invalid Product Wallet");
                case 525:
                    throw new ThirdPartyProductException($"VRLottery - Invalid timerange, it must be within the configured timerange.");
                case 526:
                    throw new ThirdPartyProductException($"VRLottery - StartDate can't be later than EndDate or now.");
                case 528:
                    throw new ThirdPartyProductException($"VRLottery - Invalid datetime format.");
                case 0:
                    return;
                default:
                    throw new ThirdPartyProductException($"VRLottery - Fail request - response code {value}");
            }
        }
    }
}
