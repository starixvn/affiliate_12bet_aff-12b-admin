using Fanex.Affiliate.DataService.CheckSum.Utilities;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Constants;
using Fanex.Affiliate.DataService.Job.MetaModels.TimeTracker;
using Fanex.Affiliate.DataService.Job.Services;
using Fanex.Logging;
using Quartz;
using Sunplus.UMControllerLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    public class LBKenoJob : IJob
    {
        private readonly IAppSettings _appSetting;
        private readonly UMBusiness _umBusiness;
        private readonly IImporterService service;
        private readonly ITimeTracker _timeTracker;

        public LBKenoJob()
        {
            _appSetting = new AppSettings();
            _umBusiness = new UMBusiness();
            service = new LBKenoService();
            _timeTracker = new TimeTracker();
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var umUrl = _appSetting.GetValue("LBUrl");
                var isUm = !string.IsNullOrEmpty(umUrl) && _umBusiness.isUnderMaintainence(Sites.Aff12Bet, umUrl);

                if (!isUm)
                {
                    var defaultStartTime = DateTime.ParseExact(_appSetting.GetValue("TopazStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
                    var param = new TimeTrackerBaseParam { ProductId = Products.LBKenoId };
                    var lastQueryTime = _timeTracker.GetLastestCallSuccess(param, defaultStartTime);

                    var now = DateTime.Now;

                    if (DateTime.Compare(lastQueryTime, now) < 0)
                    {
                        var startTime = new DateTime(lastQueryTime.Year, lastQueryTime.Month, lastQueryTime.Day, lastQueryTime.Hour, lastQueryTime.Minute, lastQueryTime.Second);
                        var endTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second).AddDays(-1);

                        var startTimeServer = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(startTime, TimeZoneId.GmtPlus8);
                        var endTimeServer = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(endTime, TimeZoneId.GmtPlus8);

                        await service.ImportAsync(startTimeServer, endTimeServer);

                        var saveTimeTrackerParam = new InsertTimeTrackerParam
                        {
                            ProductId = Products.LBKenoId,
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
