using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using Fanex.Affiliate.DataService.Importer.BigGaming;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.WinFormApp.Forms;
using Fanex.Data;
using Fanex.Data.SqlServer;
using Fanex.Logging;
using Fanex.Logging.Sentry.RavenClient;
using SharpRaven;

namespace Fanex.Affiliate.DataService.WinFormApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            DbSettingProviderManager
                .StartNewSession()
                .WithSqlServer("~/App_Data")
                .Run();
            InitLogging();
            InitializeComponent();
        }

        private static void InitLogging()
        {
            try
            {
                SetupFanexLogging();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }

        }

        private static void SetupFanexLogging()
        {
            var env = ConfigurationManager.AppSettings["Environment"];
            var dsn = ConfigurationManager.AppSettings["SentryDsn"];
            var category = ConfigurationManager.AppSettings["LoggingCategory"];
            if (!string.IsNullOrEmpty(dsn))
            {
                LogManager.SetDefaultLogCategory(category)
                    .Use(new List<ILogEngine>
                    {
                        new SentryLogging(new SentryEngineOptions
                        {
                            Dsn = new Dsn(dsn),
                            Environment = env
                        })

                    });
            }
        }

        private void btnCasino_Click(object sender, EventArgs e)
        {
            var casinoForm = new Form1();
            casinoForm.FormClosing += FormClosingHandler;
            casinoForm.Show();
            this.Hide();
        }

        private void btnBetsoft_Click(object sender, EventArgs e)
        {
            var betsoftForm = new BetsoftForm();
            betsoftForm.FormClosing += FormClosingHandler;
            betsoftForm.Show();
            this.Hide();
        }

        private void FormClosingHandler(object sender, FormClosingEventArgs e)
        {
            this.Show();
        }

        private void btnEbet_Click(object sender, EventArgs e)
        {
            var ebetForm = new EbetForm();
            ebetForm.FormClosing += FormClosingHandler;
            ebetForm.Show();
            this.Hide();
        }

        private void btnCrystal_Click(object sender, EventArgs e)
        {
            var crystalForm = new CrystalForm();
            crystalForm.FormClosing += FormClosingHandler;
            crystalForm.Show();
            this.Hide();
        }

        //private void btnQuartz_Click(object sender, EventArgs e)
        //{
        //    var quartzForm = new ISoftbetForm();
        //    quartzForm.FormClosing += FormClosingHandler;
        //    quartzForm.Show();
        //    this.Hide();
        //}

        //private void btnOpus_Click(object sender, EventArgs e)
        //{
        //    var opusForm = new OpusForm();
        //    opusForm.FormClosing += FormClosingHandler;
        //    opusForm.Show();
        //    this.Hide();
        //}

        private void btnTopaz_Click(object sender, EventArgs e)
        {
            var topazForm = new TopazForm();
            SetUpServiceForm(topazForm);
        }

        private void btnGpiLottery_Click(object sender, EventArgs e)
        {
            var gpiForm = new GpiLotteryForm();
            SetUpServiceForm(gpiForm);
        }

        private void btnVrLottery_Click(object sender, EventArgs e)
        {
            var form = new VrLotteryForm();
            SetUpServiceForm(form);
        }

        private void btLBKeno_Click(object sender, EventArgs e)
        {
            var form = new LBKenoForm();
            SetUpServiceForm(form);
        }

        private void SetUpServiceForm(Form form)
        {
            form.FormClosing += FormClosingHandler;
            form.Show();
            this.Hide();
        }

        private void importFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FileImportForm();
            SetUpServiceForm(form);
        }

        private void btnKYGaming_Click(object sender, EventArgs e)
        {
            var form = new KYGamingForm();
            SetUpServiceForm(form);
        }

        private void btnBTi_Click(object sender, EventArgs e)
        {
            var form = new BTiForm();
            SetUpServiceForm(form);
        }

        private void btnIMEsport_Click(object sender, EventArgs e)
        {
            var form = new IMEsportForm();
            SetUpServiceForm(form);
        }

        private void btnSkywind_Click(object sender, EventArgs e)
        {
            var form = new SkywindForm();
            SetUpServiceForm(form);
        }

        private void btnPlaytech_Click(object sender, EventArgs e)
        {
            var form = new PlaytechForm();
            SetUpServiceForm(form);
        }

        private void btnJasper_Click(object sender, EventArgs e)
        {
            var form = new JasperForm();
            SetUpServiceForm(form);
        }

        private void btnSunstone_Click(object sender, EventArgs e)
        {
            var form = new SunstoneForm();
            SetUpServiceForm(form);
        }

        private void btnBgLiveCasino_Click(object sender, EventArgs e)
        {
            var form = new BigGamingForm(new BgLiveCasinoService());
            SetUpServiceForm(form);
        }

        private void btnBgFish_Click(object sender, EventArgs e)
        {
            var form = new BigGamingForm(new BgFishService());
            SetUpServiceForm(form);
        }
    }
}
