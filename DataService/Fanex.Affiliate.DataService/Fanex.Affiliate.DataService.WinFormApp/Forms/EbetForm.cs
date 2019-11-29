using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Ebet.DTO;
using Fanex.Affiliate.DataService.Importer.Ebet.MetaModels;
using Fanex.Affiliate.DataService.Importer.Ebet.Utilities;

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
                await Task.Run(() =>
                {
                    _logBuilder.AppendLine("Start:" + DateTime.Now);

                    var resultList = new List<EbetApiResult>();
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

                        resultList.AddRange(_ebetService.GetResultDataList(param));
                    }

                    foreach (var resultItem in resultList)
                    {
                        var apiParam = resultItem.Param;
                        _rawDataBuilder.AppendLine($"Currency: {apiParam.CurrencyId}, ChanelId: {apiParam.ChannelId}, ApiUrl: {apiParam.ApiUrl}");
                        _rawDataBuilder.AppendLine($"UserName: {apiParam.UserName}, StartTime: {apiParam.StartTime}, EndTime: {apiParam.EndTime}, PageSize: {apiParam.PageSize}, PageNumber: {apiParam.PageNumber}");
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

                    _logBuilder.AppendLine("End:" + DateTime.Now.ToString());
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

            _logBuilder.Clear();
            _rawDataBuilder.Clear();
            _convertedXmlBuilder.Clear();
        }

        private async void btnImportConvertedXml_Click(object sender, EventArgs e)
        {
            HandleBeforeImporting();
            try
            {
                await Task.Run(() =>
                {
                    foreach (var xml in _convertedXmlList)
                    {
                        _ebetService.Import(xml);
                    }
                });

                synchronizationContext.Post(new SendOrPostCallback((state) =>
                {
                    richTextLog.Text = "Import successfully!";
                }), null);
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
                    HandleAfterImporting();
                }), null);
            }
        }

        private void HandleBeforeImporting()
        {
            ToggleButton(false);
            richTextLog.Text = "Importing...";
        }

        private void HandleAfterImporting()
        {
            ToggleButton(true);
            _convertedXmlList.Clear();
            richTextLog.Text = "Import successfully!";
        }

        private void ToggleButton(bool isEnable)
        {
            btnGetRawData.Enabled = isEnable;
            btnImportConvertedXml.Enabled = isEnable;
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
                    ResetResult();
                    richTextLog.Text = "Importing data...";

                    try
                    {
                        ToggleButton(false);

                        var configList = _ebetConfigHelper.GetConfigList();
                        await Task.Run(() =>
                        {
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

                                _ebetService.Import(param);
                                _logBuilder.AppendLine($"Done - Currency: {param.CurrencyId} - Start:{param.StartTime} - End:{param.EndTime}");
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
