using System;
using System.IO;
using System.ServiceProcess;
using Fanex.Affiliate.DataService.CheckSum;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Job;
using Fanex.Logging;
using Sunplus.UMControllerLibrary;

namespace Fanex.Affiliate.DataService.WinService
{
    public partial class Service1 : ServiceBase
    {
        private readonly IDailyMemberCheckSumRunner _dailyMemberCheckSumRunner;
        private readonly JobScheduler _scheduler;
        private readonly IDataImportingRunner _dataImportingRunner;
        private readonly UMBusiness _umBusiness;

        public Service1()
        {
            InitializeComponent();
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            _scheduler = new JobScheduler();
            _dailyMemberCheckSumRunner = new DailyMemberCheckSumRunner();
            _dataImportingRunner = new DataImportingRunner();
            _umBusiness = new UMBusiness();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                LogService.Info("Start 12bet AFF service");
                _scheduler.Start(_dailyMemberCheckSumRunner, _dataImportingRunner, _umBusiness);
            }
            catch (Exception exception)
            {
                LogService.Error(exception.Message, exception.InnerException);
            }
        }

        protected override void OnStop()
        {
            try
            {
                LogService.Info("Stop 12bet AFF service");

                _scheduler.Stop();
            }
            catch (Exception exception)
            {
                LogService.Error(exception.Message, exception.InnerException);
            }
        }
    }
}