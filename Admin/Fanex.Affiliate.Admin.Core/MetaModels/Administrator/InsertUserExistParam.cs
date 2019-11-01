using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Administrator
{
    public class InsertUserExistParam: UserInfoParam
    {
        [MaxLength(20)]
        [Required(ErrorMessage = "Please input user name!")]
        public string UserName { get; set; }

        [Range(Validation.MinId, Validation.MinId, ErrorMessage = "Please choose department!")]
        public int GroupId { get; set; }
    }
}
