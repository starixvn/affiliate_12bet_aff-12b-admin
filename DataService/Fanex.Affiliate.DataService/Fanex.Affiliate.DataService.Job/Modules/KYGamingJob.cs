using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.KYGaming;
using Fanex.Affiliate.DataService.Job.MetaModels.TimeTracker;
using Fanex.Affiliate.DataService.Job.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    public class KYGamingJob : NewApiJobBase
    {
        private readonly IAppSettings _appSetting;
        private readonly IImporterService _service;
        private readonly ITimeTracker _timeTracker;

        public KYGamingJob(IAppSettings appSettings, IImporterService importerService, ITimeTracker timeTracker)
        {
            _appSetting = appSettings ?? new AppSettings();
            _service = importerService ?? new KYGamingService();
            _timeTracker = timeTracker ?? new TimeTracker();
        }

        public override string ServiceName => "KYGaming Service";

        protected override DateTime GetEndTimeRequest()
        {
            var ajustHour = int.Parse(_appSetting.GetValue("KYGamingHourAdjustment"));
            var now = DateTime.Now;
            var endTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second).AddHours(ajustHour);

            return endTime;
        }

        protected override DateTime GetStartTimeRequest()
        {
            var defaultStartTime = DateTime.ParseExact(_appSetting.GetValue("KYGamingStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
            var param = new TimeTrackerBaseParam { ProductId = Products.KyGamingId };
            var lastQueryTime = _timeTracker.GetLastestCallSuccess(param, defaultStartTime);
            var startTime = new DateTime(lastQueryTime.Year, lastQueryTime.Month, lastQueryTime.Day, lastQueryTime.Hour, lastQueryTime.Minute, lastQueryTime.Second);
            return startTime;
        }

        protected override IEnumerable<Tuple<DateTime, DateTime>> GetTimeRange(DateTime startTime, DateTime endTime)
        {
            var intervalTime = int.Parse(_appSetting.GetValue("KYGamingIntervalTime"));
            return DateTimeHelper.GetProportionTimeRange(startTime, endTime, TimeSpan.FromMinutes(intervalTime));
        }

        protected override async Task ImportDataAndTrackImportTime(DateTime startTime, DateTime endTime)
        {
            var startTimeServer = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(startTime, TimeZoneId.GmtPlus8);
            var endTimeServer = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(endTime, TimeZoneId.GmtPlus8);

            Logger.Info($"{ServiceName} - Get data from {startTimeServer} to {endTimeServer}");
            await _service.ImportAsync(startTimeServer, endTimeServer);

            var saveTimeTrackerParam = new InsertTimeTrackerParam
            {
                ProductId = Products.KyGamingId,
                CallTime = endTime
            };
            _timeTracker.SaveTime(saveTimeTrackerParam);
            Logger.Info($"{ServiceName} - Finished get data");
        }
    }
}
