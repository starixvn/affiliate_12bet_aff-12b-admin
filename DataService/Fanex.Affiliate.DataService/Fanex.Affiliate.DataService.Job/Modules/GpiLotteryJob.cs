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
    public class GpiLotteryJob : NewApiJobBase
    {
        private readonly IAppSettings _appSetting;
        private readonly IImporterService _gpiLotteryService;
        private readonly ITimeTracker _timeTracker;

        public override string ServiceName => "GPI Lottery Service";

        public GpiLotteryJob()
        {
            _appSetting = new AppSettings();
            _gpiLotteryService = new GpiLotteryService();
            _timeTracker = new TimeTracker();
        }

        protected override DateTime GetStartTimeRequest()
        {
            var defaultStartTime = DateTime.ParseExact(_appSetting.GetValue("GpiLotteryStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
            var param = new TimeTrackerBaseParam { ProductId = Products.GPILotteryId };
            var lastQueryTime = _timeTracker.GetLastestCallSuccess(param, defaultStartTime);
            return lastQueryTime;
        }

        protected override DateTime GetEndTimeRequest()
        {
            var ajustHour = int.Parse(_appSetting.GetValue("GpiLotteryHourAdjustment"));
            var now = DateTime.Now;
            var ajustTime = now.AddHours(ajustHour);
            return ajustTime;
        }

        protected override IEnumerable<Tuple<DateTime, DateTime>> GetTimeRange(DateTime startTime, DateTime endTime)
        {
            var intervalTime = int.Parse(_appSetting.GetValue("GpiLotteryIntervalTime"));
            return DateTimeHelper.GetProportionTimeRange(startTime, endTime, TimeSpan.FromMinutes(intervalTime));
        }

        protected override async Task ImportDataAndTrackImportTime(DateTime startTime, DateTime endTime)
        {
            var startTimeServer = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(startTime, TimeZoneId.GmtPlus8);
            var endTimeServer = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(endTime, TimeZoneId.GmtPlus8);

            Logger.Info($"{ServiceName} - Get data from {startTimeServer} to {endTimeServer}");
            await _gpiLotteryService.ImportAsync(startTimeServer, endTimeServer);

            var saveTimeTrackerParam = new InsertTimeTrackerParam
            {
                ProductId = Products.GPILotteryId,
                CallTime = endTime
            };
            _timeTracker.SaveTime(saveTimeTrackerParam);
            Logger.Info($"{ServiceName} - Finished get data");
        }
    }
}
