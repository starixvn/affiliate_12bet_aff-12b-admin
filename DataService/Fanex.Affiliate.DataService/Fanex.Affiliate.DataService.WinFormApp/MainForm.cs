using System;
using System.Windows.Forms;
using Fanex.Affiliate.DataService.WinFormApp.Forms;
using Fanex.Data;
using Fanex.Data.SqlServer;

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
            InitializeComponent();
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

        private void btnQuartz_Click(object sender, EventArgs e)
        {
            var quartzForm = new ISoftbetForm();
            quartzForm.FormClosing += FormClosingHandler;
            quartzForm.Show();
            this.Hide();
        }

        private void btnOpus_Click(object sender, EventArgs e)
        {
            var opusForm = new OpusForm();
            opusForm.FormClosing += FormClosingHandler;
            opusForm.Show();
            this.Hide();
        }

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
    }
}
