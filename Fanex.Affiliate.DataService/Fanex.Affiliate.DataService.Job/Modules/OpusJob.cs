using System;
using System.Globalization;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.MetaModels.Opus;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Fanex.Affiliate.DataService.Job.Services;
using Fanex.Logging;
using Quartz;
using Sunplus.UMControllerLibrary;

namespace Fanex.Affiliate.DataService.Job.Modules
{
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

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                var umUrl = _appSetting.GetValue("LBUrl");
                var isUm = !string.IsNullOrEmpty(umUrl) && _umBusiness.isUnderMaintainence(Sites.Aff12Bet, umUrl);

                if (!isUm)
                {
                    var trackerFolderPath = _appSetting.GetValue("OpusTrackerFolder");
                    var defaultStartTime = DateTime.ParseExact(_appSetting.GetValue("OpusStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
                    var lastQueryTime = _timeTracker.GetLastestCallSuccess(trackerFolderPath, defaultStartTime);
                    var now = DateTime.Now;
                    var nowByMinute = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0, DateTimeKind.Local);

                    if (DateTime.Compare(lastQueryTime, nowByMinute) < 0)
                    {
                        var opusParam = new OpusParam
                        {
                            StartTime = lastQueryTime,
                            EndTime = nowByMinute
                        };

                        _opusService.Import(opusParam);

                        _timeTracker.SaveTime(trackerFolderPath, nowByMinute);
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
