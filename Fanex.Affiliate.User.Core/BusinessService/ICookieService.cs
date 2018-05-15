namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface ICookieService
    {
        string GetCookie(string name);

        void SetCookie(string name, string value, bool httpOnly = true);

        void RemoveCookie(string name);

        void ClearAllCookies();
    }
}
