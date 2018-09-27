using System;

namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    public class MemberParam : BaseParam
    {
        public int AffId { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public bool IsGetAll { get; set; }

        public byte MemberType { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int NRows { get; set; }
    }
}
