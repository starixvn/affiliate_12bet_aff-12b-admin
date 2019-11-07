using Fanex.Affiliate.DataService.Importer.MetaModels.GPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer
{
    public interface IImporterService
    {
        IEnumerable<string> GetXmlDataList(DateTime startTime, DateTime endTime);
        IEnumerable<string> GetRawDataList(DateTime startTime, DateTime endTime);
        Task<IEnumerable<string>> GetXmlDataListAsync(DateTime startTime, DateTime endTime);
        Task<IEnumerable<string>> GetRawDataListAsync(DateTime startTime, DateTime endTime);
        void Import(string data);
        void Import(DateTime startTime, DateTime endTime);
        Task ImportAsync(string data);
        Task ImportAsync(DateTime startTime, DateTime endTime);
    }
}
