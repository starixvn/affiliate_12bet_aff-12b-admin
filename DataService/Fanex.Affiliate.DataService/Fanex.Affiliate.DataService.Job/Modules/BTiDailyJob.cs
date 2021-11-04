using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Job.Constants;
using Fanex.Affiliate.DataService.Job.Services;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    [DisallowConcurrentExecution]
    class BTiDailyJob : BTiJob
    {
        private readonly DateTime _now = DateTime.Now;
        private DateTime Today => new DateTime(_now.Year, _now.Month, _now.Day, 0, 0, 0);

        public BTiDailyJob() : this(null, null, null)
        { }

        public BTiDailyJob(IAppSettings appSettings, IImporterService importerService, ITimeTracker timeTracker)
            : base(appSettings, importerService, timeTracker)
        { }

        public override string ServiceName => "BTi Daily Service";

        protected override DateTime GetStartTimeRequest()
            => Today.AddDays(DateTimeConfig.ThreeDaysAgo);

        protected override DateTime GetEndTimeRequest()
            => Today.AddDays(DateTimeConfig.TwoDaysAgo);

        protected override async Task ImportDataAndTrackImportTime(DateTime startTime, DateTime endTime)
        {
            Logger.Info($"{ServiceName} - Get data from {startTime} to {endTime}");
            await _service.ImportAsync(startTime, endTime);
            Logger.Info($"{ServiceName} - Finished get data");
        }
    }
}
