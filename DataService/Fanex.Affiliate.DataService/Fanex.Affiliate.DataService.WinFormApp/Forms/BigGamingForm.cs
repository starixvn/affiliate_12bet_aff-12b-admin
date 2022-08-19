using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using System;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class BigGamingForm : ImporterFormBase
    {
        public BigGamingForm(IImporterService service)
        {
            InitializeComponent();
            Service = service;
        }

        //protected override TimeSpan _pullRange => new TimeSpan(1, 0, 0, 0);
        protected override TimeSpan _serverTimeOffset => new TimeSpan(0, 0, 0);

        protected override void AppendSpilter()
        {
            richTextRawData.AppendText("-----------------------------------------\n");
        }
    }
}
