using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using AffCasinoImportDataAccess;

namespace AffCasinoImportDataWinApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            base.OnLoad(e);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            var startDate = dateTimePicker1.Value.Date;
            var endDate = dateTimePicker2.Value.Date;
            if(startDate > endDate)
            {
                MessageBox.Show("Wrong date range!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            if (MessageBox.Show("Do you want to import data from " + startDate.ToShortDateString() + " to " + endDate.ToShortDateString() + "?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            EnableInputs(false);
            lblStatus.Text = "Status: Running...";
            Application.DoEvents();
            for (DateTime date = startDate; date.Date <= endDate.Date; date = date.AddDays(1))
            {
                lblStatus.Text = "Status: Importing for " + date.ToShortDateString();
                Application.DoEvents();
                ImportCasinoData(date);
            }

            this.lblStatus.Text = "Status: Done for " + startDate.ToShortDateString() + " -> " + endDate.ToShortDateString();
            EnableInputs(true);
        }

        private void EnableInputs(bool isEnabled)
        {
            btnImport.Enabled = isEnabled;
            dateTimePicker1.Enabled = isEnabled;
            dateTimePicker2.Enabled = isEnabled;
        }

        private void ImportCasinoData(DateTime date)
        {
            int errCode = 0;
            string errMsg = string.Empty;
            var filePath = Common.DownloadCsvFileFromApi(date);
            XmlDocument docXML = Common.ConvertCSVToXML(filePath);

            DataAccess.ImportCasinoData(Common.ForSystem, Common.AdminID, docXML.OuterXml, date, ref errCode, ref errMsg);

            if (errCode != 0)
            {
                MessageBox.Show(errCode + ": " + errMsg);
            }
        }
    }
}