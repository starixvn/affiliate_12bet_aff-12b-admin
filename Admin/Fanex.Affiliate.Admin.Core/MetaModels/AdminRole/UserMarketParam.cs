using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.AdminRole
{
    public class UserMarketParam
    {

        [Required(ErrorMessage = "Market Id is invalid!")]
        public string Key { get; set; }

        public bool Checked { get; set; }
    }
}
