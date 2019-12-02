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
    public class GpiLotteryJob : IJob
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

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var isUm = UMDefaultInstances.MaintenanceService.IsUnderMaintenance();

                if (!isUm)
                {
                    var defaultStartTime = DateTime.ParseExact(_appSetting.GetValue("GpiLotteryStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
                    var param = new TimeTrackerBaseParam { ProductId = Products.GPILotteryId };
                    var lastQueryTime = _timeTracker.GetLastestCallSuccess(param, defaultStartTime);
                    var previousHour = DateTime.Now.AddHours(-1);

                    if (DateTime.Compare(lastQueryTime, previousHour) < 0)
                    {
                        var startTime = lastQueryTime;
                        var endTime = previousHour;

                        var startTimeServer = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(startTime, TimeZoneId.GmtPlus8);
                        var endTimeServer = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(endTime, TimeZoneId.GmtPlus8);
                        await _gpiLotteryService.ImportAsync(startTimeServer, endTimeServer);

                        var saveTimeTrackerParam = new InsertTimeTrackerParam
                        {
                            ProductId = Products.GPILotteryId,
                            CallTime = endTime
                        };
                        _timeTracker.SaveTime(saveTimeTrackerParam);
                    }
                }
            }
            catch (Exception exception)
            {
                LogService.Error(exception.Message, exception);
            }

        }
    }
}
