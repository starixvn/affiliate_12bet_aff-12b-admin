using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.LandingPage
{
    public class DeleteModuleParam : UserInfoParam
    {
        [Required(ErrorMessage = "Please select modules to delete")]
        public string ModuleIds { get; set; }
    }
}
