<%@ WebHandler Language="C#" Class="ChangePass" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class ChangePass : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        new ChangePassService().ChangePass(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}