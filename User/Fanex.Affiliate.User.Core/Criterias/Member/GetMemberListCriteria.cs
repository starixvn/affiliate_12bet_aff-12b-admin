namespace Fanex.Affiliate.User.Core.Criterias
{
    using System;
    using Utilities;

    public class GetMemberListCriteria : BaseCriteria
    {
        private const string SpKey = "Aff_MemberList_Sel";

        public int AffId { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public bool IsGetAll { get; set; }

        public byte MemberType { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int NRows { get; set; }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => AffId > 0;
    }
}
