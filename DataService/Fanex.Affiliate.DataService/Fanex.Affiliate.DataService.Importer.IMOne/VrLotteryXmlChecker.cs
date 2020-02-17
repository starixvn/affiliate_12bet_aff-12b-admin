using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.ResponseChecker;
using System;
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

            IMoneResponseStatus status;
            if (!Enum.TryParse<IMoneResponseStatus>(code.Value, out status))
                throw new ThirdPartyProductException($"VRLottery - Can't convert {code.Value} response code");

            switch(status)
            {
                case IMoneResponseStatus.INVALID_CURRENCY:
                    throw new ThirdPartyProductException($"VRLottery - Invalid Currency.");
                case IMoneResponseStatus.INVALID_CODEWALLET:
                    throw new ThirdPartyProductException($"VRLottery - Invalid Product Wallet");
                case IMoneResponseStatus.INVALID_TIME_RANGE:
                    throw new ThirdPartyProductException($"VRLottery - Invalid timerange, it must be within the configured timerange.");
                case IMoneResponseStatus.INVALID_START_TIME:
                    throw new ThirdPartyProductException($"VRLottery - StartDate can't be later than EndDate or now.");
                case IMoneResponseStatus.INVALID_DATETIME_FORMAT:
                    throw new ThirdPartyProductException($"VRLottery - Invalid datetime format.");
                case IMoneResponseStatus.BET_DETAIL_IN_PROCESS:
                    throw new ThirdPartyProductException($"VrLottery - Bet details in process. Please try again");
                case IMoneResponseStatus.INVALID_PAGE_SIZE:
                    throw new ThirdPartyProductException($"VrLottery - Invalid page size");
                case IMoneResponseStatus.NO_DATA_FOUND:
                case IMoneResponseStatus.SUCCESS:
                    return;
                default:
                    throw new ThirdPartyProductException($"VRLottery - Fail request - response code {status}");
            }
        }
    }
}
