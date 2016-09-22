<%@ WebHandler Language="C#" Class="UpText" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class UpText : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new UpdateTextService().Update(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}