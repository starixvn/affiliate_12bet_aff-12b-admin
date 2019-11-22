using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Data;

namespace Fanex.Affiliate.DataService.WinFormApp
{
    public partial class Form1 : Form
    {
        private const string DateFormatForFile = "yyyyMMdd";
        private IEnumerable<string> _selectedFiles;
        private readonly CultureInfo cultureInfo = new CultureInfo("en-US");

        public Form1()
        {
            InitializeComponent();
            DbSettingProviderManager
                .StartNewSession()
                .UseDefaultConnectionStringProvider()
                .UseDefaultDbSettingProvider(
                    resourcePath: "~/App_Data/SpName.xml",
                    ignoreRedundantParameters: false
                )
                .Run();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            Reset();

            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Chose File";
                dialog.Filter = "csv Files(*.csv)|*.csv|All files (*.*)|*.*";
                dialog.RestoreDirectory = true;
                dialog.Multiselect = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _selectedFiles = dialog.FileNames;
                    txtFileName.Text = string.Join(",", _selectedFiles);
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = "Processing...";

                var result = rdImportByDate.Checked ? ImportByDate() : ImportByFiles();

                lblStatus.Text = string.IsNullOrEmpty(result) ? "Done" : result;
            }
            catch (Exception exception)
            {
                lblStatus.Text = exception.Message;
            }
        }

        private string ImportByDate()
        {
            var errorMessage = new StringBuilder();
            var fromDate = fromDatePicker.Value.Date;
            var toDate = toDatePicker.Value.Date;

            if (fromDate > toDate)
            {
                return "From Date must be less or equal to To Date";
            }

            var service = new CasinoDataService();
            while (fromDate <= toDate)
            {
                try
                {
                    var result = service.ImportData(fromDate);

                    if (!result.IsSuccessful)
                    {
                        errorMessage.AppendLine($"Error when importing by Date: {fromDate}, Code: {result.Code} - Message: {result.Message}");
                    }
                }
                catch (Exception exception)
                {
                    var webException = exception as WebException;

                    if (webException == null)
                    {
                        errorMessage.AppendLine($"Exception: {exception.Message}, Date: {fromDate.Date}");
                    }
                    else
                    {
                        errorMessage.AppendLine($"File Not Found - Date: {fromDate.Date}");
                    }

                    errorMessage.Append(exception.Message);
                }

                fromDate = fromDate.AddDays(1);
            }

            return errorMessage.ToString();
        }

        private string ImportByFiles()
        {
            var errorMessage = new StringBuilder();

            if (_selectedFiles == null)
            {
                return "You have not chosen any files";
            }

            var service = new CasinoDataService();
            foreach (var file in _selectedFiles)
            {
                var date = GetDateByFileName(file);

                if (!date.HasValue)
                {
                    errorMessage.AppendLine($"Error when importing by files: {file}, Can not get date by file name");
                }
                else
                {
                    var result = service.ImportData(date.GetValueOrDefault().Date, file);

                    if (!result.IsSuccessful)
                    {
                        errorMessage.AppendLine($"Error when importing by files: {file}, Code: {result.Code} - Message: {result.Message}");
                    }
                }
            }

            return errorMessage.ToString();
        }

        private DateTime? GetDateByFileName(string fileName)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

            var temp = fileNameWithoutExtension.Replace("Casino ", "");
            DateTime value;
            if (DateTime.TryParseExact(temp, DateFormatForFile, cultureInfo, DateTimeStyles.None, out value))
            {
                return value;
            }

            return null;
        }

        private void rdImportByDate_CheckedChanged(object sender, EventArgs e)
        {
            ToggleImportByDateArea(rdImportByDate.Checked);
        }

        private void rdImportByFiles_CheckedChanged(object sender, EventArgs e)
        {
            _selectedFiles = null;
            txtFileName.Text = string.Empty;

            ToggleImportByFilesArea(rdImportByFiles.Checked);
        }

        private void ToggleImportByDateArea(bool isEnable)
        {
            fromDatePicker.Enabled = isEnable;
            toDatePicker.Enabled = isEnable;
        }

        private void ToggleImportByFilesArea(bool isEnable)
        {
            txtFileName.Enabled = isEnable;
            btnBrowse.Enabled = isEnable;
        }

        private void Reset()
        {
            _selectedFiles = null;
            lblStatus.Text = string.Empty;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }
    }
}