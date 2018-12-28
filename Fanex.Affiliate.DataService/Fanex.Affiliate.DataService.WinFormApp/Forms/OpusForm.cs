using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.MetaModels.Opus;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class OpusForm : Form
    {
        private readonly IOpusService _opusService;
        private readonly StringBuilder _logBuilder;

        private readonly SynchronizationContext synchronizationContext;

        private readonly List<string> _convertedDataList;

        public OpusForm()
        {
            InitializeComponent();
            _opusService = new OpusService();
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

            var param = new OpusParam
            {
                StartTime = startTimePicker.Value,
                EndTime = endTimePicker.Value
            };

            try
            {
                ToggleButton(false);

                await Task.Run(() =>
                {
                    var rawDataList = _opusService.GetRawDataList(param);
                    if (rawDataList.Any())
                    {
                        foreach (var item in rawDataList)
                        {
                            rawDataBuilder.AppendLine(item);
                            rawDataBuilder.AppendLine();
                            rawDataBuilder.AppendLine("-----------------------------------------");
                        }

                        var convertedData = _opusService.ConvertDataTimeZone(rawDataList);
                        foreach (var item in convertedData)
                        {
                            convertedDataBuilder.AppendLine(item);
                            convertedDataBuilder.AppendLine();
                            convertedDataBuilder.AppendLine("-----------------------------------------");

                            _convertedDataList.Add(item);
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
            ToggleButton(false);
            try
            {
                await Task.Run(() =>
                {
                    foreach (var xml in _convertedDataList)
                    {
                        _opusService.Import(xml);
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
    }
}