using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Administrator
{
    public class InsertUserParam: UserInfoParam
    {
        [MaxLength(20)]
        [Required(ErrorMessage = "Please input user name!")]
        public string UserName { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }

        [Range(Validation.MinId, Validation.MaxId, ErrorMessage = "Please choose department!")]
        public int GroupId { get; set; }
    }
}
