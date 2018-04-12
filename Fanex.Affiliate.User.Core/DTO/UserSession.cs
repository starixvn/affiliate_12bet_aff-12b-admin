namespace Fanex.Affiliate.User.Core.DTO.Auth
{
    public class UserSession
    {
        public bool IsInternal { get; set; }

        public string PreferredLanguage { get; set; }

        public string UserName { get; set; }

        public int AffId { get; set; }

        public int OnUserId { get; set; }
    }
}