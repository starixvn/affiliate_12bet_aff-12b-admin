<%@ WebHandler Language="C#" Class="ListUser" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class ListUser : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new ListUserService().ListUser(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}