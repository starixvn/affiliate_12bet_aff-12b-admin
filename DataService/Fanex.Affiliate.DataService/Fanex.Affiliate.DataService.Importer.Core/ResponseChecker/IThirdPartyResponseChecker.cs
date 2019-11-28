using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.Core.ResponseChecker
{
    public interface IThirdPartyResponseChecker
    {
        void CheckXml(XDocument xml);
    }
}
