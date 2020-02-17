using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.Core.Infrastructure
{
    public interface IImporterService
    {
        Task<XDocument> ConvertRawDataToXmlAsync(string raw);       
        Task<IEnumerable<XDocument>> GetXmlDataListAsync(DateTime startTime, DateTime endTime);
        Task<IEnumerable<string>> GetRawDataListAsync(DateTime startTime, DateTime endTime);
        Task ImportAsync(string data);
        Task ImportAsync(IEnumerable<string> datas);
        Task ImportAsync(DateTime startTime, DateTime endTime);
    }
}
