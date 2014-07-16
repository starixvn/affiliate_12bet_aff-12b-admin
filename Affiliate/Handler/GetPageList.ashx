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
            var hasAll = GetHasAll(context.Request.Params["hasAll"]);

            string result = hasAll 
                ? Common.GenListLink2Page("sportCode", string.Empty, true, deviceType) 
                : Common.GenListLink2Page("sportCode", deviceType);
            
            context.Response.Write(result);
        }
    }
    
    private bool GetHasAll(string hasAll)
    {
        bool isAll;
        var result = bool.TryParse(hasAll, out isAll);

        return result && isAll;
    }
    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
