<%@ WebHandler Language="C#" Class="EditRate" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class EditRate : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new EditRateService().EditRate(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}