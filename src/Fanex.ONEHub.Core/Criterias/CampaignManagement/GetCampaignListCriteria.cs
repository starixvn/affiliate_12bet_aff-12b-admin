namespace Fanex.Affiliate.Admin.Core.Criterias.CampaignManagement
{
    using Data;

    public class GetCampaignListCriteria : PagingCriteria
    {
        [SpParam(SqlParamName = "CampaignSearch")]
        public string Search { get; set; }

        public string StatusIds { get; set; }

        public string Description { get; set; }

        public bool IsExportExcel { get; set; }

        public override string GetSettingKey() => "Aff_LandingPage_Campaign_ManageSel";

        public override bool IsValid() => true;
    }
}
