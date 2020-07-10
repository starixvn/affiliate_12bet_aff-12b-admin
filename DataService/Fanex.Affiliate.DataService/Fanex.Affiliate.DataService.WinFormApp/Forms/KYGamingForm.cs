using Fanex.Affiliate.DataService.Importer.KYGaming;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class KYGamingForm : ImporterFormBase
    {
        public KYGamingForm()
        {
            InitializeComponent();
            Service = new KYGamingService();
        }

    }
}
