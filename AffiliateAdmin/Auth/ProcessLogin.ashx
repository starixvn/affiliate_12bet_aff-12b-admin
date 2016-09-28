<%@ WebHandler Language="C#" Class="ProcessLogin" %>

using System;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using DNAide.Web;

public class ProcessLogin : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/html";
        context.Response.Cache.SetCacheability(HttpCacheability.Private);
        string _username = context.Request.Form["un"];
        string _password = context.Request.Form["pw"];
        string errmsg = "";
        int custid = 0;
        int roleid = 0;
        string isok = "";
        string lang = "";
        //1 : invalid code
        //2 : wrong password
        //0 : successful 
        try
        {
            int errcode = DataHandler.Login.ExecLogin(ref _username, _password, ref errmsg, context, ref custid, ref roleid, lang);
            if (errcode == 0)
            {
                var authTicket = new FormsAuthenticationTicket(custid.ToString(), true, 20);
                var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                {
                    HttpOnly = true,
                    Secure = FormsAuthentication.RequireSSL,
                    Path = FormsAuthentication.FormsCookiePath,
                    Domain = FormsAuthentication.CookieDomain,
                    Expires = authTicket.Expiration
                };

                AppSettings.Session.CustID = custid;
                AppSettings.Session.OnUser = DataHandler.Login.LoginOnUser(context);
                AppSettings.Session.RoleID = roleid;
                AppSettings.Session.UserName = _username;
                AppSettings.Session.RoleAccess = DataHandler.Login.AssignRoles(custid);
                isok = "OK";

                context.Response.SetCookie(cookie);

            }
            else
            {
                isok = errmsg;
            }

            context.Response.Write(isok);
        }
        catch (Exception ex)
        {
            if (Common.IsUnderMaintenance())
            {
                context.Response.Write("UM");
                return;
            }

            context.Response.Write(ex.Message.ToString());
        }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
