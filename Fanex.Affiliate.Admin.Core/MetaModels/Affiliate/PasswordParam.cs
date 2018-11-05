using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Affiliate
{
    public class PasswordParam
    {
        public int UserId { get; set; }

        public int AffId { get; set; }

        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*\\d+.*\\d+)(?=.*[a-zA-Z].*[a-zA-Z]).{6,}$")]
        public string Password { get; set; }

        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*\\d+.*\\d+)(?=.*[a-zA-Z].*[a-zA-Z]).{6,}$")]
        public string ConfirmPassword { get; set; }

        public string Ip { get; set; }
    }
}
