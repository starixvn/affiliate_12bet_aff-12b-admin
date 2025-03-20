namespace Fanex.Affiliate.Admin.Core.MetaModels.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class InsertDomainParam : UserInfoParam
    {
        [Required]
        public string DomainName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string BlockMarkets { get; set; }

        public bool IsSSL { get; set; }

        public bool IsAffDomain { get; set; }
    }
}
