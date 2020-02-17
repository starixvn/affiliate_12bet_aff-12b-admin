using System;
using Fanex.Affiliate.DataService.Importer;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class LBKenoForm : ImporterFormBase
    {
        protected override TimeSpan _pullRange => new TimeSpan(1, 0, 0, 0);
        public LBKenoForm()
        {
            InitializeComponent();
            Service = new LBKenoService();
        }

    }
}
