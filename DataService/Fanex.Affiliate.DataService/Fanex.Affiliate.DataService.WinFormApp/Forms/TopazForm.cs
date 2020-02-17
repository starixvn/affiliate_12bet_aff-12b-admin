using Fanex.Affiliate.DataService.Importer;
using Polly;
using System;
using System.Configuration;

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
            Init();
        }

        protected override TimeSpan _serverTimeOffset => new TimeSpan(0, 0, 0);
        protected override TimeSpan _pullRange => new TimeSpan(0, 15, 0);

        private static void Init()
        {
            if (!_isSetUpPolicy)
            {
                var url = ConfigurationManager.AppSettings.Get("TopazApiUrl");
                var policy = GetForeverRetryPolicy();
                TopazService.SetRequestPolicy(url, policy);
            }
            _isSetUpPolicy = true;
        }

        private static IAsyncPolicy<String> GetForeverRetryPolicy()
        {
            var retryPolicy = Policy.HandleResult<String>(s => s.Contains(CALL_LIMIT_EXCEEDED))
                .WaitAndRetryForeverAsync(retryAttempt => TimeSpan.FromMilliseconds(TIME_BETWEEN_TWO_REQUEST));

            var bulkHeadPolicy = Policy.BulkheadAsync<String>(1, 10)
                .WrapAsync(retryPolicy);

            return bulkHeadPolicy;
        }
    }
}
