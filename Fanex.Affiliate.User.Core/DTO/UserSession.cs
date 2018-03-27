namespace Fanex.Affiliate.User.Core.DTO.Auth
{
    public class UserSession
    {
        public bool IsInternal { get; set; }

        public string LanguageCode { get; set; }

        public string UserName { get; set; }

        public int CustID { get; set; }
    }
}