using System.Security.Cryptography;

namespace Fanex.Affiliate.User.Core.DTO
{
    public class RsaKeyPair
    {
        public RSAParameters PublicKey { get; set; }

        public RSAParameters PrivateKey { get; set; }
    }
}
