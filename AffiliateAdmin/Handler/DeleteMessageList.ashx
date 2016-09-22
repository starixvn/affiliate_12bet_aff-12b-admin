<%@ WebHandler Language="C#" Class="DeleteMessageList" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class DeleteMessageList : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new DeleteMessageListService().DeleteMessageList(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}