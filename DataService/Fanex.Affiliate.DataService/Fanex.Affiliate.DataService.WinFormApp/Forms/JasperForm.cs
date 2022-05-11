using Fanex.Affiliate.DataService.Importer.Jasper;
using System;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class JasperForm : ImporterFormBase
    {
        public JasperForm()
        {
            InitializeComponent();
            Service = new JasperService();
        }

        //protected override TimeSpan _pullRange => new TimeSpan(1, 0, 0, 0);
        protected override TimeSpan _serverTimeOffset => new TimeSpan(0, 0, 0);

        protected override void AppendSpilter()
        {
            richTextRawData.AppendText("-----------------------------------------\n");
        }
    }
}
