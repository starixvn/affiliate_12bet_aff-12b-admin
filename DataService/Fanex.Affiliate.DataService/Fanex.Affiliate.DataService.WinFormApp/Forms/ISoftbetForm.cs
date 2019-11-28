using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.ISoftbet.MetaModels;
using Fanex.Affiliate.DataService.Importer.ISoftbet.Utilities;
using Newtonsoft.Json.Linq;


namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class ISoftbetForm : Form
    {
        private readonly IISoftbetService _isoftbetService;
        private readonly StringBuilder _logBuilder;

        private readonly SynchronizationContext synchronizationContext;

        private readonly List<string> _convertedDataList;

        public ISoftbetForm()
        {
            InitializeComponent();
            _isoftbetService = new ISoftbetService();
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

            var param = new ISoftbetEntryParam
            {
                StartTime = startTimePicker.Value,
                EndTime = endTimePicker.Value,
                PageNumber = Convert.ToInt32(txtPageNumber.Text)
            };

            try
            {
                ToggleButton(false);

                await Task.Run(() =>
                {
                    var rawDataList = _isoftbetService.GetRawDataList(param);
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
                                throw new ThirdPartyProductException($"Error: ISoftbet - ProductId: {Products.ISoftbetProductId}\n{item.JsonData}");
                            }

                            var summary = ISoftbetParser.FromJsonObject(jObject);
                            var xml = _isoftbetService.ConvertToXml(summary, item.Param);

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
                });
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
                    ToggleButton(true);
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

        private void ToggleButton(bool isEnable)
        {
            btnGetRawData.Enabled = isEnable;
            btnImport.Enabled = isEnable;
        }

        private async void btnImport_Click(object sender, EventArgs e)
        {
            const byte JobType = 0;
            ToggleButton(false);
            try
            {
                await Task.Run(() =>
                {
                    foreach (var xml in _convertedDataList)
                    {
                        _isoftbetService.Import(xml, JobType);
                    }

                    _convertedDataList.Clear();
                    _logBuilder.AppendLine("Done!");
                });
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
                    ToggleButton(true);
                }), null);
            }
        }

        private async void btnImportDirectly_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(startTimePicker.Value, endTimePicker.Value) < 0)
            {
                var confirmResult = MessageBox.Show("Are you sure to Import Directly ??",
                                     "Confirm Import!!",
                                     MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    const int TimeRangeByMinute = 1;
                    ResetResult();
                    richTextLog.Text = "Importing data...";

                    var start = startTimePicker.Value;
                    var end = endTimePicker.Value;
                    var param = new ISoftbetEntryParam
                    {
                        StartTime = new DateTime(start.Year, start.Month, start.Day, start.Hour, start.Minute, 0),
                        EndTime = new DateTime(end.Year, end.Month, end.Day, end.Hour, end.Minute, 0),
                        PageNumber = Convert.ToInt32(txtPageNumber.Text)
                    };
                    try
                    {
                        ToggleButton(false);

                        await Task.Run(() =>
                        {
                            for (var requestTime = param.StartTime; DateTime.Compare(requestTime, param.EndTime) < 0; requestTime = requestTime.AddMinutes(TimeRangeByMinute))
                            {
                                var paramByBatch = new ISoftbetEntryParam
                                {
                                    StartTime = requestTime,
                                    EndTime = requestTime.AddMinutes(TimeRangeByMinute),
                                    PageNumber = Convert.ToInt32(txtPageNumber.Text)
                                };

                                _isoftbetService.Import(paramByBatch);

                                _logBuilder.AppendLine($"Done - Start:{paramByBatch.StartTime.ToString()} - End:{paramByBatch.EndTime.ToString()}");
                                synchronizationContext.Post(new SendOrPostCallback((state) =>
                                {
                                    richTextLog.Text = _logBuilder.ToString();
                                }), null);
                            }
                        });
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
                            _logBuilder.AppendLine("Done!");
                            richTextLog.Text = _logBuilder.ToString();
                            _logBuilder.Clear();
                            ToggleButton(true);
                        }), null);
                    }
                }
            }
            else
            {
                richTextLog.Text = "Incorrect time range!";
            }
        }
    }
}