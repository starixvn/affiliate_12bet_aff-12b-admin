using System;
using System.Globalization;
using Fanex.Affiliate.DataService.CheckSum.Utilities;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Constants;
using Fanex.Affiliate.DataService.Importer.MetaModels.CrystalGpi;
using Fanex.Affiliate.DataService.Job.MetaModels.TimeTracker;
using Fanex.Affiliate.DataService.Job.Services;
using Fanex.Logging;
using Quartz;
using Sunplus.UMControllerLibrary;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    [DisallowConcurrentExecution]
    public class CrystalGpiJob : IJob
    {
        private readonly IAppSettings _appSetting;
        private readonly UMBusiness _umBusiness;
        private readonly ICrystalGpiService _crystalGpiService;
        private readonly ITimeTracker _timeTracker;

        public CrystalGpiJob()
        {
            _appSetting = new AppSettings();
            _umBusiness = new UMBusiness();
            _crystalGpiService = new CrystalGpiService();
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
                    var defaultStartTime = DateTime.ParseExact(_appSetting.GetValue("CrystalGpiStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
                    var trackerParam = new TimeTrackerBaseParam { ProductId = Products.CrystalProductId };
                    var lastQueryTime = _timeTracker.GetLastestCallSuccess(trackerParam, defaultStartTime);

                    var hourAdjustment = Convert.ToInt32(_appSetting.GetValue("CrystalGpiHourAdjustment"));
                    var now = DateTime.Now.AddHours(hourAdjustment);
                    var nowByHour = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0, DateTimeKind.Local);

                    if (DateTime.Compare(lastQueryTime, nowByHour) < 0)
                    {
                        var param = new CrystalGpiParam
                        {
                            StartTime = lastQueryTime,
                            EndTime = nowByHour,
                            PageNumber = 1,
                            PageSize = Convert.ToInt32(_appSetting.GetValue("CrystalGpiPageSize"))
                        };

                        _crystalGpiService.Import(param);

                        var saveTimeParam = new InsertTimeTrackerParam
                        {
                            ProductId = Products.CrystalProductId,
                            CallTime = nowByHour
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
