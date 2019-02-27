<%@ WebHandler Language="C#" Class="UpdAffType" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class UpdAffType : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new UpdateAffiliateType().Update(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}