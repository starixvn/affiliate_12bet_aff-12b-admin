using System;
using System.Globalization;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Constants;
using Fanex.Affiliate.DataService.Importer.MetaModels.ISoftbet;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Fanex.Affiliate.DataService.Job.MetaModels.TimeTracker;
using Fanex.Affiliate.DataService.Job.Services;
using Fanex.Logging;
using Quartz;
using Sunplus.UMControllerLibrary;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    [DisallowConcurrentExecution]
    public class ISoftbetJob : IJob
    {
        private readonly IAppSettings _appSetting;
        private readonly UMBusiness _umBusiness;
        private readonly IISoftbetService _isoftbetService;
        private readonly ITimeTracker _timeTracker;

        public ISoftbetJob()
        {
            _appSetting = new AppSettings();
            _umBusiness = new UMBusiness();
            _isoftbetService = new ISoftbetService();
            _timeTracker = new TimeTracker();
        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                var umUrl = _appSetting.GetValue("LBUrl");
                var isUm = !string.IsNullOrEmpty(umUrl) && _umBusiness.isUnderMaintainence(Sites.Aff12Bet, umUrl);

                if (!isUm)
                {
                    var defaultStartTime = DateTime.ParseExact(_appSetting.GetValue("ISoftbetStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
                    var trackerParam = new TimeTrackerBaseParam { ProductId = Products.ISoftbetProductId };
                    var lastQueryTime = _timeTracker.GetLastestCallSuccess(trackerParam, defaultStartTime);

                    var hourAdjustment = Convert.ToInt32(_appSetting.GetValue("ISoftbetHourAdjustment"));
                    var now = DateTime.Now.AddHours(hourAdjustment);
                    var nowByMinute = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0, DateTimeKind.Local);

                    if (DateTime.Compare(lastQueryTime, nowByMinute) < 0)
                    {
                        var isoftbetParam = new ISoftbetEntryParam
                        {
                            StartTime = lastQueryTime,
                            EndTime = nowByMinute,
                            PageNumber = 1
                        };

                        _isoftbetService.Import(isoftbetParam);

                        var saveTimeParam = new InsertTimeTrackerParam
                        {
                            ProductId = Products.ISoftbetProductId,
                            CallTime = nowByMinute
                        };
                        _timeTracker.SaveTime(saveTimeParam);
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
