using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using System;
using System.Net.Http;
using System.Threading;
using System.Windows.Forms;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class PingUrlForm : Form
    {
        protected string Data { get; set; }

        public PingUrlForm()
        {
            InitializeComponent();
            radioBtnMethodGet.Checked = true;
        }

        private void DisplayRawData()
        {
            DisplayRawData(Data);
        }

        private void DisplayRawData(string data)
        {
            if (richTbxPingResult.InvokeRequired)
            {
                Data = data;
                richTbxPingResult.Invoke(new MethodInvoker(() => DisplayRawData()));
                return;
            }
            richTbxPingResult.Clear();
            richTbxPingResult.AppendText($"{data}\n");
        }

        private async void btnPing_Click(object sender, EventArgs e)
        {
            DisplayRawData("Please waiting!");
            var pingUrl = txtUrlPath.Text;
            var isGetMethod = radioBtnMethodGet.Checked;

            HttpClient httpClient = HttpClientPooling.GetHttpClient(pingUrl);
            var request = new HttpRequestMessage(isGetMethod ? HttpMethod.Get : HttpMethod.Post, pingUrl);

            try
            {
                using (var response = await httpClient.SendAsync(request, default(CancellationToken)).ConfigureAwait(false))
                {
                    DisplayRawData("Ping successfully - " + response.StatusCode.ToString());
                }
            } catch(Exception ex)
            {
                DisplayRawData("Ping failed - " + ex.Message);
            }
        }
    }
}
