using Fanex.Affiliate.DataService.Importer.BTi;
using System;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class BTiForm : ImporterFormBase
    {
        public BTiForm()
        {
            InitializeComponent();
            Service = new BTiService();
        }

        protected override TimeSpan _serverTimeOffset => new TimeSpan(0, 0, 0);
    }
}
