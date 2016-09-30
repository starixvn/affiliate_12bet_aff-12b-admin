<%@ WebHandler Language="C#" Class="GetMessageList" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class GetMessageList : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
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