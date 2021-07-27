using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.ResponseChecker;
using System;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.IMOne
{
    public class IMEsportXmlChecker : IThirdPartyResponseChecker
    {
        public void CheckXml(XDocument xml)
        {
            var root = xml.Root;
            var code = root.Element("Code");
            if (code == null)
                throw new ThirdPartyProductException($"IMEsport - xml not correct format {xml}");

            IMoneResponseStatus status;
            if (!Enum.TryParse<IMoneResponseStatus>(code.Value, out status))
                throw new ThirdPartyProductException($"IMEsport - Can't convert {code.Value} response code");

            switch (status)
            {
                case IMoneResponseStatus.INVALID_CURRENCY:
                    throw new ThirdPartyProductException($"IMEsport - Invalid Currency.");
                case IMoneResponseStatus.INVALID_CODEWALLET:
                    throw new ThirdPartyProductException($"IMEsport - Invalid Product Wallet");
                case IMoneResponseStatus.INVALID_TIME_RANGE:
                    throw new ThirdPartyProductException($"IMEsport - Invalid timerange, it must be within the configured timerange.");
                case IMoneResponseStatus.INVALID_START_TIME:
                    throw new ThirdPartyProductException($"IMEsport - StartDate can't be later than EndDate or now.");
                case IMoneResponseStatus.INVALID_DATETIME_FORMAT:
                    throw new ThirdPartyProductException($"IMEsport - Invalid datetime format.");
                case IMoneResponseStatus.SUCCESS:
                    return;
                default:
                    throw new ThirdPartyProductException($"IMEsport - Fail request - response code {status}");
            }
        }
    }
}
