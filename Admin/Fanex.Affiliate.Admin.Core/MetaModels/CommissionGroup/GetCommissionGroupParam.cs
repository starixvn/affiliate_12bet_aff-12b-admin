using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup
{
    public class GetCommissionGroupParam
    {
        [Range(Validation.MinId, Validation.MaxId, ErrorMessage = "GroupId not exists!")]
        public int GroupId { get; set; }

        public string GroupName { get; set; }

        [Range(Validation.MinPageIndex, Validation.MaxPageIndex)]
        public int PageIndex { get; set; }

        [Range(Validation.MinPageSize, Validation.MaxPageSize)]
        public int PageSize { get; set; }

        public int Nrows { get; set; }
    }
}
