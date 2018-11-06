using System;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.MetaModels.Betsoft;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Fanex.Affiliate.DataService.Job.Services;
using Fanex.Logging;
using Quartz;
using Sunplus.UMControllerLibrary;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    public class BetsoftJob : IJob
    {
        private readonly IAppSettings _appSetting;
        private readonly UMBusiness _umBusiness;
        private readonly IBetsoftService _betSoftService;
        private readonly ITimeTracker _timeTracker;

        public BetsoftJob()
        {
            _appSetting = new AppSettings();
            _umBusiness = new UMBusiness();
            _betSoftService = new BetsoftService();
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
                    var trackerFolderPath = _appSetting.BetsoftTimeTrackerFolder;
                    var lastQueryTime = _timeTracker.GetLastestCallSuccess(trackerFolderPath, _appSetting.BetsoftStartTime);
                    var now = DateTime.Now;
                    var nowByHour = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0, DateTimeKind.Local);

                    var betSoftParam = new BetsoftParam
                    {
                        StartDate = lastQueryTime,
                        EndDate = nowByHour
                    };

                    _betSoftService.Import(betSoftParam);

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
