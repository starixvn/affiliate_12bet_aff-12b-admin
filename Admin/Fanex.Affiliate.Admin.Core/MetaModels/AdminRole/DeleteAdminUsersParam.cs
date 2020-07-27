
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.AdminRole
{
    public class DeleteAdminUsersParam
    {
        [Required(ErrorMessage = "User Ids is required!")]
        public string UserIds { get; set; }

        public int ByUserId { get; set; }

        public string Ip { get; set; }
    }
}
