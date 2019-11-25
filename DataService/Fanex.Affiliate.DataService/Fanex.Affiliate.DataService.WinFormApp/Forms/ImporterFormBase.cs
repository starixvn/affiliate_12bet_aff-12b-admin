using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class ImporterFormBase : Form
    {
        protected IImporterService Service;
        protected Logger Logger;
        protected IEnumerable<string> RawDatas { get; set; }
        protected List<XDocument> XmlDatas { get; set; } = new List<XDocument>();

        public ImporterFormBase()
        {
            InitializeComponent();
            this.Logger = new Logger(this.richTextLog);
            RawDatas = new List<string>();
        }

        protected virtual async void btnGetRawData_Click(object sender, EventArgs e)
        {
            Logger.Log("Start getting raw data");
            var startTime = startTimePicker.Value;
            var endTime = endTimePicker.Value;
            ToggleButton(false);
            try
            {
                RawDatas = await Service.GetRawDataListAsync(startTime, endTime);
                DisplayRawData();
                XmlDatas.Clear();
                DisplayXmlData();
                Logger.Log("Finished get raw data");
            }
            catch (Exception ex)
            {
                Logger.Log($"Exception when get raw data - {ex.Message}\n{ex.StackTrace}");
            }
            ToggleButton(true);

        }

        protected virtual async void btnCovert_Click(object sender, EventArgs e)
        {
            Logger.Log("Start convert raw data to xml");
            XmlDatas.Clear();
            ToggleButton(false);
            try
            {
                if (chkBoxConvertDirectFromTxtField.Checked)
                {
                    var xml =  await GetXmlFromTextFieldAsync();
                    XmlDatas.Add(xml);
                }
                else
                    foreach (var data in RawDatas)
                    {
                        var xml = await Task.Run(async () => await Service.ConvertRawDataToXmlAsync(data));
                        XmlDatas.Add(xml);
                    }
                DisplayXmlData();
                Logger.Log("Finished convert raw data");
            }
            catch (Exception ex)
            {
                Logger.Log($"Error when convert raw data - {ex.Message}\n {ex.StackTrace}");
            }

            ToggleButton(true);
        }

        private async Task<XDocument> GetXmlFromTextFieldAsync()
        {
            var data = richTextRawData.Text;
            return await Service.ConvertRawDataToXmlAsync(data);
        }

        protected virtual async void btnImport_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to Import Directly ??",
                     "Confirm Import!!",
                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                if (XmlDatas.Count == 0)
                {
                    MessageBox.Show("You need convert raw data to xml first.", "Warning", MessageBoxButtons.OK);
                    return;
                }
                Logger.Log("Start import data");
                ToggleButton(false);
                try
                {
                    await Service.ImportAsync(XmlDatas.Select(xml => xml.ToString()));
                    Logger.Log("Finished import data");
                }
                catch (Exception ex)
                {
                    Logger.Log($"Error when import data {ex.Message}\n{ex.StackTrace}\n{ex.Source}");
                }
                ToggleButton(true);
            }
        }

        private void DisplayXmlData()
        {
            if (richTextConvertedData.InvokeRequired)
            {
                richTextConvertedData.Invoke(new MethodInvoker(() => DisplayXmlData()));
                return;
            }
            richTextConvertedData.Clear();
            foreach (var xml in XmlDatas)
            {
                richTextConvertedData.AppendText($"{xml.ToString()}\n");
            }
        }

        private void DisplayRawData()
        {
            if (richTextRawData.InvokeRequired)
            {
                richTextRawData.Invoke(new MethodInvoker(() => DisplayRawData()));
                return;
            }
            richTextRawData.Clear();
            foreach (var data in RawDatas)
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
