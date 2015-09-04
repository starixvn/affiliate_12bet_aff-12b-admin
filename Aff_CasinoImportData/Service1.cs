using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Xml;
using log4net;
using DNAide.Utils;
using DNAide.Web;
//using Fanex.Logging;
using Fanex.MailDeliver.Model;
using Fanex.MailDeliver.MailClient.API;
using Aff_CasinoImportDataJob;


namespace Aff_CasinoImportData
{
    public partial class Service1 : ServiceBase
    {
        private Timer serviceTimer;
        private ImportCasinoDataJob _job;
        //private ILog log;
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
                DNAideLog.Write(ex.Message);
            }
        }
    }
}
