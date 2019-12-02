
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.Core.ResponseChecker
{
    public interface IThirdPartyResponseChecker
    {
        /// <summary>
        /// Check xml is correct format or not
        /// Throw exception if xml is not correct format
        /// </summary>
        /// <param name="data"></param>
        void CheckXml(XDocument xml);
    }
}
