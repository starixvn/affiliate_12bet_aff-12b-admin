using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Constants;
using Fanex.Affiliate.DataService.Importer.Exceptions;
using Fanex.Affiliate.DataService.Importer.MetaModels.Quartz;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Newtonsoft.Json.Linq;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class QuartzForm : Form
    {
        private readonly IQuartzService _quartzService;
        private readonly StringBuilder _logBuilder;

        private readonly SynchronizationContext synchronizationContext;

        private readonly List<string> _convertedDataList;

        public QuartzForm()
        {
            InitializeComponent();
            _quartzService = new QuartzService();
            synchronizationContext = SynchronizationContext.Current;
            _convertedDataList = new List<string>();
            _logBuilder = new StringBuilder();
        }

        private async void btnGetRawData_Click(object sender, EventArgs e)
        {
            var rawDataBuilder = new StringBuilder();
            var convertedDataBuilder = new StringBuilder();
            ResetResult();
            richTextLog.Text = "Getting data...";

            var param = new QuartzParam
            {
                RequestTime = datePicker.Value,
                PageNumber = Convert.ToInt32(txtPageNumber.Text)
            };

            try
            {
                btnGetRawData.Enabled = false;
                btnImport.Enabled = false;

                var rawDataList = await _quartzService.GetRawDataList(param);
                if (rawDataList.Any())
                {
                    foreach (var item in rawDataList)
                    {
                        rawDataBuilder.AppendLine(item.JsonData);
                        rawDataBuilder.AppendLine();
                        rawDataBuilder.AppendLine("-----------------------------------------");

                        var jObject = JObject.Parse(item.JsonData);
                        var status = Convert.ToString(jObject["status"]);
                        if (status != "success")
                        {
                            throw new ThirdPartyProductException($"Error: Quartz - ProductId: {Products.QuartzProductId}\n{item.JsonData}");
                        }

                        var summary = QuartzParser.FromJsonObject(jObject);
                        var xml = _quartzService.ConvertToXml(summary, item.Param);

                        convertedDataBuilder.AppendLine(xml);
                        convertedDataBuilder.AppendLine();
                        convertedDataBuilder.AppendLine("-----------------------------------------");

                        _convertedDataList.Add(xml);
                    }
                    _logBuilder.AppendLine("Done!");
                }
                else
                {
                    _logBuilder.AppendLine("No information");
                }
            }
            catch (Exception ex)
            {
                _logBuilder.AppendLine(ex.Message);
                _logBuilder.AppendLine(ex.StackTrace);
                _logBuilder.AppendLine(ex.Source);
            }
            finally
            {
                synchronizationContext.Post(new SendOrPostCallback((state) =>
                {
                    DisplayResult();
                    richTextRawData.Text = rawDataBuilder.ToString();
                    richTextConvertedData.Text = convertedDataBuilder.ToString();
                    btnGetRawData.Enabled = true;
                    btnImport.Enabled = true;
                }), null);
            }
        }

        private void ResetResult()
        {
            _logBuilder.Clear();
            richTextLog.Clear();
            richTextRawData.Clear();
            richTextConvertedData.Clear();

            _convertedDataList.Clear();
        }

        private void DisplayResult()
        {
            richTextLog.Text = _logBuilder.ToString();
            _logBuilder.Clear();
        }
    }
}
