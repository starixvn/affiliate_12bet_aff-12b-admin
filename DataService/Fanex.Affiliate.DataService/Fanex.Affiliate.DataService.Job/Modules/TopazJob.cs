using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Job.MetaModels.TimeTracker;
using Fanex.Affiliate.DataService.Job.Services;
using Quartz;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    [DisallowConcurrentExecution]
    public class TopazJob : NewApiJobBase
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

        protected override DateTime GetStartTimeRequest()
        {
            var defaultStartTime = DateTime.ParseExact(_appSetting.GetValue("TopazStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
            var param = new TimeTrackerBaseParam { ProductId = Products.TopazId };
            var lastQueryTime = _timeTracker.GetLastestCallSuccess(param, defaultStartTime);
            var startTime = new DateTime(lastQueryTime.Year, lastQueryTime.Month, lastQueryTime.Day, lastQueryTime.Hour, lastQueryTime.Minute, lastQueryTime.Second);
            return startTime;
        }

        protected override DateTime GetEndTimeRequest()
        {
            var ajustHour = int.Parse(_appSetting.GetValue("TopazHourAdjustment"));
            var ajustTime = DateTime.Now.AddHours(ajustHour);
            var endTime = new DateTime(ajustTime.Year, ajustTime.Month, ajustTime.Day, ajustTime.Hour, ajustTime.Minute, ajustTime.Second);
            return endTime;
        }

        protected override IEnumerable<Tuple<DateTime, DateTime>> GetTimeRange(DateTime startTime, DateTime endTime)
        {
            var minuteToRun = int.Parse(_appSetting.GetValue("TopazIntervalTime"));
            return DateTimeHelper.GetProportionTimeRange(startTime, endTime, TimeSpan.FromMinutes(minuteToRun));
        }

        protected override async Task ImportDataAndTrackImportTime(DateTime startTime, DateTime endTime)
        {
            var startUtcTime = startTime.ToUniversalTime();
            var endUtcTime = endTime.ToUniversalTime();
            Logger.Info($"{ServiceName} - Get data from {startUtcTime} to {endUtcTime}");

            await _service.ImportAsync(startUtcTime, endUtcTime);

            var saveTimeTrackerParam = new InsertTimeTrackerParam
            {
                ProductId = Products.TopazId,
                CallTime = endTime
            };
            _timeTracker.SaveTime(saveTimeTrackerParam);
            Logger.Info($"{ServiceName} - Finished get data");
        }
    }
}
