using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Job.MetaModels.TimeTracker;
using Fanex.Affiliate.DataService.Job.Services;
using Fanex.Logging;
using ONEbook.UM.NonWebApp;
using Quartz;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    [DisallowConcurrentExecution]
    public class LBKenoJob : JobBase
    {
        private readonly IAppSettings _appSetting;
        private readonly IImporterService service;
        private readonly ITimeTracker _timeTracker;

        public LBKenoJob()
        {
            _appSetting = new AppSettings();
            service = new LBKenoService();
            _timeTracker = new TimeTracker();
        }

        protected override async Task ImportDataToSystem(IJobExecutionContext context = null)
        {
            var defaultStartTime = DateTime.ParseExact(_appSetting.GetValue("LBKenoStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
            var param = new TimeTrackerBaseParam { ProductId = Products.LBKenoId };
            var lastQueryTime = _timeTracker.GetLastestCallSuccess(param, defaultStartTime);
            var now = DateTime.Now;

            var gapTime = now - lastQueryTime;
            LogService.Info($"Start LBKeno service at {now}");
            if (gapTime.Days >= 1)
            {
                var startTime = new DateTime(lastQueryTime.Year, lastQueryTime.Month, lastQueryTime.Day, lastQueryTime.Hour, lastQueryTime.Minute, lastQueryTime.Second);
                var endTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second).AddDays(-1);

                var startTimeServer = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(startTime, TimeZoneId.GmtPlus8);
                var endTimeServer = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(endTime, TimeZoneId.GmtPlus8);
                LogService.Info($"LBKeno service - Get data from {startTimeServer} to {endTimeServer}");
                await service.ImportAsync(startTimeServer, endTimeServer);

                var saveTimeTrackerParam = new InsertTimeTrackerParam
                {
                    ProductId = Products.LBKenoId,
                    CallTime = endTime
                };
                _timeTracker.SaveTime(saveTimeTrackerParam);
                LogService.Info($"LBKeno service - Finished get data");
            }
        }
    }
}
