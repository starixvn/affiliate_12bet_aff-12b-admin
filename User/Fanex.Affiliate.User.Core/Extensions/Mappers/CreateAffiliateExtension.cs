namespace Fanex.Affiliate.User.Core.Extensions.Mappers
{
    using Commands.Account;
    using MetalModels;

    public static class CreateAffiliateExtension
    {
        public static void Map(this CreateAffiliateCommand command, UserSignUpParam param)
        {
            command.FirstName = param.FirstName;
            command.LastName = param.LastName;
            command.Gender = param.Gender;
            command.Email = param.Email;
            command.Phone = param.Phone;
            command.Address = param.Address;
            command.City = param.City;
            command.State = param.State;
            command.Postal = param.Postal;
            command.Country = param.Country;
            command.SiteUrl = param.SiteUrl;
            command.MoreSiteUrls = param.MoreSiteUrls;
            command.NoSiteDesc = param.NoSiteDesc;
            command.PreferredLanguage = param.PreferredLanguage;
            command.Player_UserName = param.Player_UserName;
            command.QuestionId = param.QuestionId;
            command.Answer = param.Answer;
            command.ContactApps = param.ContactApps;
            command.Ip = param.Ip;
            command.Language = param.Language;
            command.UserName = param.UserName;
            command.AffType = param.AffType;
            command.AffParentCode = param.AffParentCode;
            command.Player_CustId = param.Player_CustId;
            command.Player_Currency = param.Player_Currency;
        }
    }
}
