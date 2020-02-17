using Fanex.Affiliate.DataService.Importer;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class VrLotteryForm : ImporterFormBase
    {
        public VrLotteryForm() 
        {
            InitializeComponent();
            Service = new VrLotteryService();
        }

    }
}
