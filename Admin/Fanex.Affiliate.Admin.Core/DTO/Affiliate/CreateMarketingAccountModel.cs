using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fanex.Affiliate.Admin.Core.Commands.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels;

namespace Fanex.Affiliate.Admin.Core.DTO.Affiliate
{
    public class CreateMarketingAccountModel : UserInfoParam
    {
        // General Info
        public int AffId { get; set; }

        public string ParentAffName { get; set; }

        public string AffCode { get; set; }

        public string AffName { get; set; }

        public string UserName { get; set; }

        public string Status { get; set; }

        public bool IsPrivate { get; set; }

        public int AffType { get; set; }

        public int GroupId { get; set; }

        public string PreferredLanguage { get; set; }

        public string TargetCountryCode { get; set; }

        // Personal
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool? Gender { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string ContactApps { get; set; }

        // Additional Info

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Postal { get; set; }

        public string Country { get; set; }

        public int QuestionId { get; set; }

        public string Answer { get; set; }

        // Site 
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string SiteUrl { get; set; }

        public string MoreSiteUrls { get; set; }

        public string NoSiteDesc { get; set; }

        // Payment 
        public string PreferredPayment { get; set; }

        public string PlayerUsername { get; set; }

        public int? PlayerCustId { get; set; }

        public int? PlayerCurrencyId { get; set; }

        public bool? MailOption { get; set; }

        // Others
        public IEnumerable<ContactApp> ContactAppList { get; set; }

        public CreateMarketingAccountCommand CreateCommand()
        {
            return new CreateMarketingAccountCommand
            {
                UserId = UserId,
                AffType = AffType,
                Password = string.Empty,
                GroupId = GroupId,

                FirstName = FirstName,
                LastName = LastName,
                Gender = Gender,
                Email = Email,
                Phone = Phone,
                ContactApps = ContactApps,

                Address = Address,
                City = City,
                State = State,
                Postal = Postal,
                Country = Country,
                
                SiteUrl = SiteUrl,
                MoreSiteUrls = MoreSiteUrls,
                NoSiteDesc = NoSiteDesc,
                
                PreferredLanguage = PreferredLanguage,
                QuestionId = QuestionId,
                Answer = string.Empty,

                PreferredPayment = PreferredPayment,
                Player_CustId = PlayerCustId,
                Player_UserName = PlayerUsername,
                Player_Currency = PlayerCurrencyId,

                Ip = Ip,
                Language = PreferredLanguage,
                TargetCountryCode = TargetCountryCode,
            };
        }
    }
}
