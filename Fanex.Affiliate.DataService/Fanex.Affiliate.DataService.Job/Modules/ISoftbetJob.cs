using System;
using System.Globalization;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.MetaModels.ISoftbet;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Fanex.Affiliate.DataService.Job.Services;
using Fanex.Logging;
using Quartz;
using Sunplus.UMControllerLibrary;

namespace Fanex.Affiliate.DataService.Job.Modules
{
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
                    var trackerFolderPath = _appSetting.GetValue("ISoftbetTrackerFolder");
                    var defaultStartTime = DateTime.ParseExact(_appSetting.GetValue("ISoftbetStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
                    var lastQueryTime = _timeTracker.GetLastestCallSuccess(trackerFolderPath, defaultStartTime);
                    var now = DateTime.Now;
                    var nowByHour = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0, DateTimeKind.Local);

                    var isoftbetParam = new ISoftbetEntryParam
                    {
                        StartTime = lastQueryTime,
                        EndTime = nowByHour,
                        PageNumber = 1
                    };

                    _isoftbetService.Import(isoftbetParam);

                    _timeTracker.SaveTime(trackerFolderPath, nowByHour);
                }
            }
            catch (Exception exception)
            {
                LogService.Error(exception.Message, exception);
            }
        }
    }
}
