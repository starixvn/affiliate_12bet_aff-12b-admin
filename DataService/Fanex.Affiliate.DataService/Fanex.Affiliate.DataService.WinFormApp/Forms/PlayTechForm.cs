using Fanex.Affiliate.DataService.Importer.Playtech;
using System;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class PlaytechForm : ImporterFormBase
    {
        public PlaytechForm()
        {
            InitializeComponent();
            Service = new PlaytechService();
        }

        protected override TimeSpan _serverTimeOffset => new TimeSpan(0, 0, 0);

        protected override void AppendSpilter()
        {
            richTextRawData.AppendText("-----------------------------------------\n");
        }
    }
}
