using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.WinFormApp.Converter
{
    public interface IFileImporter
    {
        Task<IEnumerable<XDocument>> ConvertToXml(string data);
        Task<IEnumerable<XDocument>> ConvertToXml(string data, IDataSpliter spliter);
        Task ImportAsync(IEnumerable<string> xml);
    }
}
