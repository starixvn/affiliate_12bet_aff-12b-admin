using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.DTO.UserPanel
{
    [Serializable]
    public class UserAccount
    {
        public int AffId { get; set; }

        public string AffName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool? Gender { get; set; }

        [PermissionCheck(Function.AffEmail)]
        public string Email { get; set; }

        [PermissionCheck(Function.AffPhone)]
        public string Phone { get; set; }

        [JsonIgnore]
        public string ContactApps { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Postcode { get; set; }

        public string Country { get; set; }

        [PermissionCheck(Function.AffWebUrl)]
        public string SiteUrl { get; set; }

        [PermissionCheck(Function.AffWebUrl)]
        public string MoreSiteUrls { get; set; }

        [PermissionCheck(Function.AffWebUrl)]
        public string NoSiteDescription { get; set; }

        [PermissionCheck(Function.AffPaymentInfo)]
        public string PreferredPayment { get; set; }

        [PermissionCheck(Function.AffPaymentInfo)]
        public string PlayerUsername { get; set; }

        [PermissionCheck(Function.AffPaymentInfo)]
        public string NetellerAccountId { get; set; }

        [PermissionCheck(Function.AffPaymentInfo)]
        public string MoneybookersEmail { get; set; }

        [PermissionCheck(Function.AffPaymentInfo)]
        public string BankName { get; set; }

        [PermissionCheck(Function.AffPaymentInfo)]
        public string BankAddress { get; set; }

        [PermissionCheck(Function.AffPaymentInfo)]
        public string BankCode { get; set; }

        [PermissionCheck(Function.AffPaymentInfo)]
        public string BankAccountName { get; set; }

        [PermissionCheck(Function.AffPaymentInfo)]
        public string BankAccountNumber { get; set; }

        [PermissionCheck(Function.SecurityQA)]
        public string Question { get; set; }

        [PermissionCheck(Function.SecurityQA)]
        public int? QuestionId { get; set; }

        [PermissionCheck(Function.SecurityQA)]
        public string Answer { get; set; }

        public string Lang { get; set; }

        public bool? MailOption { get; set; }

        public IEnumerable<ContactApp> ContactAppList { get; set; }

        public IEnumerable<KeyValuePair<int, string>> SecurityQuestions { get; set; }
    }
}
