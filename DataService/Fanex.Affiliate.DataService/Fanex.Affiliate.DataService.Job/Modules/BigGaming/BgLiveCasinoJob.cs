using Fanex.Affiliate.DataService.Importer.BigGaming;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Quartz;

namespace Fanex.Affiliate.DataService.Job.Modules.BigGaming
{
    [DisallowConcurrentExecution]
    public class BgLiveCasinoJob : BigGamingJob
    {
        public override string ServiceName => "Bg Live Casino Service";

        protected override int ProductId => Products.BgLiveCasinoId;

        public BgLiveCasinoJob() : this(new BgLiveCasinoService())
        { }

        public BgLiveCasinoJob(IImporterService importerService) : base(importerService)
        {
        }
    }
}
