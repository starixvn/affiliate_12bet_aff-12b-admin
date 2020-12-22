using Fanex.Affiliate.DataService.Importer.BTi;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Job.MetaModels.TimeTracker;
using Fanex.Affiliate.DataService.Job.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    [DisallowConcurrentExecution]
    public class BTiJob : NewApiJobBase
    {
        private readonly IAppSettings _appSetting;
        private readonly IImporterService _service;
        private readonly ITimeTracker _timeTracker;

        public BTiJob() : this(null, null, null)
        { }

        public BTiJob(IAppSettings appSettings, IImporterService importerService, ITimeTracker timeTracker)
        {
            _appSetting = appSettings ?? new AppSettings();
            _service = importerService ?? new BTiService();
            _timeTracker = timeTracker ?? new TimeTracker();
        }

        public override string ServiceName => "BTi Service";

        protected override DateTime GetStartTimeRequest()
        {
            var defaultStartTime = DateTime.ParseExact(_appSetting.GetValue("BTiStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
            var param = new TimeTrackerBaseParam { ProductId = Products.BTiId };

            var lastQueryTime = _timeTracker.GetLastestCallSuccess(param, defaultStartTime);
            var startTime = new DateTime(lastQueryTime.Year, lastQueryTime.Month, lastQueryTime.Day, lastQueryTime.Hour, lastQueryTime.Minute, lastQueryTime.Second);

            return startTime;
        }

        protected override DateTime GetEndTimeRequest()
        {
            var adjustHour = int.Parse(_appSetting.GetValue("BTiHourAdjustment"));
            var now = DateTime.Now;
            var endTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second).AddHours(adjustHour);

            return endTime;
        }

        protected override IEnumerable<Tuple<DateTime, DateTime>> GetTimeRange(DateTime startTime, DateTime endTime)
        {
            var intervalTime = int.Parse(_appSetting.GetValue("BTiIntervalTime"));

            return DateTimeHelper.GetProportionTimeRange(startTime, endTime, TimeSpan.FromMinutes(intervalTime));
        }

        protected override async Task ImportDataAndTrackImportTime(DateTime startTime, DateTime endTime)
        {
            var startTimeServer = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(startTime, TimeZoneId.GMT);
            var endTimeServer = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(startTime, TimeZoneId.GMT);

            Logger.Info($"{ServiceName} - Get data from {startTimeServer} to {endTimeServer}");
            await _service.ImportAsync(startTimeServer, endTimeServer);

            var saveTimeTrackerParam = new InsertTimeTrackerParam
            {
                ProductId = Products.BTiId,
                CallTime = endTime
            };

            _timeTracker.SaveTime(saveTimeTrackerParam);
            Logger.Info($"{ServiceName} - Finished get data");
        }
    }
}
