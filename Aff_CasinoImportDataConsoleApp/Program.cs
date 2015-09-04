using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DNAide.Web;
using Aff_CasinoImportDataJob;

namespace Aff_CasinoImportDataConsoleApp
{
    class Program
    {
        private static  Timer serviceTimer;
        private static ImportCasinoDataJob _job = new ImportCasinoDataJob();
        static void Main(string[] args)
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

            Console.ReadLine();
        }
    }
}
