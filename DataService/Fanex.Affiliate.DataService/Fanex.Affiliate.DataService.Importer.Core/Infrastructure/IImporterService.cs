using Fanex.Affiliate.DataService.Importer.MetaModels.GPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer
{
    public interface IImporterService : IDisposable
    {
        IEnumerable<XDocument> GetXmlDataList(DateTime startTime, DateTime endTime);
        IEnumerable<string> GetRawDataList(DateTime startTime, DateTime endTime);
        XDocument ConvertRawDataToXml(string raw);       
        Task<IEnumerable<XDocument>> GetXmlDataListAsync(DateTime startTime, DateTime endTime);
        Task<IEnumerable<string>> GetRawDataListAsync(DateTime startTime, DateTime endTime);
        Task<XDocument> ConvertRawDataToXmlAsync(string raw);
        void Import(string data);
        void Import(DateTime startTime, DateTime endTime);
        Task ImportAsync(string data);
        Task ImportAsync(DateTime startTime, DateTime endTime);
    }
}
