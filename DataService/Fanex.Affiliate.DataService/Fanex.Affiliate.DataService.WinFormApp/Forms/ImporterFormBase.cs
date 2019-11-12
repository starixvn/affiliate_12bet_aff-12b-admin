using Fanex.Affiliate.DataService.Importer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class ImporterFormBase : Form
    {
        protected IImporterService Service;
        protected Logger Logger;
        protected IEnumerable<string> RawData { get; set; }
        public ImporterFormBase()
        {
            InitializeComponent();
            this.Logger = new Logger(this.richTextLog);
            RawData = new List<string>();
        }

        protected virtual async void btnGetRawData_Click(object sender, EventArgs e)
        {
            Logger.Log("Start getting raw data");
            var startTime = startTimePicker.Value;
            var endTime = endTimePicker.Value;
            ToggleButton(false);
            try
            {
                RawData = Service.GetRawDataList(startTime, endTime).ToList();
                DisplayRawData();
                Logger.Log("Finished get raw data");
            }
            catch (Exception ex)
            {
                Logger.Log($"Exception when get raw data - {ex.Message}\n{ex.StackTrace}");
            }
            ToggleButton(true);

        }

        private void DisplayRawData()
        {
            if (richTextRawData.InvokeRequired)
            {
                richTextRawData.Invoke(new MethodInvoker(() => DisplayRawData()));
                return;
            }
            richTextRawData.Clear();
            foreach (var data in RawData)
            {
                richTextRawData.AppendText($"{data}\n");
            }
        }

        private void ToggleButton(bool toggle)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => ToggleButton(toggle)));
                return;
            }

            btnGetRawData.Enabled = toggle;
            btnCovert.Enabled = toggle;
            btnImport.Enabled = toggle;
        }

    }
    public class Logger
    {
        private readonly TextBoxBase _txtBox;
        public Logger(TextBoxBase txtBox)
        {
            _txtBox = txtBox;
        }

        public void Log(string msg)
        {
            Log(msg, Color.Black);
        }

        public void Log(string msg, Color color)
        {
            _txtBox.Invoke(new MethodInvoker(() => _txtBox.AppendText($"{msg}\n")));
        }

        public void Clear()
        {
            _txtBox.Invoke(new MethodInvoker(() => _txtBox.Clear()));
        }
    }
}
