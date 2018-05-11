using System.ComponentModel.DataAnnotations;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Data;

namespace Fanex.Affiliate.User.Core.MetalModels
{
    public class UpdatePasswordParam
    {
        public int AffId { get; set; }

        public int UserId => AffId;

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "OldPassandNewPass")]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "OldPassandNewPass")]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "ReenterNewPassw")]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "ReenterNewPassw")]
        [SpParam(Ignored = true)]
        public string ConfirmNewPassword { get; set; }

        public string Ip { get; set; }

        public string Language { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }
    }
}
