
using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Member
{
    public class DeleteMemberParam: UserInfoParam
    {
        [Range(Validation.MinId, Validation.MaxId)]
        public int MemberId { get; set; }

        public int AffId { get; set; }

        public int DepositMemberId { get; set; }
    }
}
