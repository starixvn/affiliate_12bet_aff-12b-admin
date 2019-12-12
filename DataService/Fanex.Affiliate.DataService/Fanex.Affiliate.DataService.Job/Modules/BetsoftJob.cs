using System;
using System.Globalization;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Betsoft.MetaModels;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Job.MetaModels.TimeTracker;
using Fanex.Affiliate.DataService.Job.Services;
using Quartz;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    [DisallowConcurrentExecution]
    public class BetsoftJob : JobBase
    {
        private readonly IAppSettings _appSetting;
        private readonly IBetsoftService _betSoftService;
        private readonly ITimeTracker _timeTracker;

        public override string ServiceName => "Betsoft Service";
        public BetsoftJob()
        {
            _appSetting = new AppSettings();
            _betSoftService = new BetsoftService();
            _timeTracker = new TimeTracker();
        }

        protected override Task ImportDataToSystem(IJobExecutionContext context)
        {
            var defaultStartTime = DateTime.ParseExact(_appSetting.GetValue("BetsoftStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
            var param = new TimeTrackerBaseParam { ProductId = Products.BetsoftProductId };
            var lastQueryTime = _timeTracker.GetLastestCallSuccess(param, defaultStartTime);

            var hourAdjustment = Convert.ToInt32(_appSetting.GetValue("BetsoftHourAdjustment"));
            var now = DateTime.Now.AddHours(hourAdjustment);
            var nowByHour = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0, DateTimeKind.Local);

            if (DateTime.Compare(lastQueryTime, nowByHour) < 0)
            {
                var startTime = new DateTime(lastQueryTime.Year, lastQueryTime.Month, lastQueryTime.Day, lastQueryTime.Hour, lastQueryTime.Minute, lastQueryTime.Second);
                var endTime = new DateTime(nowByHour.Year, nowByHour.Month, nowByHour.Day, nowByHour.Hour, nowByHour.Minute, nowByHour.Second);

                for (var start = startTime; DateTime.Compare(start, endTime) < 0; start = start.AddHours(1))
                {
                    var endTimeByBatch = start.AddHours(1);
                    var betSoftParam = new BetsoftParam
                    {
                        StartDate = start,
                        EndDate = DateTime.Compare(endTimeByBatch, endTime) > 0 ? endTime : endTimeByBatch
                    };
                    var nextStartTime = betSoftParam.EndDate;
                    Logger.Info($"{ServiceName} - Get data from {betSoftParam.StartDate} to {betSoftParam.EndDate}");
                    _betSoftService.Import(betSoftParam);

                    var saveTimeTrackerParam = new InsertTimeTrackerParam
                    {
                        ProductId = Products.BetsoftProductId,
                        CallTime = nextStartTime
                    };
                    _timeTracker.SaveTime(saveTimeTrackerParam);
                    Logger.Info($"{ServiceName} - Finished get data");
                }
            }
            return Task.CompletedTask;
        }
    }
}
