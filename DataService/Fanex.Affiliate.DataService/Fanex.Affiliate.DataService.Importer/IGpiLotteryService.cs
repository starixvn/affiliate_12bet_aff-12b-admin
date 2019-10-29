using Fanex.Affiliate.DataService.Importer.MetaModels.GPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer
{
    public interface IGpiLotteryService
    {
        List<string> GetRawDataList(DateTime startDate, DateTime endDate);
        void Import(string xmlData);
        void Import(DateTime startDate, DateTime endDate);
    }
}
