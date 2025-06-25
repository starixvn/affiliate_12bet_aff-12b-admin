using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Commands.Affiliate;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class UserAccountParam : AffiliateBasicInfoParam
    {
        public int GroupId { get; set; }

        public int AffType { get; set; }

        // Contact Info
        [ConditionalRequired("IsMarketingAccount")]
        public string FirstName { get; set; }

        [ConditionalRequired("IsMarketingAccount")]
        public string LastName { get; set; }

        [ConditionalRequired("IsMarketingAccount")]
        public bool Gender { get; set; }

        [ConditionalRequired("IsMarketingAccount")]
        [EmailAddress]
        public string Email { get; set; }

        [ConditionalRequired("IsMarketingAccount")]
        public string Phone { get; set; }

        public string ContactApps { get; set; }

        // Mailing Info
        [ConditionalRequired("IsMarketingAccount")]
        public string Address { get; set; }

        [ConditionalRequired("IsMarketingAccount")]
        public string City { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string State { get; set; }

        [ConditionalRequired("IsMarketingAccount")]
        public string Postal { get; set; }

        [ConditionalRequired("IsMarketingAccount")]
        public string Country { get; set; }

        // Website Info
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string SiteUrl { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MoreSiteUrls { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string NoSiteDesc { get; set; }

        // Preference
        [ConditionalRequired("IsMarketingAccount")]
        public string PreferredLanguage { get; set; }

        [ConditionalRequired("IsMarketingAccount")]

        public int QuestionId { get; set; }

        [ConditionalRequired("IsMarketingAccount")]
        public string Answer { get; set; }

        // Payment Info
        public string PreferredPayment { get; set; }

        public int Player_CustId { get; set; } = 1;

        public string Player_UserName { get; set; }

        public int? Player_Currency { get; set; }

        // Others
        public string IP { get; set; }

        public string Language { get; set; }

        // Target Country code
        public string TargetCountryCode { get; set; }

        public IEnumerable<ContactApp> ContactAppList { get; set; }

        public bool IsMarketingAccount => false;

        public CreateMarketingAccountCommand CreateCommand()
        {
            return new CreateMarketingAccountCommand
            {
                UserId = UserId,
                GroupId = GroupId,
                Ip = IP,
                Language = Language,
                TargetCountryCode = TargetCountryCode,

                // Contact Info
                FirstName = FirstName,
                LastName = LastName,
                Gender = Gender,
                Email = Email,
                Phone = Phone,
                ContactApps = ContactApps,

                // Mailing Info
                Address = Address,
                City = City,
                State = State,
                Postal = Postal,
                Country = Country,

                // Website Info
                SiteUrl = SiteUrl,
                MoreSiteUrls = MoreSiteUrls,
                NoSiteDesc = NoSiteDesc,

                // Preference
                PreferredLanguage = PreferredLanguage,
                QuestionId = QuestionId,
                Answer = Answer,

                // Payment Info
                PreferredPayment = PreferredPayment,
                Player_CustId = Player_CustId,
                Player_UserName = Player_UserName,
            };
        }
    }
}
