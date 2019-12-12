using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Job.MetaModels.TimeTracker;
using Fanex.Affiliate.DataService.Job.Services;
using Fanex.Logging;
using Quartz;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    [DisallowConcurrentExecution]
    public class TopazJob : JobBase
    {
        private readonly IAppSettings _appSetting;
        private readonly IImporterService _service;
        private readonly ITimeTracker _timeTracker;

        public override string ServiceName => "Topaz Service";
        public TopazJob()
        {
            _appSetting = new AppSettings();
            _service = new TopazService();
            _timeTracker = new TimeTracker();
        }

        protected override async Task ImportDataToSystem(IJobExecutionContext context = null)
        {
            var defaultStartTime = DateTime.ParseExact(_appSetting.GetValue("TopazStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
            var param = new TimeTrackerBaseParam { ProductId = Products.TopazId };
            var lastQueryTime = _timeTracker.GetLastestCallSuccess(param, defaultStartTime);
            var minuteToRun = int.Parse(_appSetting.GetValue("TopazIntervalTime"));

            var now = DateTime.Now;

            if (DateTime.Compare(lastQueryTime, now) < 0)
            {
                var startTime = new DateTime(lastQueryTime.Year, lastQueryTime.Month, lastQueryTime.Day, lastQueryTime.Hour, lastQueryTime.Minute, lastQueryTime.Second);
                var endTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second).AddMinutes(-15);

                for (var start = startTime; DateTime.Compare(start.AddMinutes(minuteToRun), endTime) < 0; start = start.AddMinutes(minuteToRun))
                { 
                    var nextStartTime = start.AddMinutes(minuteToRun);

                    var startUtcTime = start.ToUniversalTime();
                    var endUtcTime = nextStartTime.ToUniversalTime();
                    Logger.Info($"{ServiceName} - Get data from {startUtcTime} to {endUtcTime}");
                    await _service.ImportAsync(startUtcTime, endUtcTime);

                    var saveTimeTrackerParam = new InsertTimeTrackerParam
                    {
                        ProductId = Products.TopazId,
                        CallTime = nextStartTime
                    };
                    _timeTracker.SaveTime(saveTimeTrackerParam);
                    Logger.Info($"{ServiceName} - Finished get data");
                }
            }
        }
    }
}
