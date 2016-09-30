<%@ WebHandler Language="C#" Class="Adjustment" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class Adjustment : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        new AdjustmentService().Adjustment(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}