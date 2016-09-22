<%@ WebHandler Language="C#" Class="CommGroupUpd" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class CommGroupUpd : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        new CommGroupUpdService().CommGroupUpd(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}