using System;
using System.Text;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.MetaModels.CrystalGpi;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Fanex.Logging;
using Quartz;
using Sunplus.UMControllerLibrary;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    public class CrystalGpiDailyJob : IJob
    {
        private readonly IAppSettings _appSetting;
        private readonly UMBusiness _umBusiness;
        private readonly ICrystalGpiService _crystalGpiService;

        public CrystalGpiDailyJob()
        {
            _appSetting = new AppSettings();
            _umBusiness = new UMBusiness();
            _crystalGpiService = new CrystalGpiService();
        }

        public void Execute(IJobExecutionContext context)
        {
            var logBuilder = new StringBuilder();
            try
            {
                var umUrl = _appSetting.GetValue("LBUrl");
                var isUm = !string.IsNullOrEmpty(umUrl) && _umBusiness.isUnderMaintainence(Sites.Aff12Bet, umUrl);

                if (!isUm)
                {
                    var now = DateTime.Now;
                    var nowByHour = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0, DateTimeKind.Local);

                    var param = new CrystalGpiParam
                    {
                        StartTime = nowByHour.AddDays(-1).Date,
                        EndTime = nowByHour,
                        PageNumber = 1,
                        PageSize = Convert.ToInt32(_appSetting.GetValue("CrystalGpiPageSize")),
                        JobType = 1
                    };

                    logBuilder.AppendLine($"Crystal Gpi Daily Job is starting. Request time {param.StartTime.ToString()} - {param.EndTime.ToString()}");

                    _crystalGpiService.Import(param);

                    logBuilder.AppendLine("Crystal Gpi Daily Job Done");
                }
            }
            catch (Exception exception)
            {
                logBuilder.AppendLine(exception.Message);
                LogService.Error(logBuilder.ToString(), exception);
            }
        }
    }
}
