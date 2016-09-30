<%@ WebHandler Language="C#" Class="DeleteMessageList" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services.Message;

public class DeleteMessageList : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.UrlReferrer == null)
        {
            return;
        }

        IBCHandler.InitializeCulture(HttpContext.Current);
        context.Response.ContentType = "text/plain";

        string idList = context.Request.Params["params"].Trim();
        if (string.IsNullOrEmpty(idList))
        {
            context.Response.Write("Null data is sent to server");
            return;
        }

        idList = idList.Substring(0, idList.Length - 1);
        var service = new DeleteMessageListService();
        var result = service.DeleteMessageList(idList);

        context.Response.Write(result.Message);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}