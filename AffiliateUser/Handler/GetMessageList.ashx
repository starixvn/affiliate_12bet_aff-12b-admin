<%@ WebHandler Language="C#" Class="GetMessageList" %>

using System.Web;
using System.Web.SessionState;
using AffiliateUser.Business.Services;

public class GetMessageList : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {

        if (context.Request.UrlReferrer == null)
        {
            return;
        }

        new GetMessageListService().GetMessageList(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
