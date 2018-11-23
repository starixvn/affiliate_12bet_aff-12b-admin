using System;
using System.Windows.Forms;
using Fanex.Affiliate.DataService.WinFormApp.Forms;
using Fanex.Data.Configuration;

namespace Fanex.Affiliate.DataService.WinFormApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            DbSettingProviderManager.Current.Start();
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
    }
}
