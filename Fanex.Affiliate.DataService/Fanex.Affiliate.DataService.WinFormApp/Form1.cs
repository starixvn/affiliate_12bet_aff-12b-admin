using System;
using System.Windows.Forms;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Data.Configuration;

namespace Fanex.Affiliate.DataService.WinFormApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DbSettingProviderManager.Current.Start();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Chose File";
            dialog.Filter = "csv Files(*.csv)|*.csv|All files (*.*)|*.*";
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = dialog.FileName;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = "Processing...";

                var date = datePicker.Value;
                var fileName = txtFileName.Text;

                var service = new CasinoDataService();
                var result = service.ImportData(date, fileName);

                if (result.IsSuccessful)
                {
                    lblStatus.Text = "Done";
                }
                else
                {
                    lblStatus.Text = $"Code: {result.Code} - Message: {result.Message}";
                }
            }
            catch (Exception exception)
            {
                lblStatus.Text = exception.Message;
            }
        }
    }
}