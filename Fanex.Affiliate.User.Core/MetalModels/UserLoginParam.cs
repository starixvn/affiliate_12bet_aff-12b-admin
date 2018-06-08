using System.ComponentModel.DataAnnotations;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Data;

namespace Fanex.Affiliate.User.Core.MetalModels
{
    public class UserLoginParam : BaseParam
    {
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "ErrmsgEntryUsername")]
        public string UserName { get; set; }

        [SpParam(Ignored = true)]
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "ErrmsgEntryPassword")]
        public string Password { get; set; }

        [SpParam(Ignored = true)]
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "ErrmsgEntryValidCode")]
        public string ValidationKey { get; set; }

        public string LoginIP { get; set; }

        public int ServerId { get; set; }

        public string Language { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        [SpParam(Ignored = true)]
        [Required(ErrorMessage = "Login failed")]
        public string EncriptionKey { get; set; }

        [SpParam(Ignored = true)]
        [Required(ErrorMessage = "Login failed")]
        public string EncriptionIv { get; set; }
    }
}