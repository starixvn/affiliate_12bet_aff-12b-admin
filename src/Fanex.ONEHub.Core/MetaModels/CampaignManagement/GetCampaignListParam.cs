namespace Fanex.Affiliate.Admin.Core.MetaModels.CampaignManagement
{
    public class GetCampaignListParam
    {
        public string Search { get; set; }

        public string StatusIds { get; set; }

        public string Description { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public bool IsExportExcel { get; set; }
    }
}
