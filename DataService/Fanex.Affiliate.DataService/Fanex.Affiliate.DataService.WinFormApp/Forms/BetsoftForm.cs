using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Betsoft.MetaModels;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class BetsoftForm : Form
    {
        private readonly IBetsoftService _betsoftService;
        private readonly IAppSettings _appSetting;
        private readonly StringBuilder _logBuilder;

        private readonly SynchronizationContext synchronizationContext;

        public BetsoftForm()
        {
            InitializeComponent();

            _logBuilder = new StringBuilder();
            _appSetting = new AppSettings();
            _betsoftService = new BetsoftService();
            synchronizationContext = SynchronizationContext.Current;
        }

        private async void btnGetRawData_Click(object sender, EventArgs e)
        {
            var rawDataBuilder = new StringBuilder();
            try
            {
                ResetResult();
                richTextLog.Text = "Getting data...";
                var betSoftParam = new BetsoftParam
                {
                    StartDate = startDatePicker.Value,
                    EndDate = endDatePicker.Value
                };

                var rawDataList = await _betsoftService.GetMultipleRawDataBatchAsync(betSoftParam);
                if (rawDataList.Any())
                {
                    foreach (var item in rawDataList)
                    {
                        rawDataBuilder.AppendLine(item);
                        rawDataBuilder.AppendLine();
                    }
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
                }), null);
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
            if (DateTime.Compare(startDatePicker.Value, endDatePicker.Value) < 0)
            {
                var confirmResult = MessageBox.Show("Are you sure to Import Directly ??",
                                     "Confirm Import!!",
                                     MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    const int TimeRangeByHour = 1;
                    ResetResult();
                    richTextLog.Text = "Importing data...";

                    try
                    {
                        ToggleButton(false);

                        await Task.Run(() =>
                        {
                            for (var start = startDatePicker.Value; DateTime.Compare(start, endDatePicker.Value) < 0; start = start.AddHours(TimeRangeByHour))
                            {
                                var param = new BetsoftParam
                                {
                                    StartDate = start,
                                    EndDate = start.AddHours(TimeRangeByHour)
                                };

                                _betsoftService.Import(param);

                                _logBuilder.AppendLine($"Done - Start:{param.StartDate.ToString()} - End:{param.EndDate.ToString()}");
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