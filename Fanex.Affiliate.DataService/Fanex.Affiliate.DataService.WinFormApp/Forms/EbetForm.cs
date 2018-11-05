using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.DTO.Ebet;
using Fanex.Affiliate.DataService.Importer.MetaModels.Ebet;
using Fanex.Affiliate.DataService.Importer.Utilities;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class EbetForm : Form
    {
        private readonly IEbetService _ebetService;
        private readonly IEbetConfigHelper _ebetConfigHelper;
        private readonly StringBuilder _logBuilder;
        private readonly StringBuilder _rawDataBuilder;
        private readonly StringBuilder _convertedXmlBuilder;
        private readonly IList<string> _convertedXmlList;

        private readonly SynchronizationContext synchronizationContext;

        public EbetForm()
        {
            InitializeComponent();

            _logBuilder = new StringBuilder();
            _rawDataBuilder = new StringBuilder();
            _convertedXmlBuilder = new StringBuilder();
            _ebetConfigHelper = new EbetConfigHelper();
            _ebetService = new EbetService();
            synchronizationContext = SynchronizationContext.Current;
            _convertedXmlList = new List<string>();
        }

        private async void btnGetRawData_Click(object sender, EventArgs e)
        {
            ResetResult();
            richTextLog.Text = "Getting data...";

            try
            {
                var configList = _ebetConfigHelper.GetConfigList();
                var taskList = new List<Task<IEnumerable<EbetApiResult>>>();
                _logBuilder.AppendLine(DateTime.Now.ToString());
                foreach (var config in configList)
                {
                    var param = new EbetParam
                    {
                        UserName = txtUserName.Text,
                        StartTime = startDatePicker.Value,
                        EndTime = endDatePicker.Value,
                        ChannelId = config.ChannelId,
                        ApiUrl = config.ApiServerHost,
                        PrivateKey = config.PrivateKey,
                        CurrencyId = config.CurrencyID,
                        PageNumber = Convert.ToInt32(txtPageNumber.Text),
                        PageSize = Convert.ToInt32(txtPageSize.Text),
                    };

                    taskList.Add(_ebetService.GetResultDataList(param));
                }

                var resultList = await Task.WhenAll(taskList);
                foreach (var result in resultList)
                {
                    foreach (var resultItem in result)
                    {
                        var apiParam = resultItem.Param;
                        _rawDataBuilder.AppendLine($"Currency: {apiParam.CurrencyId}, ChanelId: {apiParam.ChannelId}, ApiUrl: {apiParam.ApiUrl}");
                        _rawDataBuilder.AppendLine($"UserName: {apiParam.UserName}, StartTime: {apiParam.StartTime}, EndTime: {apiParam.EndTime}");
                        _rawDataBuilder.AppendLine(resultItem.JsonData);
                        _rawDataBuilder.AppendLine("------------------------------------------------------");
                        _rawDataBuilder.AppendLine();

                        var ebetSummary = EbetParser.FromJson(resultItem.JsonData);
                        var xml = _ebetService.ConvertToXml(ebetSummary, apiParam);
                        _convertedXmlList.Add(xml);

                        _convertedXmlBuilder.AppendLine(xml);
                        _convertedXmlBuilder.AppendLine("------------------------------------------------------");
                        _convertedXmlBuilder.AppendLine();
                    }
                }

                _logBuilder.AppendLine(DateTime.Now.ToString());
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
                }), null);
            }
        }

        private void ResetResult()
        {
            _logBuilder.Clear();
            _rawDataBuilder.Clear();
            _convertedXmlBuilder.Clear();
            richTextLog.Clear();
            richTextRawData.Clear();
            richTextConvertedXml.Clear();

            _convertedXmlList.Clear();
        }

        private void DisplayResult()
        {
            richTextLog.Text = _logBuilder.ToString();
            richTextRawData.Text = _rawDataBuilder.ToString();
            richTextConvertedXml.Text = _convertedXmlBuilder.ToString();
        }
    }
}
