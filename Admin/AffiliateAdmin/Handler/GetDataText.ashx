<%@ WebHandler Language="C#" Class="GetDataText" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class GetDataText : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new GetDataTextService().GetDataText(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}