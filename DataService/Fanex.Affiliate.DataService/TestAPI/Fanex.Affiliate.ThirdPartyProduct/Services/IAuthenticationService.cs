namespace Fanex.Affiliate.ThirdPartyProduct.Site.Services
{
    public interface IAuthenticationService
    {
        bool Auth(string userName, string pass);
    }
}