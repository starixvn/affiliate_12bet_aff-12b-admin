<%@ WebHandler Language="C#" Class="UpdateAcc" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class UpdateAcc : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new UpdateAccountService().Update(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}