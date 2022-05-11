using Fanex.Affiliate.DataService.Importer.Sunstone;
using System;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class SunstoneForm : ImporterFormBase
    {
        public SunstoneForm()
        {
            InitializeComponent();
            Service = new SunstoneService();
        }

        //protected override TimeSpan _pullRange => new TimeSpan(1, 0, 0, 0);
        protected override TimeSpan _serverTimeOffset => new TimeSpan(0, 0, 0);

        protected override void AppendSpilter()
        {
            richTextRawData.AppendText("-----------------------------------------\n");
        }
    }
}
