using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Administrator
{
    public class ChangeStatusParam: UserInfoParam
    {
        [Range(Validation.MinId, Validation.MaxId, ErrorMessage = "User Id is invalid!")]
        public int SelectedUserId { get; set; }

        public bool IsOpen { get; set; }
    }
}
