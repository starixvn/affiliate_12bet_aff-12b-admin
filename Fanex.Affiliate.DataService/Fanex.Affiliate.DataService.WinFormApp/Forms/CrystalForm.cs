using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.MetaModels.Crystal;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class CrystalForm : Form
    {
        private readonly ICrystalService _crystalService;
        private readonly StringBuilder _logBuilder;

        private readonly SynchronizationContext synchronizationContext;

        private readonly IEnumerable<string> _convertedDataList;

        public CrystalForm()
        {
            InitializeComponent();
            _crystalService = new CrystalService();
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
            var param = new CrystalParam
            {
                StartTime = startDatePicker.Value,
                EndTime = endDatePicker.Value,
                PageNumber = Convert.ToInt32(txtPageNumber.Text),
                PageSize = Convert.ToInt32(txtPageSize.Text)
            };
            try
            {
                await Task.Run(() =>
                {
                    var rawDataList = _crystalService.GetRawDataList(param);

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
                    richTextConvertedData.Text = convertedDataBuilder.ToString();
                }), null);
            }
        }

        private void ResetResult()
        {
            _logBuilder.Clear();
            richTextLog.Clear();
            richTextRawData.Clear();
            richTextConvertedData.Clear();
        }

        private void DisplayResult()
        {
            richTextLog.Text = _logBuilder.ToString();
            _logBuilder.Clear();
        }
    }
}
