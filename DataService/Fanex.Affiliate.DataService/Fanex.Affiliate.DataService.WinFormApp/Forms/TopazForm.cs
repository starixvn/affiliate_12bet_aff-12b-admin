using Fanex.Affiliate.DataService.Importer;
using System;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class TopazForm : ImporterFormBase
    {
        private const string CALL_LIMIT_EXCEEDED = "CALL_LIMIT_EXCEEDED";
        //60s to request next data for same brand_id request
        private const int TIME_BETWEEN_TWO_REQUEST = 60000;
        private static bool _isSetUpPolicy = false;

        public TopazForm()
        {
            InitializeComponent();
            Service = new TopazService();
        }

        protected override TimeSpan _serverTimeOffset => new TimeSpan(0, 0, 0);
        protected override TimeSpan _pullRange => new TimeSpan(0, 15, 0);
    }
}
