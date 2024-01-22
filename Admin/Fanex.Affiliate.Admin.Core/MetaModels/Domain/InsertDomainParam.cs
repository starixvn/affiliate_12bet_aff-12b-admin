using Fanex.Data;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Domain
{
    public class InsertDomainParam : UserInfoParam
    {
        [Required]
        public string DomainName { get; set; }

        [SpParam(SqlParamName = "BlockTargetCountry")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ExcludedTargetCountries { get; set; }

        public bool IsSSL { get; set; }

        [SpParam(Ignored = true)]
        public bool IsAffDomain { get; set; }
    }
}
