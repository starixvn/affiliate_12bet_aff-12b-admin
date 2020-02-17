using Fanex.Affiliate.DataService.Importer;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class GpiLotteryForm : ImporterFormBase
    {
        public GpiLotteryForm()
        {
            InitializeComponent();
            Service = new GpiLotteryService();
        }
    }
}
