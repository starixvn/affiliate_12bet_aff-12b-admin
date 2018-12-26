using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Affiliate
{
    public class AffiliateInfoParam
    {
        public int UserId { get; set; }

        [RegularExpression("^(?!(0))([0-9]{0,9})$")]
        public string AffId { get; set; }

        public int numberAffId { get; set; }

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
