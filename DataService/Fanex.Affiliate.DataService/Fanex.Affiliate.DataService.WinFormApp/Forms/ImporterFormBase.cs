using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using System;
using System.Collections.Generic;
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
        protected List<string> RawDatas { get; set; }
        protected List<XDocument> XmlDatas { get; set; } = new List<XDocument>();

        protected TimeSpan _localTimeOffset => TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
        protected virtual TimeSpan _serverTimeOffset => new TimeSpan(8, 0, 0);
        protected virtual TimeSpan _pullRange => new TimeSpan(0, 1, 0, 0);

        public ImporterFormBase()
        {
            InitializeComponent();
            this.Logger = new Logger(this.richTextLog);
            RawDatas = new List<string>();
            lbLocalTime.Text = _localTimeOffset.Hours >= 0 ? $"Local Time Zone: GMT+{_localTimeOffset.Hours}" : $"Local Time Zone: GMT{_localTimeOffset.Hours}";
            lbServiceTime.Text =_serverTimeOffset.Hours >= 0 ? $"Server Time Zone: GMT+{_serverTimeOffset.Hours}" : $"Server Time Zone: GMT{_serverTimeOffset.Hours}";
            startTimePicker.Value = DateTimeOffset.Now.ToOffset(_serverTimeOffset).DateTime;
            endTimePicker.Value = DateTimeOffset.Now.ToOffset(_serverTimeOffset).DateTime;
        }

        protected virtual async void btnGetRawData_Click(object sender, EventArgs e)
        {
            Logger.Log("Start getting raw data");
            RawDatas.Clear();
            var startTime = startTimePicker.Value;
            var endTime = endTimePicker.Value;
            ToggleButton(false);
            try
            {
                var timeRanges = DateTimeHelper.GetProportionTimeRange(startTime, endTime, _pullRange);
                foreach (var range in timeRanges)
                {
                    Logger.Log($"Get raw data from {range.Item1} to {range.Item2}");
                    RawDatas.AddRange(await Service.GetRawDataListAsync(range.Item1, range.Item2));
                    DisplayRawData();
                }
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
                    var xml = await GetXmlFromTextFieldAsync();
                    XmlDatas.Add(xml);
                }
                else
                {
                    foreach (var data in RawDatas)
                    {
                        var xml = await Task.Run(async () => await Service.ConvertRawDataToXmlAsync(data));
                        XmlDatas.Add(xml);
                    }
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
            var confirmResult = MessageBox.Show("Are you sure to Import from converted data ??",
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

        private async void btnImportDirectly_Click(object sender, EventArgs e)
        {
            Logger.Log("Start import data");
            RawDatas.Clear();
            var startTime = startTimePicker.Value;
            var endTime = endTimePicker.Value;
            ToggleButton(false);
            try
            {
                var timeRanges = DateTimeHelper.GetProportionTimeRange(startTime, endTime, _pullRange);
                foreach (var range in timeRanges)
                {
                    Logger.Log($"Import from {range.Item1} to {range.Item2}");
                    var rawDatas =  await Service.GetRawDataListAsync(range.Item1, range.Item2);
                    DisplayRawData(rawDatas);
                    var xmlDatas = await ConvertToXmlAsync(rawDatas);
                    DisplayXmlData(xmlDatas);
                    await Service.ImportAsync(xmlDatas.Select(xml => xml.ToString()));
                    
                }
                DisplayXmlData();
                Logger.Log("Finished import");
            }
            catch (Exception ex)
            {
                Logger.Log($"Exception when get raw data - {ex.Message}\n{ex.StackTrace}");
            }
            ToggleButton(true);
        }

        private async Task<IEnumerable<XDocument>> ConvertToXmlAsync(IEnumerable<string> rawDatas)
        {
            var xmlDatas = new List<XDocument>();
            foreach (var data in rawDatas)
            {
                xmlDatas.Add(await Service.ConvertRawDataToXmlAsync(data));
            }
            return xmlDatas;
        }

        private void datePickerLocalStartTime_ValueChanged(object sender, EventArgs e)
        {
            var timeOffSet = new DateTimeOffset(datePickerLocalStartTime.Value);
            startTimePicker.Value = timeOffSet.ToOffset(_serverTimeOffset).DateTime;
        }

        private void datePickerLocalEndTime_ValueChanged(object sender, EventArgs e)
        {
            var timeOffSet = new DateTimeOffset(datePickerLocalEndTime.Value);
            endTimePicker.Value = timeOffSet.ToOffset(_serverTimeOffset).DateTime;
        }

        private void DisplayXmlData()
        {
            DisplayXmlData(XmlDatas);
        }

        private void DisplayXmlData(IEnumerable<XDocument> xmls)
        {
            if (richTextConvertedData.InvokeRequired)
            {
                richTextConvertedData.Invoke(new MethodInvoker(() => DisplayXmlData()));
                return;
            }
            richTextConvertedData.Clear();
            foreach (var xml in xmls)
            {
                richTextConvertedData.AppendText($"{xml.ToString()}\n");
            }
        }

        private void DisplayRawData()
        {
            DisplayRawData(RawDatas);
        }

        private void DisplayRawData(IEnumerable<string> datas)
        {

            if (richTextRawData.InvokeRequired)
            {
                richTextRawData.Invoke(new MethodInvoker(() => DisplayRawData()));
                return;
            }
            richTextRawData.Clear();
            foreach (var data in datas)
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
            btnImportDirectly.Enabled = toggle;
        }

        private void ImporterFormBase_FormClosing(object sender, FormClosingEventArgs e)
        {
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
            if (_txtBox.IsDisposed)
                return;
            _txtBox.Invoke(new MethodInvoker(() => _txtBox.AppendText($"{msg}\n")));
        }

        public void Clear()
        {
            _txtBox.Invoke(new MethodInvoker(() => _txtBox.Clear()));
        }
    }
}
