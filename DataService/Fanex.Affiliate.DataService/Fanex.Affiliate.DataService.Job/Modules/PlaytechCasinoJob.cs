using Fanex.Affiliate.DataService.Importer;
using Fanex.Logging;
using Quartz;
using System.Threading.Tasks;
using System;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    public class PlaytechCasinoJob : JobBase
    {
        public override string ServiceName => "PlaytecCasion Service";

        protected override Task ImportDataToSystem(IJobExecutionContext context)
        {
            var dataMap = context.JobDetail.JobDataMap;
            var importService = (IDataImportingRunner)dataMap.Get("dataImportingRunner");
            var importResult = importService.Run();
            if (!importResult.IsSuccessful)
            {
                Logger.Error($"{importResult.Code}\n{importResult.Message}");
            }

            return Task.CompletedTask;
        }
    }
}
