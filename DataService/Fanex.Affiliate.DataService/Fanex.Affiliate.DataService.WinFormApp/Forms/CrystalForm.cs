using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Gpi.MetaModels;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class CrystalForm : Form
    {
        private readonly ICrystalGpiService _crystalGpiService;
        private readonly StringBuilder _logBuilder;

        private readonly SynchronizationContext synchronizationContext;

        public CrystalForm()
        {
            InitializeComponent();
            _crystalGpiService = new CrystalGpiService();
            synchronizationContext = SynchronizationContext.Current;
            _logBuilder = new StringBuilder();
        }

        private async void btnGetRawData_Click(object sender, EventArgs e)
        {
            var rawDataBuilder = new StringBuilder();

            ResetResult();
            richTextLog.Text = "Getting data...";
            var param = new CrystalGpiParam
            {
                StartTime = startDatePicker.Value,
                EndTime = endDatePicker.Value,
                PageNumber = Convert.ToInt32(txtPageNumber.Text),
                PageSize = Convert.ToInt32(txtPageSize.Text)
            };
            try
            {
                ToggleButton(false);
                await Task.Run(() =>
                {
                    var rawDataList = _crystalGpiService.GetRawDataList(param);

                    if (rawDataList.Any())
                    {
                        foreach (var item in rawDataList)
                        {
                            rawDataBuilder.AppendLine(item);
                            rawDataBuilder.AppendLine();
                            rawDataBuilder.AppendLine("-----------------------------------------");
                        }
                    }
                    else
                    {
                        _logBuilder.AppendLine("No information");
                    }
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
                    richTextRawData.Text = rawDataBuilder.ToString();
                    ToggleButton(true);
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
                        var param = new CrystalGpiParam
                        {
                            StartTime = startDatePicker.Value,
                            EndTime = endDatePicker.Value,
                            PageNumber = Convert.ToInt32(txtPageNumber.Text),
                            PageSize = Convert.ToInt32(txtPageSize.Text)
                        };

                        await Task.Run(() =>
                        {
                            for (var start = param.StartTime; DateTime.Compare(start, param.EndTime) < 0; start = start.AddHours(TimeRangeByHour))
                            {
                                var crystalGpiParam = new CrystalGpiParam
                                {
                                    StartTime = start,
                                    EndTime = start.AddHours(TimeRangeByHour),
                                    PageNumber = param.PageNumber,
                                    PageSize = param.PageSize
                                };
                                _crystalGpiService.Import(crystalGpiParam);
                                _logBuilder.AppendLine($"Done - Start:{crystalGpiParam.StartTime} - End:{crystalGpiParam.EndTime}");
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
