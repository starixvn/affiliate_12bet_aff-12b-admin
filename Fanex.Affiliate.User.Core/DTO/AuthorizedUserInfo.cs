namespace Fanex.Affiliate.User.Core.DTO.Auth
{
    public class AuthorizedUserInfo
    {
        public int AffId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int CurrencyId { get; set; }

        public int GroupId { get; set; }

        public int AffType { get; set; }

        public string PreferredLanguage { get; set; }
    }
}
