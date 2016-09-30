<%@ WebHandler Language="C#" Class="UpdateAcc" %>

using System.Web;
using System.Web.SessionState;
using AffiliateUser.Business.Services;

public class UpdateAcc : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new UpdateAccountService().UpdateAccount(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}