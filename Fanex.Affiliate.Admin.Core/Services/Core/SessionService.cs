using System;
using System.Web;
using Fanex.Affiliate.Admin.Core.Enum;

namespace Fanex.Affiliate.Admin.Core.Services.Core
{
    public class SessionService : ISessionService
    {
        public int CustId
        {
            get
            {
                return Convert.ToInt32(HttpContext.Current.Session["CustID"]);
            }
            set
            {
                HttpContext.Current.Session["CustID"] = value;
            }
        }

        public int OnUserId
        {
            get
            {
                return Convert.ToInt32(HttpContext.Current.Session["On_UserID"]);
            }
            set
            {
                HttpContext.Current.Session["On_UserID"] = value;
            }
        }

        public int SelectedAffId
        {
            get
            {
                return Convert.ToInt32(HttpContext.Current.Session["AffId"]);
            }
            set
            {
                HttpContext.Current.Session["AffId"] = value;
            }
        }

        public AffType SelectedAffType
        {
            get
            {
                return (AffType)HttpContext.Current.Session["AffType"];
            }
            set
            {
                HttpContext.Current.Session["AffType"] = value;
            }
        }

        public string SelectedAffUserName
        {
            get
            {
                return HttpContext.Current.Session["AffUsername"] as string;
            }
            set
            {
                HttpContext.Current.Session["AffUsername"] = value;
            }
        }

        public string SessionId
        {
            get
            {
                return HttpContext.Current.Session.SessionID;
            }
        }
    }
}
