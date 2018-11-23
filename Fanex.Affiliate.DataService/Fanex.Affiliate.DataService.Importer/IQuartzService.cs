using System.Collections.Generic;
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.Importer.DTO.Quartz;
using Fanex.Affiliate.DataService.Importer.MetaModels.Quartz;

namespace Fanex.Affiliate.DataService.Importer
{
    public interface IQuartzService
    {
        Task<IEnumerable<QuartzApiParamResultPair>> GetRawDataList(QuartzEntryParam param);

        string ConvertToXml(QuartzSummary quartzSummary, QuartzApiParam param);

        void Import(string xmlData);

        Task Import(QuartzEntryParam param);
    }
}
