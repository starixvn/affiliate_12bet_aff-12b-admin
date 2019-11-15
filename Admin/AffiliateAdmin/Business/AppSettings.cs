using System.Configuration;
using System.Web;
using DNAide.Core;

public static class AppSettings
{
    public static bool VirtualDir
    {
        get
        {
            return GetValue("VirtualDir") == "1";
        }
    }

    public static string GetValue(string key)
    {
        return ConfigurationManager.AppSettings[key];
    }

    #region ---------- for Session-------------

    public static class Session
    {
        public static int CustID
        {
            get
            {
                return CastAide.AsInt32(HttpContext.Current.Session["CustID"], -1);
            }
            set
            {
                HttpContext.Current.Session["CustID"] = value;
            }
        }

        public static string RoleAccess
        {
            get
            {
                var value = HttpContext.Current.Session["RoleAccess"];
                return value == null ? string.Empty : value.ToString();
            }
            set
            {
                HttpContext.Current.Session["RoleAccess"] = value;
            }
        }
    }

    #endregion ---------- for Session-------------
}