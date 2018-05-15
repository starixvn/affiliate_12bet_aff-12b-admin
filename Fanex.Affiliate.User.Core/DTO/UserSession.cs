namespace Fanex.Affiliate.User.Core.DTO.Auth
{
    public class UserSession
    {
        public string PreferredLanguage { get; set; }

        public string UserName { get; set; }

        public int AffId { get; set; }

        public int OnUserId { get; set; }

        public byte AffType { get; set; }
    }
}