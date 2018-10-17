using System;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class CustomerMemberParam
    {
        public int AffId { get; set; }

        public byte MemberType { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool IsGetAll { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
