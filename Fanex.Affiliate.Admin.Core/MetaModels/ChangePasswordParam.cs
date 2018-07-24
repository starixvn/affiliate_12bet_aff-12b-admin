using System.ComponentModel.DataAnnotations;
using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.MetaModels
{
    public class ChangePasswordParam
    {
        public int UserId { get; set; }

        public int AffId { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password")]
        [SpParam(Ignored = true)]
        public string ConfirmPassword { get; set; }

        public string Ip { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }
    }
}
