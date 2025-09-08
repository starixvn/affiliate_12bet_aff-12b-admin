using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.LandingPage
{
    public class DeleteMediasParam : AssetListParam
    {
        [Required]
        public string MediaIds { get; set; }

        public string Ip { get; set; }
    }
}
