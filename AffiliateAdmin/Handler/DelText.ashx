<%@ WebHandler Language="C#" Class="DelText" %>

using System;
using System.Web;
using DNAide.Data;
using DNAide.Core;
using System.Web.SessionState;

public class DelText : IHttpHandler, IRequiresSessionState
{   
    public void ProcessRequest (HttpContext context) {
        if (context.Request.UrlReferrer != null)
        {
            IBCHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/plain";
            string listIdBanner = context.Request.Params["params"];
            string result = DataHandler.Text.DelDataText(listIdBanner);
            context.Response.Write(result);
        }
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}