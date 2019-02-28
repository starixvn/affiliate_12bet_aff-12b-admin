namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class ReferrersDetailParam : ReferrersParam
    {
        public string SelectedSiteUrl { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
