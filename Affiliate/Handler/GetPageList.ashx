<%@ WebHandler Language="C#" Class="GetPageList" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Text;

public class GetPageList : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {

        if (context.Request.UrlReferrer != null)
        {
            AffHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/plain";
            var deviceType = Common.GetDeviceType(context.Request.Params["deviceType"]);
            
            var result = Common.GenListLink2Page(string.Empty, deviceType);
            
            context.Response.Write(result);
        }
    }
    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
