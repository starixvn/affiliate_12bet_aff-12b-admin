using Fanex.Affiliate.DataService.Importer.Skywind;
using System;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class SkywindForm : ImporterFormBase
    {
        public SkywindForm()
        {
            InitializeComponent();
            Service = new SkywindService();
        }

        protected override TimeSpan _serverTimeOffset => new TimeSpan(0, 0, 0);
    }
}
