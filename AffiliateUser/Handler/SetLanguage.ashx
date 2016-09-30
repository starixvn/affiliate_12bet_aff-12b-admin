<%@ WebHandler Language="C#" Class="SetLanguage" %>

using System.Web;
using System.Web.SessionState;
using AffiliateUser.Business.Services;

public class SetLanguage : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new SetLanguageService().SetLanguage(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}