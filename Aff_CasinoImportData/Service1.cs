using System;
using System.ServiceProcess;
using System.Threading;
using Aff_CasinoImportDataJob;
using Fanex.Logging;

namespace Aff_CasinoImportData
{
    public partial class Service1 : ServiceBase
    {
        private Timer serviceTimer;
        private ImportCasinoDataJob _job;

        public Service1()
        {
            InitializeComponent();
            _job = new ImportCasinoDataJob();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                TimerCallback timerDelegate = new TimerCallback(_job.DoWork);
                //call Dowork after the time is configured as TimeInterval in web.config
                serviceTimer = new Timer(timerDelegate, null, int.Parse(Common.DueTime), int.Parse(Common.TimeInterval));
            }
            catch (Exception ex)
            {
                LogService.Error("AFF Import Casino Data: " + ex.Message, ex);
            }
        }
    }
}