using Fanex.Affiliate.DataService.Importer;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class IMEsportForm : ImporterFormBase
    {
        public IMEsportForm()
        {
            InitializeComponent();
            Service = new VrLotteryService();
        }

    }
}
