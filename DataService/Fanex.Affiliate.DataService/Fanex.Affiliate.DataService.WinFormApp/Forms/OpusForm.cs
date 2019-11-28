using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Opus.MetaModels;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class OpusForm : Form
    {
        private const int MAX_TIME_RANGE = 15;
        private const int TIME_BETWEEN_TWO_TRANSACTION = 16000;
        private readonly IOpusService _opusService;
        private readonly StringBuilder _logBuilder;


        public OpusForm()
        {
            InitializeComponent();
            _opusService = new OpusService();
            _logBuilder = new StringBuilder();
        }

        private async void btnGetRawData_Click(object sender, EventArgs e)
        {
            var rawDataBuilder = new StringBuilder();
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
                this.Invoke(new MethodInvoker(() =>
                {
                    DisplayResult();
                    richTextRawData.Text = rawDataBuilder.ToString();
                    ToggleButton(true);
                }));
            }
        }

        private void ResetResult()
        {
            _logBuilder.Clear();
            richTextLog.Clear();
            richTextRawData.Clear();
        }

        private void DisplayResult()
        {
            richTextLog.Text = _logBuilder.ToString();
            _logBuilder.Clear();
        }

        private void ToggleButton(bool isEnable)
        {
            btnGetRawData.Enabled = isEnable;
            btnImportDirectly.Enabled = isEnable;
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
                    ResetResult();
                    richTextLog.Text = "Importing data...";

                    try
                    {
                        ToggleButton(false);

                        await Task.Run(async () =>
                        {
                            for (var start = startTimePicker.Value; DateTime.Compare(start, endTimePicker.Value) < 0; start = start.AddMinutes(MAX_TIME_RANGE))
                            {
                                var opusParam = new OpusParam
                                {
                                    StartTime = start,
                                    EndTime = start.AddMinutes(15)
                                };
                                _opusService.Import(opusParam);

                                _logBuilder.AppendLine($"Done - Start:{opusParam.StartTime} - End:{opusParam.EndTime}");
                                richTextLog.Invoke(new MethodInvoker(() => richTextLog.Text = _logBuilder.ToString()));
                                await Task.Delay(TIME_BETWEEN_TWO_TRANSACTION);
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
                        this.Invoke(new MethodInvoker(() =>
                        {
                            _logBuilder.AppendLine("Done!");
                            richTextLog.Text = _logBuilder.ToString();
                            _logBuilder.Clear();
                            ToggleButton(true);
                        }));
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