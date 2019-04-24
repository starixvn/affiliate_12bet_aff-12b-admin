using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class UserAccountParam : AffiliateBasicInfoParam
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public bool Gender { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        public string ContactApps { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        public string State { get; set; }

        [Required]
        public string Postcode { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        public string SiteUrl { get; set; }

        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        public string MoreSiteUrls { get; set; }

        public string NoSiteDescription { get; set; }

        public string PreferredPayment { get; set; }

        public string PlayerUsername { get; set; }

        public string NetellerAccountId { get; set; }

        public string MoneybookersEmail { get; set; }

        public string BankName { get; set; }

        public string BankAddress { get; set; }

        public string BankCode { get; set; }

        public string BankAccountName { get; set; }

        public string BankAccountNumber { get; set; }

        public bool MailOption { get; set; }

        public string Language { get; set; }
    }
}
