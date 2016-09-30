<%@ WebHandler Language="C#" Class="GetEditedText" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class GetEditedText : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new GetEditedTextService().GetEditedText(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}