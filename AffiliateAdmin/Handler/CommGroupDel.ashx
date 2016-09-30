<%@ WebHandler Language="C#" Class="CommGroupDel" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class CommGroupDel : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        new CommGroupDelService().CommGroupDel(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}