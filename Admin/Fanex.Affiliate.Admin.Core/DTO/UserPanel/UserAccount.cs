using System.Diagnostics.CodeAnalysis;

namespace Fanex.Affiliate.Admin.Core.DTO.UserPanel
{
    public class UserAccount
    {
        public int AffId { get; set; }

        public string AffName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Gender { get; set; }

        public string Status { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string ContactApps { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Postal { get; set; }

        public string Country { get; set; }

        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        public string SiteUrl { get; set; }

        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        public string MoreSiteUrl { get; set; }
        
        public string NoSiteDescription { get; set; }

        public string PreferredPayment { get; set; }

        public string PreferredLanguage { get; set; }

        public string PlayerUsername { get; set; }

        public string NetellerAccountID { get; set; }

        public string MoneybookersEmail { get; set; }

        public string BankName { get; set; }

        public string BankAddress { get; set; }

        public string BankSwiftCode { get; set; }

        public string BankAccountName { get; set; }

        public string BankAccountNo { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public bool MailOption { get; set; }
    }
}
