<%@ WebHandler Language="C#" Class="UpdateUser" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class UpdateUser : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new UpdateUserService().Update(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}