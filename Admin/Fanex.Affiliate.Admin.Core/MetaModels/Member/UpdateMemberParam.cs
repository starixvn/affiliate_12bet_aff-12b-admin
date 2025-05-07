using Fanex.Affiliate.Admin.Core.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Member
{
    public class UpdateMemberParam : UserInfoParam
    {
        [Range(Validation.MinId, Validation.MaxId, ErrorMessage = "The member is invalid!")]
        public int MemberId { get; set; }

        public DateTime FromDate { get; set; }
    }
}
