using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Job.Services;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    [DisallowConcurrentExecution]
    class BTiMonthlyJob : BTiJob
    {
        public BTiMonthlyJob() : this(null, null, null)
        { }

        public BTiMonthlyJob(IAppSettings appSettings, IImporterService importerService, ITimeTracker timeTracker)
            : base(appSettings, importerService, timeTracker)
        { }

        public override string ServiceName => "BTi Monthly Service";

        protected override DateTime GetStartTimeRequest()
        {
            var now = DateTime.Now;
            var startTime = new DateTime(now.Year, now.Month, 1, 0, 0, 0).AddMonths(-1);

            return startTime;
        }

        protected override DateTime GetEndTimeRequest()
        {
            var now = DateTime.Now;
            var endTime = new DateTime(now.Year, now.Month, 1, 0, 0, 0);

            return endTime;
        }

        protected override async Task ImportDataAndTrackImportTime(DateTime startTime, DateTime endTime)
        {
            Logger.Info($"{ServiceName} - Get data from {startTime} to {endTime}");
            await _service.ImportAsync(startTime, endTime);
            Logger.Info($"{ServiceName} - Finished get data");
        }
    }
}
