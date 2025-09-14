using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.LandingPage
{
    public class DeleteAssetsParam : BaseAssetParam
    {
        [Required]
        public string MediaIds { get; set; }
    }
}
