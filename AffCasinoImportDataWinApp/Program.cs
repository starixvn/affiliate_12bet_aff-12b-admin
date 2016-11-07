using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AffCasinoImportDataWinApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            Fanex.Data.Configuration.DbSettingProviderManager.Current.Start();
            Application.Run(new MainForm());
        }
    }
}
