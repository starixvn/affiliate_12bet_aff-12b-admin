using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.DTO.LandingPage
{
    public class ModuleComponentItem
    {
        [Required(ErrorMessage = "Module is required")]
        public int ModuleId { get; set; }
        public int? MediaId { get; set; }
        public string ExtraInfo { get; set; }
        public int OrderId { get; set; }
    }
}
