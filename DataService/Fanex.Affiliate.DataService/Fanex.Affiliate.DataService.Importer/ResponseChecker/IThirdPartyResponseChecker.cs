using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.ResponseChecker
{
    public interface IThirdPartyResponseChecker
    {
        /// <summary>
        /// Check that xml 
        /// </summary>
        /// <param name="data"></param>
        void CheckData(string data);
    }
}
