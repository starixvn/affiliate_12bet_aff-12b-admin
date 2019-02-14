namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class ReferrersDetailParam : ReferrersParam
    {
        public string SiteUrl { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
