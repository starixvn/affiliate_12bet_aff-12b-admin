using Fanex.Affiliate.DataService.Importer.BigGaming;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Quartz;

namespace Fanex.Affiliate.DataService.Job.Modules.BigGaming
{
    [DisallowConcurrentExecution]
    public class BgFishJob : BigGamingJob
    {
        public override string ServiceName => "Bg Fish Service";

        protected override int ProductId => Products.BgFishId;

        public BgFishJob() : this(new BgFishService())
        { }

        public BgFishJob(IImporterService importerService): base(importerService)
        {
        }
    }
}
