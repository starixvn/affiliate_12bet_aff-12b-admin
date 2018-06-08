namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface ICryptographyService
    {
        string EncryptPassword(string inputPassword, string encryptionKey, string iv);
    }
}
