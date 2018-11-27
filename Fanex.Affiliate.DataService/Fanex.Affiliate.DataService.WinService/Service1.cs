using System;
using System.IO;
using System.ServiceProcess;
using Fanex.Affiliate.DataService.Job;
using Fanex.Logging;

namespace Fanex.Affiliate.DataService.WinService
{
    public partial class Service1 : ServiceBase
    {
        private readonly IJobSchedulerFactory _schedulerFactory;

        public Service1()
        {
            InitializeComponent();
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            _schedulerFactory = new JobSchedulerFactory();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                LogService.Info("Start 12bet AFF service");
                _schedulerFactory.SetUp();
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
                _schedulerFactory.Shutdown();
            }
            catch (Exception exception)
            {
                LogService.Error(exception.Message, exception.InnerException);
            }
        }

        protected override void OnPause()
        {
            base.OnPause();

            this._schedulerFactory.Pause();
        }

        protected override void OnContinue()
        {
            base.OnContinue();

            this._schedulerFactory.Resume();
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();
            this._schedulerFactory.Shutdown();
        }
    }
}