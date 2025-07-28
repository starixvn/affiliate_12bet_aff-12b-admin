using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Affiliate.Infrastructure.Helpers;
using Fanex.Affiliate.Admin.Core.Commands.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.Utilities;

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
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FirstName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string LastName { get; set; }

        public bool? Gender { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Phone { get; set; }

        public string ContactApps { get; set; }

        // Additional Info

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Address { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string City { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]

        public string State { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Postcode { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Country { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int QuestionId { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
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
                Password = GetRandomEncryptPassword(),
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
                Postcode = Postcode,
                Country = Country,
                
                SiteUrl = SiteUrl,
                MoreSiteUrls = MoreSiteUrls,
                NoSiteDesc = NoSiteDesc,
                
                PreferredLanguage = PreferredLanguage,
                QuestionId = QuestionId,
                Answer = Answer,

                PreferredPayment = PreferredPayment,
                Player_CustId = PlayerCustId,
                Player_UserName = PlayerUsername,
                Player_Currency = PlayerCurrencyId,

                Ip = Ip,
                TargetCountryCode = TargetCountryCode,
            };
        }

        private static string GetRandomEncryptPassword()
        {
            const int PASSWORD_LENGTH = 10;

            return CryptographyHelper.Encrypt(Generator.GetRandomString(PASSWORD_LENGTH));
        }
    }
}
