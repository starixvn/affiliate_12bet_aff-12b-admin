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
    public class ImOneVrLotteryJob : NewApiJobBase
    {
        private readonly IAppSettings _appSetting;
        private readonly IImporterService _service;
        private readonly ITimeTracker _timeTracker;

        public override string ServiceName => "VR Lottery Serivce";
        public ImOneVrLotteryJob()
        {
            _appSetting = new AppSettings();
            _service = new VrLotteryService();
            _timeTracker = new TimeTracker();
        }

        protected override DateTime GetStartTimeRequest()
        {
            var param = new TimeTrackerBaseParam { ProductId = Products.VRLotteryId };
            var defaultStartTime = DateTime.ParseExact(_appSetting.GetValue("VrLotteryStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
            var lastQueryTime = _timeTracker.GetLastestCallSuccess(param, defaultStartTime);
            return lastQueryTime;
        }

        protected override DateTime GetEndTimeRequest()
        {
            var ajustHour = int.Parse(_appSetting.GetValue("VrLotteryHourAdjustment"));
            var now = DateTime.Now;
            var ajustTime = now.AddHours(ajustHour);
            return ajustTime;
        }

        protected override IEnumerable<Tuple<DateTime, DateTime>> GetTimeRange(DateTime startTime, DateTime endTime)
        {
            var intervalTime = int.Parse(_appSetting.GetValue("VrLotteryIntervalTime"));
            var timeRanges = DateTimeHelper.GetProportionTimeRange(startTime, endTime, TimeSpan.FromMinutes(intervalTime));
            return timeRanges;
        }

        protected override async Task ImportDataAndTrackImportTime(DateTime startTime, DateTime endTime)
        {
            var startTimeServer = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(startTime, TimeZoneId.GmtPlus8);
            var endTimeServer = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(endTime, TimeZoneId.GmtPlus8);

            Logger.Info($"{ServiceName} - Get data from {startTimeServer} to {endTimeServer}");
            await _service.ImportAsync(startTimeServer, endTimeServer);

            var saveTimeTrackerParam = new InsertTimeTrackerParam
            {
                ProductId = Products.VRLotteryId,
                CallTime = endTime
            };
            _timeTracker.SaveTime(saveTimeTrackerParam);
            Logger.Info($"{ServiceName} - Finish get data");
        }
    }
}
