using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Administrator
{
    public class UpdateUserParam: UserInfoParam
    {
        [Range(Validation.MinId, Validation.MinId, ErrorMessage = "User Id is invalid!")]
        public int SelectedUserId { get; set; }

        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }

        [Range(Validation.MinId, Validation.MinId, ErrorMessage = "Please choose department!")]
        public int GroupId { get; set; }
    }
}
