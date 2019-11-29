using Fanex.Affiliate.DataService.Importer;
using System;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class TopazForm : ImporterFormBase
    {
        public TopazForm()
        {
            InitializeComponent();
            Service = new TopazService();
        }

        protected override TimeSpan _serverTimeOffset => new TimeSpan(0, 0, 0);

    }
}
