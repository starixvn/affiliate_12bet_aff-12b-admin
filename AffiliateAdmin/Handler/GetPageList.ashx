<%@ WebHandler Language="C#" Class="GetPageList" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Text;

public class GetPageList : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        var deviceType = Common.GetDeviceType(context.Request.Params["deviceType"]);

        context.Response.Write(Common.GeneratePagesDropdown("sportCode", deviceType));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
