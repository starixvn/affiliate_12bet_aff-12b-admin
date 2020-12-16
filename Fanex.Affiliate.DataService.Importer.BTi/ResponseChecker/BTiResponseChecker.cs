using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.ResponseChecker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.BTi.ResponseChecker
{
    public class BTiResponseChecker : IThirdPartyResponseChecker
    {
        public void CheckXml(XDocument xml)
        {
            var root = xml.Root;
            var data = root.Element("Bets");
            if (data == null)
            {
                throw new ThirdPartyProductException($"BTi - Xml response is not correct format {xml}");
            }
            var code = data.Element("errorCode");
            if (code == null)
            {
                throw new ThirdPartyProductException($"BTi - Xml response is not correct format {xml}");
            }

            const string successCode = "0";
            const string tokenExpiredCode = "-1000";
            var value = code.Value;
            if (value != successCode && value != tokenExpiredCode)
            {
                throw new ThirdPartyProductException($"BTi - Xml response code indicate request not success {xml}");
            }
        }
    }
}
