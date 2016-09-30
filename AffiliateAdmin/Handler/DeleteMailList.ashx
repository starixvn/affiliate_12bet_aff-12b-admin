<%@ WebHandler Language="C#" Class="DeleteMailList" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class DeleteMailList : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new DeleteMailListService().DeleteMailList(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}