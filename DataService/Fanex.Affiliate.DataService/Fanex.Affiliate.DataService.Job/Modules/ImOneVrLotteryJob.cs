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
    public class ImOneVrLotteryJob : JobBase
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

        protected override async Task ImportDataToSystem(IJobExecutionContext context = null)
        {
            var defaultStartTime = DateTime.ParseExact(_appSetting.GetValue("VrLotteryStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
            var param = new TimeTrackerBaseParam { ProductId = Products.VRLotteryId };
            var lastQueryTime = _timeTracker.GetLastestCallSuccess(param, defaultStartTime);
            var delayTime = int.Parse(_appSetting.GetValue("VrLotteryDelayTime"));
            var now = DateTime.Now;
            var previousTime = now.AddMinutes(-delayTime);

            if (DateTime.Compare(lastQueryTime, previousTime) < 0)
            {
                var startTime = lastQueryTime;
                var endTime = previousTime;

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
}
