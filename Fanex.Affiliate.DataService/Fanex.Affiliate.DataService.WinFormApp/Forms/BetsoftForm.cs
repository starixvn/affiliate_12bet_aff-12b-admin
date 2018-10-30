using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.MetaModels.Betsoft;
using Fanex.Affiliate.DataService.Importer.Utilities;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class BetsoftForm : Form
    {
        private readonly IBetsoftService _betsoftService;
        private IAppSettings _appSetting;
        private StringBuilder _logBuilder;

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
            ResetResult();
            richTextResult.Text = "Getting data...";
            _logBuilder.AppendLine(DateTime.Now.ToString());

            var betSoftParam = new BetsoftParam
            {
                StartDate = startDatePicker.Value,
                EndDate = endDatePicker.Value,
                ApiUrl = _appSetting.BetsoftApiUrl,
                BankId = _appSetting.BetsoftBankId,
                Password = _appSetting.BetsoftPassword
            };

            var rawDataList = await _betsoftService.GetMultipleRawDataBatchAsync(betSoftParam);

            if (rawDataList.Any())
            {
                var batchCount = 1;
                foreach (var item in rawDataList)
                {
                    _logBuilder.AppendLine($"---Begin Batch {batchCount}---");
                    _logBuilder.AppendLine(item);
                    _logBuilder.AppendLine($"---End Batch {batchCount}---");
                    _logBuilder.AppendLine();

                    batchCount++;
                }
            }
            else
            {
                _logBuilder.AppendLine("No information");
            }

            _logBuilder.AppendLine(DateTime.Now.ToString());

            synchronizationContext.Post(new SendOrPostCallback((state) =>
            {
                DisplayResult();
            }), null);
        }

        private void ResetResult()
        {
            _logBuilder.Clear();
            richTextResult.Clear();
        }

        private void DisplayResult()
        {
            richTextResult.Text = _logBuilder.ToString();
        }
    }
}