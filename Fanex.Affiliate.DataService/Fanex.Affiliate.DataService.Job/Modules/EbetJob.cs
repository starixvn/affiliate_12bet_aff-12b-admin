using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.MetaModels.Ebet;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Fanex.Affiliate.DataService.Job.Services;
using Fanex.Logging;
using Quartz;
using Sunplus.UMControllerLibrary;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    public class EbetJob : IJob
    {
        private readonly IAppSettings _appSetting;
        private readonly UMBusiness _umBusiness;
        private readonly IEbetService _ebetService;
        private readonly ITimeTracker _timeTracker;
        private readonly IEbetConfigHelper _ebetConfigHelper;

        public EbetJob()
        {
            _appSetting = new AppSettings();
            _umBusiness = new UMBusiness();
            _ebetService = new EbetService();
            _timeTracker = new TimeTracker();
            _ebetConfigHelper = new EbetConfigHelper();
        }

        public async void Execute(IJobExecutionContext context)
        {
            try
            {
                var umUrl = _appSetting.GetValue("LBUrl");
                var isUm = !string.IsNullOrEmpty(umUrl) && _umBusiness.isUnderMaintainence(Sites.Aff12Bet, umUrl);

                if (!isUm)
                {
                    var trackerFolderPath = _appSetting.EbetTrackerFolder;
                    var defaultStartTime = _appSetting.EbetStartTime;

                    var lastQueryTime = _timeTracker.GetLastestCallSuccess(trackerFolderPath, defaultStartTime);
                    var now = DateTime.Now;
                    var nowByHour = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0, DateTimeKind.Local);

                    var taskList = new List<Task>();
                    var configList = _ebetConfigHelper.GetConfigList();
                    foreach (var config in configList)
                    {
                        var param = new EbetParam
                        {
                            UserName = string.Empty,
                            StartTime = lastQueryTime,
                            EndTime = nowByHour,
                            ChannelId = config.ChannelId,
                            ApiUrl = config.ApiServerHost,
                            PrivateKey = config.PrivateKey,
                            CurrencyId = config.CurrencyID,
                            PageNumber = 1,
                            PageSize = _appSetting.EbetPageSize,
                        };

                        taskList.Add(_ebetService.Import(param));
                    }

                    await Task.WhenAll(taskList);

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
