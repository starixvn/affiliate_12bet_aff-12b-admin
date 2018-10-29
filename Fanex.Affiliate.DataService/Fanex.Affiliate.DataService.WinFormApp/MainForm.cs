using System;
using System.Windows.Forms;
using Fanex.Affiliate.DataService.WinFormApp.Forms;

namespace Fanex.Affiliate.DataService.WinFormApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
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
    }
}
