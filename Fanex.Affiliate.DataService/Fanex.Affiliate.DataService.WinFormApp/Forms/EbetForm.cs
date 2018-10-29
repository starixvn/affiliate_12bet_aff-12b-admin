using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.MetaModels.Ebet;
using Fanex.Affiliate.DataService.Importer.Utilities;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class EbetForm : Form
    {
        private readonly IEbetService _ebetService;
        private IEbetConfigHelper _ebetConfigHelper;
        private StringBuilder _logBuilder;

        private readonly SynchronizationContext synchronizationContext;

        public EbetForm()
        {
            InitializeComponent();

            _logBuilder = new StringBuilder();
            _ebetConfigHelper = new EbetConfigHelper();
            _ebetService = new EbetService();
            synchronizationContext = SynchronizationContext.Current;
        }

        private async void btnGetRawData_Click(object sender, EventArgs e)
        {
            ResetResult();
            richTextResult.Text = "Getting data...";

            var configList = _ebetConfigHelper.GetConfigList();
            var taskList = new List<Task>();
            _logBuilder.AppendLine(DateTime.Now.ToString());
            foreach (var config in configList)
            {
                taskList.Add(Task.Run(() =>
                {
                    var param = new EbetParam
                    {
                        UserName = txtUserName.Text,
                        StartTime = startDatePicker.Value,
                        EndTime = endDatePicker.Value
                    };

                    var jsonData = _ebetService.GetJsonData(config, param);
                    _logBuilder.AppendLine($"Currency: {config.CurrencyID}, ChanelId: {config.ChannelId}, ApiUrl: {config.ApiServerHost}");
                    _logBuilder.AppendLine($"UserName: {param.UserName}, StartTime: {param.StartTime}, EndTime: {param.EndTime}");
                    _logBuilder.AppendLine(jsonData);
                    _logBuilder.AppendLine("------------------------------------------------------");
                    _logBuilder.AppendLine();
                }));
            }

            await Task.WhenAll(taskList);
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
