<%@ WebHandler Language="C#" Class="UpdateStatus" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class UpdateStatus : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new UpdateStatusService().Update(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}