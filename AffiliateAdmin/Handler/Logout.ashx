<%@ WebHandler Language="C#" Class="Logout" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
public class Logout : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/html";
        DataHandler.Login.Logout(context);
        FormsAuthentication.SignOut();
        context.Session.Clear();
        context.Session.Abandon();

        FormsAuthentication.RedirectToLoginPage();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
