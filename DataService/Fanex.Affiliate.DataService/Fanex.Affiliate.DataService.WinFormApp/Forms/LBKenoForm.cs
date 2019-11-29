using Fanex.Affiliate.DataService.Importer;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class LBKenoForm : ImporterFormBase
    {
        public LBKenoForm()
        {
            InitializeComponent();
            Service = new LBKenoService();
        }
    }
}
