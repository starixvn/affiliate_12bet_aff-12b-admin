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

namespace Fanex.Affiliate.DataService.Job.Modules
{
    [DisallowConcurrentExecution]
    public class GpiLotteryJob : JobBase
    {
        private readonly IAppSettings _appSetting;
        private readonly IImporterService _gpiLotteryService;
        private readonly ITimeTracker _timeTracker;

        public GpiLotteryJob()
        {
            _appSetting = new AppSettings();
            _gpiLotteryService = new GpiLotteryService();
            _timeTracker = new TimeTracker();
        }

        protected override async Task ImportDataToSystem(IJobExecutionContext context = null)
        {
            var defaultStartTime = DateTime.ParseExact(_appSetting.GetValue("GpiLotteryStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
            var param = new TimeTrackerBaseParam { ProductId = Products.GPILotteryId };
            var lastQueryTime = _timeTracker.GetLastestCallSuccess(param, defaultStartTime);
            var now = DateTime.Now;
            var previousHour = now.AddHours(-1);

            Logger.Info($"Start GpiLottery service at {now}");
            if (DateTime.Compare(lastQueryTime, previousHour) < 0)
            {
                var startTime = lastQueryTime;
                var endTime = previousHour;

                var startTimeServer = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(startTime, TimeZoneId.GmtPlus8);
                var endTimeServer = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(endTime, TimeZoneId.GmtPlus8);

                Logger.Info($"GpiLottery service - Get data from {startTimeServer} to {endTimeServer}");
                await _gpiLotteryService.ImportAsync(startTimeServer, endTimeServer);

                var saveTimeTrackerParam = new InsertTimeTrackerParam
                {
                    ProductId = Products.GPILotteryId,
                    CallTime = endTime
                };
                _timeTracker.SaveTime(saveTimeTrackerParam);
                Logger.Info($"GpiLottery service - Finish get data");
            }
        }
    }
}
