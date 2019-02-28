namespace Fanex.Affiliate.Admin.Core.MetaModels.Affiliate
{
    public class AffiliateInfoParam
    {
        public int UserId { get; set; }

        public string AffId { get; set; }

        public int AffIdNumber { get; set; }

        public int GroupId { get; set; }

        public string Language { get; set; }

        public string Status { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public byte SortBy { get; set; }

        public bool SortDirectionType { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public bool IsExportExcel { get; set; }
    }
}
