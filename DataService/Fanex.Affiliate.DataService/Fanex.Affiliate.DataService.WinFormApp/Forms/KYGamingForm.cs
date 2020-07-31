using Fanex.Affiliate.DataService.Importer.KYGaming;
using System;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class KYGamingForm : ImporterFormBase
    {
        protected override TimeSpan _pullRange => new TimeSpan(0, 10, 0);

        public KYGamingForm()
        {
            InitializeComponent();
            Service = new KYGamingService();
        }

    }
}
