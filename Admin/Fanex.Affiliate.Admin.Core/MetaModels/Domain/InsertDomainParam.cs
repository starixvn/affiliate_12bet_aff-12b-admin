using Fanex.Data;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Domain
{
    public class InsertDomainParam : UserInfoParam
    {
        [Required(ErrorMessage = "Please input Domain's name!")]
        public string DomainName { get; set; }

        [SpParam(SqlParamName = "BlockMarket")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string BlockMarkets { get; set; }

        public bool IsSSL { get; set; }

        [SpParam(Ignored = true)]
        public bool IsAffDomain { get; set; }
    }
}
