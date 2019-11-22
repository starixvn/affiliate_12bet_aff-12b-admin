using System;
using System.Globalization;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Constants;
using Fanex.Affiliate.DataService.Importer.MetaModels.Opus;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Fanex.Affiliate.DataService.Job.MetaModels.TimeTracker;
using Fanex.Affiliate.DataService.Job.Services;
using Fanex.Logging;
using Quartz;
using Sunplus.UMControllerLibrary;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    [DisallowConcurrentExecution]
    public class OpusJob : IJob
    {
        private readonly IAppSettings _appSetting;
        private readonly UMBusiness _umBusiness;
        private readonly IOpusService _opusService;
        private readonly ITimeTracker _timeTracker;

        public OpusJob()
        {
            _appSetting = new AppSettings();
            _umBusiness = new UMBusiness();
            _opusService = new OpusService();
            _timeTracker = new TimeTracker();
        }

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                var umUrl = _appSetting.GetValue("LBUrl");
                var isUm = !string.IsNullOrEmpty(umUrl) && _umBusiness.isUnderMaintainence(Sites.Aff12Bet, umUrl);

                if (!isUm)
                {
                    var defaultStartTime = DateTime.ParseExact(_appSetting.GetValue("OpusStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
                    var trackerParam = new TimeTrackerBaseParam { ProductId = Products.OpusProductId };
                    var lastQueryTime = _timeTracker.GetLastestCallSuccess(trackerParam, defaultStartTime);

                    var hourAdjustment = Convert.ToInt32(_appSetting.GetValue("OpusHourAdjustment"));
                    var now = DateTime.Now.AddHours(hourAdjustment);
                    var nowByMinute = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0, DateTimeKind.Local);

                    if (DateTime.Compare(lastQueryTime, nowByMinute) < 0)
                    {
                        var startTime = new DateTime(lastQueryTime.Year, lastQueryTime.Month, lastQueryTime.Day, lastQueryTime.Hour, lastQueryTime.Minute, lastQueryTime.Second);
                        var endTime = new DateTime(nowByMinute.Year, nowByMinute.Month, nowByMinute.Day, nowByMinute.Hour, nowByMinute.Minute, nowByMinute.Second);
                        for (var start = startTime; DateTime.Compare(start, endTime) < 0; start = start.AddHours(1))
                        {
                            var endTimeByBatch = start.AddHours(1);
                            var opusParam = new OpusParam
                            {
                                StartTime = start,
                                EndTime = DateTime.Compare(endTimeByBatch, endTime) > 0 ? endTime : endTimeByBatch
                            };
                            var nextStartTime = opusParam.EndTime;

                            _opusService.Import(opusParam);

                            var saveTimeParam = new InsertTimeTrackerParam
                            {
                                ProductId = Products.OpusProductId,
                                CallTime = nextStartTime
                            };
                            _timeTracker.SaveTime(saveTimeParam);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                LogService.Error(exception.Message, exception);
            }

            return Task.CompletedTask;
        }
    }
}