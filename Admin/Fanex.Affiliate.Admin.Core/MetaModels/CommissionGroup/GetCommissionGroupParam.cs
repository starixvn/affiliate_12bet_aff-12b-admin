using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup
{
    public class GetCommissionGroupParam
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 20;

        public int Nrows { get; set; }
    }
}
