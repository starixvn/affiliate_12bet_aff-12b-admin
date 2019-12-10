
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Member
{
    public class ActiveMemberParam: UserInfoParam
    {
        [Required]
        public string UserNames { get; set; }
    }
}
