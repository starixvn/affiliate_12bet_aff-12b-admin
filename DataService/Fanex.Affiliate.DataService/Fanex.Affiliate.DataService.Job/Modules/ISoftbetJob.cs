using System;
using System.Globalization;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.ISoftbet.MetaModels;
using Fanex.Affiliate.DataService.Job.MetaModels.TimeTracker;
using Fanex.Affiliate.DataService.Job.Services;
using Quartz;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    [DisallowConcurrentExecution]
    public class ISoftbetJob : JobBase
    {
        private readonly IAppSettings _appSetting;
        private readonly IISoftbetService _isoftbetService;
        private readonly ITimeTracker _timeTracker;

        public override string ServiceName => "ISoftbet Service";
        public ISoftbetJob()
        {
            _appSetting = new AppSettings();
            _isoftbetService = new ISoftbetService();
            _timeTracker = new TimeTracker();
        }

        protected override Task ImportDataToSystem(IJobExecutionContext context = null)
        {
            var defaultStartTime = DateTime.ParseExact(_appSetting.GetValue("ISoftbetStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
            var trackerParam = new TimeTrackerBaseParam { ProductId = Products.ISoftbetProductId };
            var lastQueryTime = _timeTracker.GetLastestCallSuccess(trackerParam, defaultStartTime);

            var hourAdjustment = Convert.ToInt32(_appSetting.GetValue("ISoftbetHourAdjustment"));
            var now = DateTime.Now.AddHours(hourAdjustment);
            var nowByMinute = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0, DateTimeKind.Local);

            if (DateTime.Compare(lastQueryTime, nowByMinute) < 0)
            {
                var startTime = new DateTime(lastQueryTime.Year, lastQueryTime.Month, lastQueryTime.Day, lastQueryTime.Hour, lastQueryTime.Minute, lastQueryTime.Second);
                var endTime = new DateTime(nowByMinute.Year, nowByMinute.Month, nowByMinute.Day, nowByMinute.Hour, nowByMinute.Minute, nowByMinute.Second);

                for (var start = startTime; DateTime.Compare(start, endTime) < 0; start = start.AddHours(1))
                {
                    var endTimeByBatch = start.AddHours(1);
                    var isoftbetParam = new ISoftbetEntryParam
                    {
                        StartTime = start,
                        EndTime = DateTime.Compare(endTimeByBatch, endTime) > 0 ? endTime : endTimeByBatch,
                        PageNumber = 1
                    };
                    var nextStartTime = isoftbetParam.EndTime;
                    Logger.Info($"{ServiceName} - Get data from {isoftbetParam.StartTime} to {isoftbetParam.EndTime}");
                    _isoftbetService.Import(isoftbetParam);

                    var saveTimeParam = new InsertTimeTrackerParam
                    {
                        ProductId = Products.ISoftbetProductId,
                        CallTime = nextStartTime
                    };
                    _timeTracker.SaveTime(saveTimeParam);
                    Logger.Info($"{ServiceName} - Finished get data");
                }
            }

            return Task.CompletedTask;
        }
    }
}
