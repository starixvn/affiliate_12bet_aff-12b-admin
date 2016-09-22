<%@ WebHandler Language="C#" Class="UpdAffInfo" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class UpdAffInfo : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new UpdateAffiliateService().Update(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}