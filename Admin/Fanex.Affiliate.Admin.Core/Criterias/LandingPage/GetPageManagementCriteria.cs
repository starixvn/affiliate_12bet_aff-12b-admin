namespace Fanex.Affiliate.Admin.Core.Criterias.LandingPage
{
    public class GetPageManagementCriteria : PagingCriteria
    {
        private const string SP_NAME = "";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string TargetCountry { get; set; }

        public string Language { get; set; }

        public string Deal { get; set; }

        public string Status { get; set; }

        public string LandingPageName { get; set; }
    }
}
