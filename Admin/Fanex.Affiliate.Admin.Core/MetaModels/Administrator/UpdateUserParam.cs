using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Administrator
{
    public class UpdateUserParam: UserInfoParam
    {
        [Range(Validation.MinId, Validation.MaxId, ErrorMessage = "User Id is invalid!")]
        public int UserId { get; set; }

        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }

        [Range(Validation.MinId, Validation.MaxId, ErrorMessage = "Please choose department!")]
        public int GroupId { get; set; }
    }
}
