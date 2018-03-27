namespace Fanex.Affiliate.User.Core.MetalModels
{
    public class UserLogin
    {
        public string UserName { get; set; }

        public string EncryptedPass { get; set; }

        public string ValidationKey { get; set; }

        public string LoginIP { get; set; }

        public int ServerID { get; set; }
    }
}