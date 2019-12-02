using System;
using System.Globalization;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Gpi.MetaModels;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Job.MetaModels.TimeTracker;
using Fanex.Affiliate.DataService.Job.Services;
using Fanex.Logging;
using Quartz;
using Sunplus.UMControllerLibrary;
using System.Threading.Tasks;
using ONEbook.UM.NonWebApp;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    [DisallowConcurrentExecution]
    public class CrystalGpiJob : IJob
    {
        private readonly IAppSettings _appSetting;
        private readonly ICrystalGpiService _crystalGpiService;
        private readonly ITimeTracker _timeTracker;

        public CrystalGpiJob()
        {
            _appSetting = new AppSettings();
            _crystalGpiService = new CrystalGpiService();
            _timeTracker = new TimeTracker();
        }

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                var isUm = UMDefaultInstances.MaintenanceService.IsUnderMaintenance();

                if (!isUm)
                {
                    var defaultStartTime = DateTime.ParseExact(_appSetting.GetValue("CrystalGpiStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
                    var trackerParam = new TimeTrackerBaseParam { ProductId = Products.CrystalProductId };
                    var lastQueryTime = _timeTracker.GetLastestCallSuccess(trackerParam, defaultStartTime);

                    var hourAdjustment = Convert.ToInt32(_appSetting.GetValue("CrystalGpiHourAdjustment"));
                    var now = DateTime.Now.AddHours(hourAdjustment);
                    var nowByHour = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0, DateTimeKind.Local);

                    if (DateTime.Compare(lastQueryTime, nowByHour) < 0)
                    {
                        var startTime = new DateTime(lastQueryTime.Year, lastQueryTime.Month, lastQueryTime.Day, lastQueryTime.Hour, lastQueryTime.Minute, lastQueryTime.Second);
                        var endTime = new DateTime(nowByHour.Year, nowByHour.Month, nowByHour.Day, nowByHour.Hour, nowByHour.Minute, nowByHour.Second);

                        for (var start = startTime; DateTime.Compare(start, endTime) < 0; start = start.AddHours(1))
                        {
                            var endTimeByBatch = start.AddHours(1);
                            var param = new CrystalGpiParam
                            {
                                StartTime = start,
                                EndTime = DateTime.Compare(endTimeByBatch, endTime) > 0 ? endTime : endTimeByBatch,
                                PageNumber = 1,
                                PageSize = Convert.ToInt32(_appSetting.GetValue("CrystalGpiPageSize"))
                            };
                            var nextStartTime = param.EndTime;

                            _crystalGpiService.Import(param);

                            var saveTimeParam = new InsertTimeTrackerParam
                            {
                                ProductId = Products.CrystalProductId,
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
